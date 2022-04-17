using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [HideInInspector]
    public int health;
    public int maxHealth;
    public GameObject thisBuilding;
    public Sprite destructSprite;
    public Sprite contructSprite;
    public Sprite halfSprite;
    public SpriteRenderer SR;
    [HideInInspector]
    public bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        isDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) 
        {
            //thisBuilding.SetActive(false);
            GetComponent<PolygonCollider2D>().enabled = false;
            SR.sprite = destructSprite;
            isDestroyed = true;
            health = 0;
        }
        else if(health <= maxHealth / 2)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            SR.sprite = halfSprite;
            isDestroyed = false;
        }
        else if (health >= maxHealth / 2)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            SR.sprite = contructSprite; ;
            isDestroyed = false;
        }
    }
    public void Damage(int amount)
    {
        health = health - amount;
    }
}
