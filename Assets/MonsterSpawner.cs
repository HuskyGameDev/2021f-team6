using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private int count;
    public GameObject[] monsters;
    public int hp = 3;
    [HideInInspector]
    // Start is called before the first frame update
    void Start()
    {
        count = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        int rand = Random.Range(0, monsters.Length);
        if(count % 1000 == 0)
        {
            int x = Random.Range(-31, 33);
            int y = Random.Range(-15, 17);
            
            
            GameObject monster = Instantiate(monsters[rand], new Vector2(x, y), new Quaternion());

            if (x < 0 && y < 0)
            {
                monster.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else if (x < 0 && y >= 0)
            {
                monster.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (x >= 0 && y < 0)
            {
                monster.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (x >= 0 && y >= 0)
            {
                monster.GetComponent<SpriteRenderer>().color = Color.black;
            }

            monster.GetComponent<MonsterBehavior>().aim = GameObject.Find("Player").GetComponent<Rigidbody2D>().position - monster.GetComponent<Rigidbody2D>().position;
        }
        count++;
    }
}
