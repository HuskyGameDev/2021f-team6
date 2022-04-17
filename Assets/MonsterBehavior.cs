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
    private float intelligence;
    private bool tele = true;
    private bool flip = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Animator animator;
    public string type = "default";    //elemental type of the monster
    public float slowed;    //time in seconds that the monster is slowed by an ice effect
    private Color iced = new Color(0f, 1f, 1f, .8f);    //color shift for iced monsters
    private Color original_color;

    private Rigidbody2D player_rb;
    private GameObject[] buildings;
    private GameObject monsterSpawner;
    private Transform t;

    private CanvasController canvas;
    public int notSus;
    private bool lockout;
    private float angle;
    private int dodge_crg;
    AnimatorStateInfo animinfo;


    // Start is called before the first frame update
    void Start()
    {
        //Send the creature on its way
        animator = GetComponent<Animator>();
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        original_color = sr.color;
        aim = aim.normalized;
        //rb.rotation = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg + 90f;
        //rb.velocity += aim * WalkSpeed;
        charge = Random.Range(0, 1000);
        dodge_crg = 0;
        lockout = false;
        canvas = GameObject.Find("Canvas").GetComponent<CanvasController>();
        notSus = (int)Mathf.Max(1, 300 - 2 * Mathf.Pow(canvas.currentLevel,1.5f));
        timing = Random.Range(notSus, 1000);
        totalHealth = Health;

        player_rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        buildings = GameObject.FindGameObjectsWithTag("Building");
        monsterSpawner = GameObject.Find("Monster Spawner");
        monsterSpawner.GetComponent<MonsterSpawner>().monsterCount++;
        intelligence = Random.Range(0, monsterSpawner.GetComponent<MonsterSpawner>().monster_Int);

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Walker)
        {
            int dodgechance = Random.Range(0, 1000);
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

            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            


            
            if (dodgechance < intelligence && FindDistancetoPlayer() < 10 && !lockout && projectiles.Length != 0)
            {
                //Find the angle from closest Projectile

                float shortest_proj = Mathf.Infinity;
                GameObject closest_proj = null;
                foreach (GameObject projectile in projectiles)
                {
                    float curr_distance = Mathf.Abs(FindTargetDistance(projectile));
                    GameObject curr_proj = projectile;
                    if (curr_distance < shortest_proj)
                    {
                        shortest_proj = curr_distance;
                        closest_proj = curr_proj;
                    }
                }
                Vector2 closestVel = closest_proj.GetComponent<Rigidbody2D>().velocity;

                if (FindTargetDistance(closest_proj) <= 10)
                {
                    if (Vector2.SignedAngle(rb.velocity, closestVel) > 0)
                    {
                        rb.velocity = Vector2.Perpendicular(closest_proj.GetComponent<Rigidbody2D>().velocity);
                    }
                    else
                    {
                        rb.velocity = -Vector2.Perpendicular(closest_proj.GetComponent<Rigidbody2D>().velocity);
                    }

                    lockout = true;
                    dodge_crg = charge;
                }
                

            }
            else if (lockout)
            {
                lockout = (charge <= (dodge_crg + 100));
            }
            else if (FindDistancetoPlayer() < shortest + 15)
            {
                rb.velocity = ((player_rb.position - rb.position));
            }
            else
            {
                rb.velocity = ((closest.GetComponent<Rigidbody2D>().position - rb.position));
            }

            if (rb.velocity.magnitude > WalkSpeed && !lockout)
            {
                rb.velocity = rb.velocity.normalized * WalkSpeed;
            }

            if (rb.velocity.x > 0)
            {
                sr.flipX = false;
            } else if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
        }

        if (Rusher)
        {
            
            if (FindDistancetoPlayer() < 10 && charge % 3000 >= 0 && charge % 3000 <= 60)
            {
                animator.SetBool("Dash", true);
                WalkSpeed = 32;
                rb.velocity = rb.velocity.normalized * WalkSpeed;

            }
            else
            {
                animator.SetBool("Dash", false);
                WalkSpeed = 2;

            }
        }

        if (Shooter)
        {
            if (charge % timing == 0)
            {
                animator.SetTrigger("Shoot");
                if (projectile.tag == "Enemy")
                {
                    GameObject monsterbullet = Instantiate(projectile, transform.position, transform.rotation);
                    charge = 0;
                } else
                {
                    GameObject monsterbullet = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg)));
                    charge = 0;
                }
                
            }

            timing = Random.Range((int)notSus, 1000);
            
        }

        if (Teleporter)
        {
            if (Health <= totalHealth / 2 && tele)
            {
                animator.SetTrigger("Start");

                animinfo = animator.GetCurrentAnimatorStateInfo(0);
                if (animinfo.normalizedTime % 1 < .99)
                {

                } else
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
                    } while (!(potentialy > -25 && potentialy < 25));


                    rb.MovePosition(rb.position + new Vector2(potentialx, potentialy));
                    tele = false;

                    animator.SetTrigger("Finish");

                    animinfo = animator.GetCurrentAnimatorStateInfo(0);
                    if (animinfo.normalizedTime % 1 < .99)
                    {

                    }
                    else
                    {

                        if (charge % 3000 == 0)
                        {
                            tele = true;
                            charge = 0;
                        }
                    }
                }
                
            }
        }

        if (Coward)
        {
            if (FindDistancetoPlayer() < 10f)
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
                    charge = 0;
                }
            }
        }

        if(Health <= 0)
        {
            //Run the animation for death and shut down the object
            
            animator.SetTrigger("Dead");

            animinfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animinfo.normalizedTime % 1 < .99)
            {
                rb.velocity *= 0;
                Coward = false;
                Teleporter = false;
                Shooter = false;
                Rusher = false;
                Walker = false;
            } else
            {
                monsterSpawner.GetComponent<MonsterSpawner>().monsterCount--;
                int drops = Random.Range(0, 100);
                if (drops < 25)
                {
                    int randomNumber = Random.Range(0, 4);
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
                    else if (randomNumber == 3)
                    {
                        Instantiate(GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().quicktime,
                            transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    }

                }
                Store.Gold += 15;
                int stop = 0;
                Destroy(gameObject);
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().currentScore = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().currentScore + 1;
            }
            
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
