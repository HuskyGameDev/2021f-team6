using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackType : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float dmg, speed, lifespan;   //attack damage, projectile speed, lifespan in seconds
    public bool aoe; //whether or not the attack is a aoe effect vs. single-hit projectile



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        //move proj away from player
        float newX = Mathf.Cos(rigidBody.rotation * Mathf.Deg2Rad);
        float newY = Mathf.Sin(rigidBody.rotation * Mathf.Deg2Rad);
        //transform.position += new Vector3(2f * newX, 2f * newY, 0 );

        rigidBody.AddRelativeForce(Vector2.right * speed);

        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (!aoe && collider.tag != "Enemy")
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerController>().hp -= Mathf.RoundToInt(dmg);
            }
            if (collider.CompareTag("Building"))
            {
                collider.GetComponent<BuildingController>().health -= Mathf.RoundToInt(dmg);
            }
            Destroy(gameObject, 0.0f);
        }
    }
}

