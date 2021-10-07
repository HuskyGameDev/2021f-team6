using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public int Health;
    public int WalkSpeed;
    public Vector2 aim;
    public GameObject projectile;
    private int charge;
    private bool charging;

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

    }

    // Update is called once per frame
    void Update()
    {
        rb.rotation = -Mathf.Atan2(GameObject.Find("Player").GetComponent<Rigidbody2D>().position.x, GameObject.Find("Player").GetComponent<Rigidbody2D>().position.y) * Mathf.Rad2Deg + 120f;
        rb.velocity = ((GameObject.Find("Player").GetComponent<Rigidbody2D>().position - this.GetComponent<Rigidbody2D>().position));

        if (rb.velocity.magnitude > WalkSpeed)
        {
            rb.velocity = rb.velocity.normalized * WalkSpeed;
        }
        //Operate each of the different behaviors.
        switch (name)
        {
            //Meatshield and Walkers will be virtually identical
            case "Meatshield(Clone)":
            case "Walker(Clone)":
                break;
            //Will have a quick dash as its attack
            case "Rusher(Clone)":
                if(FindTargetDistance(GameObject.Find("Player")) < 10 && charge % 3000 >= 0 && charge % 3000 <= 150)
                {
                    WalkSpeed = 32;
                    rb.velocity = rb.velocity.normalized * WalkSpeed;
                    
                } else
                {
                    WalkSpeed = 2;
                    
                }
                break;
            //Can fire a weak projectile back
            case "Shooter(Clone)":
                if (charge % 300  == 0)
                {
                    GameObject monsterbullet = Instantiate(projectile, transform.position, transform.rotation);
                }
               
                
                break;
            
        }
        if(Health <= 0)
        {
            //Run the animation for death and shut down the object
            Destroy(gameObject);
        }
        charge++;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //Health = Health - collision.gameObject.Dmg;
            if (Health <= 0)
            {
                //Run the animation for death and shut down the object
                Destroy(gameObject);
            }
        } else
        {
            
        }
        
    }

    float FindTargetDistance(GameObject target)
    {
        return Vector2.Distance(target.GetComponent<Rigidbody2D>().position, this.GetComponent<Rigidbody2D>().position);
    }
}
