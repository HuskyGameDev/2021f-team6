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

    public GameObject[] attacks;    //list of available attacks
    private int curAtk = 0;         //index of the current attack in attacks[]


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

        //weapon switching with Q and E
        if (Input.GetKeyDown(KeyCode.Q))
            curAtk--;
        if (Input.GetKeyDown(KeyCode.E))
            curAtk++;
        curAtk = (curAtk + attacks.Length) % attacks.Length;
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
        if (isDead()) 
        {
            Dead();
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
        GameObject bullet = Instantiate(attacks[curAtk], transform.position, transform.rotation);

    }
    void Dead() 
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //Time.timeScale = 0;
    }

    //return
    public bool isDead() { return hp <= 0; }
}
