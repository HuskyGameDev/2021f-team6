using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWavecontroller : MonoBehaviour
{
    private Collider2D thisCollider;
    private Rigidbody2D rigidBody;

    public float expandRate, maxsize;
    private Vector3 expand;
    private bool maxHit = false;
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
        Debug.Log(Time.deltaTime);
        if (transform.localScale.x <= maxsize && !maxHit)
        {
            transform.localScale += expand * Time.deltaTime;
        }
        if(transform.localScale.x >= maxsize)//controls expand and shrink states.
        {
            maxHit = true;
        }
        if (transform.localScale.x >= 0 && maxHit)
        {
            transform.localScale += (-expand) * Time.deltaTime;
        }
    }
}
