using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Collider2D thisCollider;

    public float dmg, speed, lifespan;   //attack damage, projectile speed, lifespan in seconds
    public string type;

    public bool hold;
    public float spread;

    public bool aoe; //whether or not the attack is a aoe or effect vs. single-hit projectile
    public float aoeSpeed;  //how fast the aoe affect applies damage
    private float aoeCool;    //time since last aoe damage

    private List<Collider2D> colliders;
    private ContactFilter2D filter = new ContactFilter2D();

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<Collider2D>();

        //move proj away from player
        float newX = Mathf.Cos(rigidBody.rotation * Mathf.Deg2Rad);
        float newY = Mathf.Sin(rigidBody.rotation * Mathf.Deg2Rad);
        //transform.position += new Vector3(2f * newX, 2f * newY, 0 );

        rigidBody.AddRelativeForce(Vector2.right * speed);

        if(lifespan > 0)
            Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (aoe)
        {
            aoeCool += Time.deltaTime;
            if (aoeCool >= aoeSpeed)
            {
                colliders = new List<Collider2D>();
                int cols = thisCollider.OverlapCollider(filter, colliders);
                foreach (Collider2D collider in colliders)
                {
                    if (collider != null && collider.CompareTag("Enemy"))
                        hit(collider);
                }

                aoeCool = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!aoe && collider.tag != "Player" && collider.tag != "AmbArea")
        {
            if (collider.CompareTag("Enemy"))
                hit(collider);
            Destroy(gameObject);
        }
    }

    public void hit(Collider2D monster)
    {
        monster.GetComponent<MonsterBehavior>().Health -= dmg;
        switch (type)
        {
            case "ice":
                if (monster.GetComponent<MonsterBehavior>().slowed <= 0)
                    monster.GetComponent<MonsterBehavior>().slowed = 5;
                break;
        }
    }
}

