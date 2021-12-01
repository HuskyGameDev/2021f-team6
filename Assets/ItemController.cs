using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    public string itemName;
    private PlayerController playerScript;
    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Destroy(this.gameObject,5f);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            if (itemName == "Heart") 
            {
                //Debug.Log("Heart");
                if (!playerScript.isDead())
                {
                    playerScript.hp = playerScript.hp + 10;
                    if (playerScript.hp > 100) 
                    {
                        playerScript.hp = 100;
                    }
                }
                Destroy(this.gameObject);
            }
            else if (itemName == "Shield")
            {
                //Debug.Log("Shield");
                
                if (!playerScript.isDead())
                {
                    playerScript.shieldOn = true;
                    playerScript.countdown = Time.time + playerScript.cooldown;
                }
                Destroy(this.gameObject);
            }
            else if (itemName == "SpeedUp")
            {
                //Debug.Log("SpeedUp");
                if (!playerScript.isDead())
                {
                    playerScript.SpeedUp(1);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
