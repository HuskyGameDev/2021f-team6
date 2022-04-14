using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject shield;
    public GameObject quicktime;
    [HideInInspector]
    public bool shieldOn;
    public bool quicktimeOn;
    public float quicktimeCD;
    //object components
    private Rigidbody2D rigidBody;

    //object variables
    public float moveSpeed;         //speed player moves at in units/second
    public float rotateSpeed;       //speed player rotates towards mouse
    //[HideInInspector]
    public int maxHp;
    public int hp;                  //player health points

    public GameObject SpellNotOwnedAlert;
    public Image[] ESpellIcon = new Image[8]; //defined in player object
    public GameObject[] attacks;    //list of available attacks
    public static int curAtk = 0;         //index of the current attack in attacks[]
    [Range(0.5f,1.5f)]
    public float deathScale;
    public Transform spawnBlood;
    public Transform hitBlood;
    public Transform bloodObject;

    public Transform spawnBulletPoint;

    public Sprite[] characterSprite;

    private bool held = false;
    private float holdCool = 0;
    [HideInInspector]
    public float cooldown;
    [HideInInspector]
    public float countdown;

    private Animator animator;
    private float speedSlowDownTime;
    private float speedCD;
    private bool speedUpOn;
    private int speedIncrease;
    private double quicktimeMultiplier; //how much faster you can fire spells
    private float[] lastshot = new float[8];
    private float[] castInterval = new float[8]; //defualt untill first spell is cast;


    // Start is called before the first frame update
    void Start()
    {

        //defaults
        cooldown = 10;
        speedSlowDownTime = 5;
        countdown = 0;
        speedCD = 0;
        quicktimeCD = 5;
        quicktimeMultiplier = 1;
        shieldOn = false;
        speedUpOn = false;
        quicktimeOn = false;
        rigidBody = GetComponent<Rigidbody2D>();
        maxHp = 100;
        hp = maxHp;
        animator = GetComponent<Animator>();
        setSpellSelfActiveSpell(0);
        for (int i = 0; i <= 6; i++)
        {
            lastshot[i] = 0;
            castInterval[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldOn) 
        {
            shield.SetActive(true);
            if (Time.time > countdown) 
            {
                shieldOn = false;
                shield.SetActive(false);
            }
        }
        if (speedUpOn)
        {
            if (Time.time > speedCD)
            {
                speedUpOn = false;
                moveSpeed = moveSpeed - speedIncrease;
                speedIncrease = 0;
            }
        }
        if (quicktimeOn)
        {
            quicktimeMultiplier = 1.5;
            if (Time.time > quicktimeCD)
            {
                quicktimeOn = false;
                quicktimeMultiplier = 1;
            }
        }
        if (hp < 0)
            hp = 0;

        if (Input.GetKey(KeyCode.Mouse0) && held)
        {
            holdCool += Time.deltaTime;
            if (holdCool >= .05)
            {
                Shoot();
                holdCool = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            holdCool = 0;
            held = false;
        }
        if (Time.time - lastshot[curAtk] < castInterval[curAtk] / quicktimeMultiplier)
        {
            ESpellIcon[curAtk].color = Color.black;
        }
        else { ESpellIcon[curAtk].color = Color.white; }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        //weapon switching with numbers
        //nutrual spell
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curAtk = 0;
            setSpellSelfActiveSpell(0);
        }
        //Fire spell
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(Store.ESpellOwned[0])
            {
                curAtk = 1;
                setSpellSelfActiveSpell(1);
            }
            else
            {
                //Show alert
                ShowSpellNotOwnedAlert();
            }
        }
        //Lighting spell
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Store.ESpellOwned[1])
            {
                curAtk = 2;
                setSpellSelfActiveSpell(2);
            }
            else
            {
                //Show alert
                ShowSpellNotOwnedAlert();
            }
        }
        //Ice spell
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Store.ESpellOwned[2])
            {
                curAtk = 3;
                setSpellSelfActiveSpell(3);
            }
            else
            {
                //Show alert
                ShowSpellNotOwnedAlert();
            }
        }
        //ice spray spell
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (Store.ESpellOwned[3])
            {
                curAtk = 4;
                setSpellSelfActiveSpell(4);
            }
            else
            {
                //Show alert
                ShowSpellNotOwnedAlert();
            }
        }
        //meteor shower spell
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (Store.ESpellOwned[4])
            {
                curAtk = 5;
                setSpellSelfActiveSpell(5);
            }
            else
            {
                //Show alert
                ShowSpellNotOwnedAlert();
            }
        }
        //Flame Dash
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (Store.ESpellOwned[5])
            {
                curAtk = 6;
                setSpellSelfActiveSpell(6);
            }
            else
            {
                //Show alert
                ShowSpellNotOwnedAlert();
            }
        }
        //Tidal Wave
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (Store.ESpellOwned[6])
            {
                curAtk = 7;
                setSpellSelfActiveSpell(7);
            }
            else
            {
                //Show alert
                ShowSpellNotOwnedAlert();
            }
        }
        /*//weapon switching with Q and E
        if (Input.GetKeyDown(KeyCode.Q))
            curAtk--;
        if (Input.GetKeyDown(KeyCode.E))
            curAtk++;*/
        curAtk = (curAtk + attacks.Length) % attacks.Length;

    }
    private void FixedUpdate()
    {
        if (!isDead())
        {
            Rotate();
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalinput = Input.GetAxis("Vertical");
            //Debug.Log(horizontalInput + " " + verticalinput);

            if (verticalinput > 0 && Mathf.Abs(horizontalInput) < .2) 
            {
                animator.SetBool("Down", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Up", true);
            }
            else if (verticalinput < 0 && Mathf.Abs(horizontalInput) < .2)
            {
                animator.SetBool("Up", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Down", true);
            }
            else if (horizontalInput > 0)
            {
                animator.SetBool("Down", false);
                animator.SetBool("Up", false);
                animator.SetBool("Left", false);
                animator.SetBool("Right", true);
            }
            else if (horizontalInput < 0)
            {
                animator.SetBool("Down", false);
                animator.SetBool("Up", false);
                animator.SetBool("Right", false);
                animator.SetBool("Left", true);
            }

            Vector3 input = new Vector3(horizontalInput, verticalinput, 0f);
            //characterBody.AddForce(input * moveSpeed * Time.deltaTime);
            //transform.position = transform.position + (input * moveSpeed * Time.deltaTime);
            transform.Translate(input * moveSpeed * Time.deltaTime);
        }
        if (isDead()) 
        {
            Dead();
        }
    }

    //make player aim towards mouse
    private void Rotate() 
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.transform.position);
        mousePos.x -= playerPos.x;
        mousePos.y -= playerPos.y;
        float angle = Mathf.Atan2(mousePos.y,mousePos.x) * Mathf.Rad2Deg ;
        spawnBulletPoint.rotation = Quaternion.RotateTowards(spawnBulletPoint.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotateSpeed);
    }

    //fire the current weapon
    private void Shoot() 
    {
        if (PauseMenu.gameIsPaused || Time.timeScale == 0) { return; }
        if (Time.time - lastshot[curAtk] < castInterval[curAtk] / quicktimeMultiplier) {return; }
        lastshot[curAtk] = Time.time;
        GameObject bullet = Instantiate(attacks[curAtk], spawnBulletPoint.position, spawnBulletPoint.rotation);
        AttackType attackType = bullet.GetComponent<AttackType>();
        Rigidbody2D attackRB = bullet.GetComponent<Rigidbody2D>();
        castInterval[curAtk] = attackType.castInterval;

        // Play bullet sound
        switch (curAtk)
        {
            case 0:
                // Play Bullet sound
                break;

            case 1:
                // Play Fireball sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Attacks/Fireball");
                break;

            // 2 is lightning, done in lightningController script

            case 3:
                // Play Frost Circle sound
                break;

            case 4:
                // Play ice spray sound
                break;
        }
        held = attackType.hold;
        attackRB.rotation += Random.Range(-1f * attackType.spread, attackType.spread);
    }
    void Dead() 
    {
        animator.SetTrigger("Dead");
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        transform.localScale = new Vector3(deathScale,deathScale,deathScale);
        float randomRotation = Random.Range(1f,360f);
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,randomRotation));
        float bloodRandomRotation = Random.Range(1f,360f);
        Instantiate(bloodObject,spawnBlood.position,Quaternion.Euler(new Vector3(0,0,bloodRandomRotation)));
        //Time.timeScale = 0;
    }

    //return
    public bool isDead() { return hp <= 0; }

    public void SpawnBlood() 
    {
        float randomX = Random.Range(-1,1);
        float randomY = Random.Range(-1,1);
        Vector3 spawnBloodVec = new Vector3(spawnBlood.position.x + randomX, spawnBlood.position.y + randomY, spawnBlood.position.z);
        Instantiate(hitBlood,spawnBloodVec,transform.rotation);
    }
    public void Damage(int amount) 
    {
        if (!shieldOn) 
        {
            SpawnBlood();
            hp = hp - amount;
        }
    }

    public void SpeedUp(int amount) 
    {
        speedIncrease = speedIncrease + amount;
        moveSpeed = moveSpeed + amount;
        speedUpOn = true;
        speedCD = Time.time + speedSlowDownTime;
    }


    public void ShowSpellNotOwnedAlert()
    {
        SpellNotOwnedAlert.SetActive(true);
        StartCoroutine(HideObjSec(1, SpellNotOwnedAlert));
        //SpellNotOwnedAlert.SetActive(false);
    }
    private IEnumerator HideObjSec(int sec, GameObject obj)
    {
        //yield on a new YieldInstruction that waits for sec seconds.
        yield return new WaitForSeconds(sec);
        obj.SetActive(false);
    }

    //chages the shown active spell on the shelf
    private void setSpellSelfActiveSpell(int spell)
    {
        for (int i = 0; i <= 7; i++)
        {
            ESpellIcon[i].color = Color.grey;
        }
        ESpellIcon[spell].color = Color.white;
    }
}
