using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSprayController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.rotation += Random.Range(-15f, 15f);
        rigidBody.AddRelativeForce(Vector2.right * gameObject.GetComponent<AttackType>().speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
