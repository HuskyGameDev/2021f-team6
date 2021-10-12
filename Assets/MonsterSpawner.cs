using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private int count;
    public GameObject[] monsters;
    public int hp = 3;
    [HideInInspector]
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        count = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        int rand = Random.Range(0, monsters.Length );
        if(count % 300 == 0)
        {
            GameObject monster = Instantiate(monsters[rand], new Vector2(0, 0), new Quaternion());
            monster.GetComponent<MonsterBehavior>().aim = GameObject.Find("Player").GetComponent<Rigidbody2D>().position - monster.GetComponent<Rigidbody2D>().position;
        }
        count++;

        if (hp <= 0) 
        {
            Death();
        }
        if (damage > 0)
        {
            hp -= damage;
            damage = 0;
        }
    }
    void Death() 
    {
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject);
        enabled = false;
    }
}
