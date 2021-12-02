using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float Health;
    public int WalkSpeed;
    public Vector2 aim;
    public int dmg;
    public int actionRate;
    public GameObject projectile;

    public bool Walker;
    public bool Shooter;
    public bool Rusher;
    public bool Teleporter;
    public bool Coward;

    private int charge;
    private int timing;
    private float totalHealth;
    private bool tele = true;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public string type = "default";    //elemental type of the monster
    public float slowed;    //time in seconds that the monster is slowed by an ice effect
    private Color iced = new Color(0f, 1f, 1f, .8f);    //color shift for iced monsters
    private Color original_color;

    private Rigidbody2D player_rb;
    private GameObject[] buildings;
    private GameObject monsterSpawner;


    // Start is called before the first frame update
    void Start()
    {
        //Send the creature on its way
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        original_color = sr.color;
        aim = aim.normalized;
        rb.rotation = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg + 90f;
        //rb.velocity += aim * WalkSpeed;
        charge = 0;
        timing = Random.Range(300, 1000);
        totalHealth = Health;

        player_rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        buildings = GameObject.FindGameObjectsWithTag("Building");
        monsterSpawner = GameObject.Find("Monster Spawner");
        monsterSpawner.GetComponent<MonsterSpawner>().monsterCount++;

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = aim * WalkSpeed;

        if (Walker)
        {
            float shortest = Mathf.Infinity;
            GameObject closest = null;
            foreach(GameObject building in buildings)
            {
                float curr_distance = FindTargetDistance(building);
                GameObject curr_build = building;
                if (curr_distance < shortest)
                {
                    shortest = curr_distance;
                    closest = curr_build;
                }
            }

            if (FindDistancetoPlayer() < shortest)
            {
                rb.velocity = ((player_rb.position - rb.position));
            }
            else
            {
                rb.velocity = ((closest.GetComponent<Rigidbody2D>().position - rb.position));
            }

            rb.velocity = ((player_rb.position - rb.position));
            if (rb.velocity != Vector2.zero)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg)), 10);
            }


            if (rb.velocity.magnitude > WalkSpeed)
            {
                rb.velocity = rb.velocity.normalized * WalkSpeed;
            }
        }

        if (Rusher)
        {
            if (FindDistancetoPlayer() < 10 && charge % 3000 >= 0 && charge % 3000 <= 150)
            {
                WalkSpeed = 32;
                rb.velocity = rb.velocity.normalized * WalkSpeed;

            }
            else
            {
                WalkSpeed = 2;

            }
        }

        if (Shooter)
        {
            if (charge % timing == 0)
            {
                GameObject monsterbullet = Instantiate(projectile, transform.position, transform.rotation);
            }

            timing = Random.Range(300, 1000);
        }

        if (Teleporter)
        {
            if (Health <= totalHealth / 2 && tele)
            {
                float potentialx;
                float potentialy;
                do
                {
                    potentialx = rb.position.x + Random.Range(-5, 5);
                } while (potentialx < -25 || potentialx > 25);

                do
                {
                    potentialy = rb.position.y + Random.Range(-5, 5);
                } while (!(potentialy > -25 && potentialy < 25)) ;

            
                rb.MovePosition(rb.position + new Vector2(potentialx, potentialy));
                tele = false;
                if (charge % 3000 == 0)
                {
                    tele = true;
                }
            }
        }

        if (Coward)
        {
            if (FindDistancetoPlayer() < 5f)
            {
                Walker = false;
                WalkSpeed = 10;
                charge = 0;
                rb.velocity = -((player_rb.position - rb.position));
            }
            else
            {
                WalkSpeed = 2;
                if (charge % 3000 == 0)
                {
                    Walker = true;
                }
            }
        }

        if(Health <= 0)
        {
            //Run the animation for death and shut down the object
            monsterSpawner.GetComponent<MonsterSpawner>().monsterCount--;
            int randomNumber = Random.Range(0, 3);
            if (randomNumber == 0)
            {
                Instantiate(GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().heart, 
                    transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (randomNumber == 1)
            {
                Instantiate(GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().shield,
                    transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (randomNumber == 2)
            {
                Instantiate(GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().speedUp,
                    transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().currentScore = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().currentScore + 1;
        }
        charge++;

        if(!type.Equals("ice") && slowed > 0)
        {
            slowed -= Time.deltaTime;
            rb.velocity /= 2;
            sr.color = iced;
        }
        else
        {
            sr.color = original_color;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Enemy" && collision.collider.tag != "Projectile")
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<PlayerController>().Damage(dmg);
                //collision.collider.GetComponent<PlayerController>().SpawnBlood();
            }
            if (collision.collider.CompareTag("Building"))
                collision.collider.GetComponent<BuildingController>().Damage(dmg);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone")) 
        {
            Health = 0;
        }
    }

    float FindTargetDistance(GameObject target)
    {
        return Vector2.Distance(target.GetComponent<Rigidbody2D>().position, this.GetComponent<Rigidbody2D>().position);
    }

    float FindDistancetoPlayer()
    {
        return Vector2.Distance(player_rb.position, this.rb.position);
    }
}
