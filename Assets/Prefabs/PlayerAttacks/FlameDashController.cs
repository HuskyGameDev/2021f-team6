using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDashController : MonoBehaviour
{
    private Collider2D thisCollider;
    private Rigidbody2D rigidBody;
    public Rigidbody2D player;

    public float expandRate, maxSize;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        player.MovePosition(rigidBody.position);
    }
}
