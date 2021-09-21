using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    public float rotateSpeed = 20f;
    //private float setMoveSpeed;
    private Rigidbody2D characterBody;
    private Transform playerRotation;
    public Transform spawnBullet;
    //private int HP;
    private bool isDeath;

    public Rigidbody2D bulletRB;
    void Start()
    {
        //setMoveSpeed = moveSpeed;
        //HP = 100;
        playerRotation = GetComponent<Transform>();
        characterBody = GetComponent<Rigidbody2D>();
        isDeath = false;
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
        if (!isDeath)
        {
            RotationPlayer();
            //isDeath = true;
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            characterBody.AddForce(input * moveSpeed*Time.deltaTime);
        }
    }
    void RotationPlayer() 
    {
        Vector3 mouse_pos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 player_pos = Camera.main.WorldToScreenPoint(playerRotation.transform.position);
        mouse_pos.x = mouse_pos.x - player_pos.x;
        mouse_pos.y = mouse_pos.y - player_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y,mouse_pos.x) * Mathf.Rad2Deg ;
        playerRotation.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(new Vector3(0,0,angle)),rotateSpeed);
    }

    void Shoot() 
    {
        Rigidbody2D bullet = Instantiate(bulletRB, spawnBullet.transform.position,spawnBullet.transform.rotation) as Rigidbody2D;
        //bullet.GetComponent<BulletController>().parentTransform = transform;
        //bullet.GetComponent<BulletController>().parentTag = transform.tag;
        Debug.Log("Shooting");
    }

}
