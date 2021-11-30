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
    }
    public void Damage(int amount)
    {
        health = health - amount;
    }
}
