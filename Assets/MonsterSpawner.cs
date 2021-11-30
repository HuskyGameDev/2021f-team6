using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public int monsterCount;
    private int prevMonsterCount;
    public GameObject[] monsters_F;
    public GameObject[] monsters_I;
    public GameObject[] monsters_Fl;
    public GameObject[] monsters_D;

    public static bool gameIsStore;
    public GameObject storeMenuUI;
    public GameObject UI;
    private CanvasController canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<CanvasController>();
        monsterCount = 0;
        prevMonsterCount = 1;
        gameIsStore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gameIsPaused)
        {

        }
        else
        {
            if (monsterCount == 0)
            {
                if (!gameIsStore)  //Change this to be after the monsters are all dead and the store is closed
                {
                    canvas.currentLevel++;
                    int score = canvas.currentScore;
                    int level = canvas.currentLevel;
                    GameObject monster;

                    int NumofMonsters = (int)Mathf.Max(1, Mathf.Floor(score / 5 + level));
                    for (int i = 0; i < NumofMonsters; i++)
                    {

                        int x = Random.Range(-28, 28);
                        int y = Random.Range(-33, 24);

                        if (x < 0 && y < 0)
                        {
                            int rand = Random.Range(0, monsters_I.Length);
                            monster = Instantiate(monsters_I[rand], new Vector2(x, y), new Quaternion());
                            monster.GetComponent<MonsterBehavior>().aim = GameObject.Find("Player").GetComponent<Rigidbody2D>().position - monster.GetComponent<Rigidbody2D>().position;
                        }
                        else if (x < 0 && y >= 0)
                        {
                            int rand = Random.Range(0, monsters_F.Length);
                            monster = Instantiate(monsters_F[rand], new Vector2(x, y), new Quaternion());
                            monster.GetComponent<MonsterBehavior>().aim = GameObject.Find("Player").GetComponent<Rigidbody2D>().position - monster.GetComponent<Rigidbody2D>().position;
                        }
                        else if (x >= 0 && y < 0)
                        {
                            int rand = Random.Range(0, monsters_Fl.Length);
                            monster = Instantiate(monsters_Fl[rand], new Vector2(x, y), new Quaternion());
                            monster.GetComponent<MonsterBehavior>().aim = GameObject.Find("Player").GetComponent<Rigidbody2D>().position - monster.GetComponent<Rigidbody2D>().position;
                        }
                        else if (x >= 0 && y >= 0)
                        {
                            int rand = Random.Range(0, monsters_D.Length);
                            monster = Instantiate(monsters_D[rand], new Vector2(x, y), new Quaternion());
                            monster.GetComponent<MonsterBehavior>().aim = GameObject.Find("Player").GetComponent<Rigidbody2D>().position - monster.GetComponent<Rigidbody2D>().position;
                        }
                        prevMonsterCount = monsterCount;

                    }

                }
                else
                {
                    gameIsStore = !gameIsStore;
                    Time.timeScale = 0;
                    storeMenuUI.SetActive(true);
                    UI.SetActive(false);
                }

                prevMonsterCount = monsterCount;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameIsStore = false;
                Time.timeScale = 1;
                storeMenuUI.SetActive(false);
                UI.SetActive(true);
            }
        }
    }
}
