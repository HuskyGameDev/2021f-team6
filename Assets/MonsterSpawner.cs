using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private int count;
    public GameObject[] monsters;
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
    }
}
