using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //object components
    private Rigidbody2D rigidBody;

    //object variables
    public float moveSpeed;         //speed player moves at in units/second
    public float rotateSpeed;       //speed player rotates towards mouse

    public int hp;                  //player health points

    public GameObject[] attacks;     //list of available attacks


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        if (!isDead())
        {
            RotatePlayer();

            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
            //characterBody.AddForce(input * moveSpeed * Time.deltaTime);
            transform.position = transform.position + (input * moveSpeed * Time.deltaTime);
        }
    }

    //make player aim towards mouse
    private void RotatePlayer() 
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.transform.position);
        mousePos.x -= playerPos.x;
        mousePos.y -= playerPos.y;
        float angle = Mathf.Atan2(mousePos.y,mousePos.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0,0,angle)), rotateSpeed);
    }

    //fire the current weapon
    private void Shoot() 
    {
        GameObject bullet = Instantiate(attacks[0], transform.position, transform.rotation);

    }

    //return
    public bool isDead() { return hp <= 0; }
}
