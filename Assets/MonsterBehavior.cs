using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float Health;
    public int WalkSpeed;
    public Vector2 aim;
    public int dmg;
    public GameObject projectile;
    public bool Walker;
    public bool Shooter;
    public bool Rusher;
    public bool Teleporter;
    private int charge;
    private int timing;
    private float totalHealth;
    private bool tele = true;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //Send the creature on its way
        rb = GetComponent<Rigidbody2D>();
        aim = aim.normalized;
        rb.rotation = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg + 90f;
        rb.velocity += aim * WalkSpeed;
        charge = 0;
        timing = Random.Range(300, 1000);
        totalHealth = Health;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Walker)
        {
            if (FindTargetDistance(GameObject.Find("Player")) < FindTargetDistance(GameObject.Find("Building")))
            {
                rb.velocity = ((GameObject.Find("Player").GetComponent<Rigidbody2D>().position - this.GetComponent<Rigidbody2D>().position));
            }
            else
            {
                rb.velocity = ((GameObject.Find("Building").GetComponent<Rigidbody2D>().position - this.GetComponent<Rigidbody2D>().position));
            }

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
            if (FindTargetDistance(GameObject.Find("Player")) < 10 && charge % 3000 >= 0 && charge % 3000 <= 150)
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
                rb.MovePosition(rb.position + new Vector2(rb.position.x + Random.Range(-1, 1), rb.position.y + Random.Range(-1, 1)));
                tele = false;
                if (charge % 3000 == 0)
                {
                    tele = true;
                }
            }
        }

        if(Health <= 0)
        {
            //Run the animation for death and shut down the object
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().currentScore = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().currentScore + 1;
        }
        charge++;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Enemy" && collision.collider.tag != "Projectile")
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<PlayerController>().hp -= dmg;
                collision.collider.GetComponent<PlayerController>().SpawnBlood();
            }
            if (collision.collider.CompareTag("Building"))
                collision.collider.GetComponent<BuildingController>().health -= dmg;
        }
    }

    float FindTargetDistance(GameObject target)
    {
        return Vector2.Distance(target.GetComponent<Rigidbody2D>().position, this.GetComponent<Rigidbody2D>().position);
    }
}
