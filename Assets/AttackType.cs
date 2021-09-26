using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float dmg;   //attack damage
    public bool bullet; //whether or not the attack is a bullet type vs. an effect



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        float newX = Mathf.Cos(rigidBody.rotation * Mathf.Deg2Rad);
        float newY = Mathf.Sin(rigidBody.rotation * Mathf.Deg2Rad);
        transform.position += new Vector3(1.5f * newX, 1.5f * newY, 0 );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (bullet)
        {
            Destroy(gameObject);
        }

    }
}

