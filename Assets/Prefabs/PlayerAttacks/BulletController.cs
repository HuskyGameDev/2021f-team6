using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //public Transform parentTransform;
    //public string parentTag;
    public float bulletSpeed = 300f;    //speed of the bullet 
    //public int damage =1;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * bulletSpeed);
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject,0.2f);
    }
}
