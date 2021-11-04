using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public int health;
    public GameObject thisBuilding;
    public Sprite destructSprite;
    public SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) 
        {
            //thisBuilding.SetActive(false);
            GetComponent<PolygonCollider2D>().enabled = false;
            SR.sprite = destructSprite;
        }
    }
}
