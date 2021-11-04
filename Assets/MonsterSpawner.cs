using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private int count;
    public GameObject[] monsters;
    [HideInInspector]
    // Start is called before the first frame update
    void Start()
    {
        count = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.gameIsPaused)
        {

        } else
        {
            
            if (count % 3600 == 0)
            {
                int score = GameObject.Find("Canvas").GetComponent<CanvasController>().currentScore;
                int level = GameObject.Find("Canvas").GetComponent<CanvasController>().currentLevel;

                int NumofMonsters = (int)Mathf.Max(1, Mathf.Floor(score / 5 + level));
                for (int i = 0; i < NumofMonsters; i++)
                {
                    int rand = Random.Range(0, monsters.Length);
                    int x = Random.Range(-25, 25);
                    int y = Random.Range(-25, 25);


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
                
            }
            count++;
        }
        
    }
}
