using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostCircleController : MonoBehaviour
{
    private Collider2D thisCollider;
    private Rigidbody2D rigidBody;

    public float expandRate, maxSize;
    private Vector3 expand;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        expand = new Vector3(expandRate, expandRate, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += expand * Time.deltaTime;
            rigidBody.rotation += 360 * Time.deltaTime;
        }
    }
}
