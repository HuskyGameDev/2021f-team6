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
    private int charge;
    private int timing;

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

    }

    // Update is called once per frame
    void Update()
    {
        if(FindTargetDistance(GameObject.Find("Player")) < FindTargetDistance(GameObject.Find("Building")))
        {
            rb.velocity = ((GameObject.Find("Player").GetComponent<Rigidbody2D>().position - this.GetComponent<Rigidbody2D>().position));
        } else
        {
            rb.velocity = ((GameObject.Find("Building").GetComponent<Rigidbody2D>().position - this.GetComponent<Rigidbody2D>().position));
        }
        
        if(rb.velocity != Vector2.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg)), 10);
        }
        

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
                if (charge % timing  == 0)
                {
                    GameObject monsterbullet = Instantiate(projectile, transform.position, transform.rotation);
                }

                timing = Random.Range(300, 1000);
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
        if (collision.collider.tag != "Enemy" && collision.collider.tag != "Projectile")
        {
            if (collision.collider.CompareTag("Player"))
                collision.collider.GetComponent<PlayerController>().hp -= dmg;
            if (collision.collider.CompareTag("Building"))
                collision.collider.GetComponent<BuildingController>().health -= dmg;
        }
    }

    float FindTargetDistance(GameObject target)
    {
        return Vector2.Distance(target.GetComponent<Rigidbody2D>().position, this.GetComponent<Rigidbody2D>().position);
    }
}
