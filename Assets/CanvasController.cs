using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject heart;
    public GameObject shield;
    public GameObject speedUp;

    private bool gamePause;
    public GameObject CanvasUI;
    //private GameObject player;
    private PlayerController playerController;
    private GameObject monsterSpawner;
    public Image playerHealthBarImage;
    private int playerTextHealthSet;
    private float playerHealthBar;
    private float playerHealthbarSet;
    public Text playerHealthText;

    public GameObject[] building;
    public Image buildingHealthBarImage;
    private int buildingTextHealthSet;
    private float buildingHealthBar;
    private float buildingHealthbarSet;
    public Text buildingHealthText;

    public Text timerText;
    private float startTime;
    private bool startTimer;
    [HideInInspector]
    public string minutes, seconds;
    [HideInInspector]
    public Text levelText;
    public Text monsterText;
    [HideInInspector]
    public int currentLevel;
    [HideInInspector]
    public int currentScore;
    public Text scoreText;

    //Gameover menu code
    public GameObject UI;
    public GameObject GameOverMenuUI;
    public Text GO_waves, GO_score, GO_time;

    //Pause menu code
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;

    //store menu code
    public GameObject storeMenu;

    bool canReward = false;
    int nextReward = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;
        gamePause = false;
        currentLevel = 0;
        currentScore = 0;
        

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        monsterSpawner = GameObject.Find("Monster Spawner");
        playerHealthbarSet = playerController.hp;
        playerTextHealthSet = playerController.hp;
        buildingHealthbarSet = 100;
        buildingTextHealthSet = 100;
        startTheTimer();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Wave: " +currentLevel.ToString();
        scoreText.text = "Score: " +currentScore.ToString();
        monsterText.text = "Monsters Remaining: " + monsterSpawner.GetComponent<MonsterSpawner>().monsterCount;
        float currentHealthBar = playerController.hp;
        int currentHealthText = playerController.hp;
        playerHealthBar = currentHealthBar / playerHealthbarSet;
        playerHealthBarImage.fillAmount = playerHealthBar;
        int healthBarText = (currentHealthText * 100) / playerTextHealthSet;
        if (playerHealthBar <= 0)
        {
            playerHealthText.text = "HP: " + 0;
        }
        else
        {
            playerHealthText.text = "HP: " + healthBarText;
        }

        int totalBuildingHealth = 0;
        foreach (GameObject currentBuilding in building)
        {
            totalBuildingHealth += currentBuilding.GetComponent<BuildingController>().health;
        }
        float currentBuildingHealthBar = totalBuildingHealth;
        int currentBuildingHealthText = totalBuildingHealth;
        buildingHealthBar = currentBuildingHealthBar / buildingHealthbarSet;
        buildingHealthBarImage.fillAmount = buildingHealthBar;
        int buildingHealthBarText = (currentBuildingHealthText * 100) / buildingTextHealthSet;

        if (currentLevel > nextReward)
        {
            canReward = true;
            nextReward = nextReward + 1;
            gamePause = true;
            if (gamePause)
            {
                storeMenu.SetActive(true);
                UI.SetActive(false);
                Time.timeScale = 0;
            }
        }

        if (totalBuildingHealth > 0) 
        {
            if (canReward)
            {
                int randomNumber = Random.Range(0, 4);
                Debug.Log(randomNumber);
                if (randomNumber == 0)
                {
                    foreach (GameObject currentBuilding in building)
                    {
                        if (!currentBuilding.GetComponent<BuildingController>().isDestroyed)
                            currentBuilding.GetComponent<BuildingController>().health = currentBuilding.GetComponent<BuildingController>().maxHealth;
                    }
                }
                else if (randomNumber == 1)
                {
                    if (!playerController.isDead())
                    {
                        playerController.hp = 100;
                    }
                }
                else if (randomNumber == 2)
                {
                    if (!playerController.isDead())
                    {
                        playerController.moveSpeed = playerController.moveSpeed + 1;
                    }
                }
                else if (randomNumber == 3)
                {
                    if (!playerController.isDead())
                    {
                        playerController.shieldOn = true;
                        playerController.countdown = Time.time + playerController.cooldown;
                    }
                }
                canReward = false;
            }
        }

        if (buildingHealthBar <= 0)
        {
            buildingHealthText.text = "HP: " + 0;
        }
        else
        {
            buildingHealthText.text = "HP: " + buildingHealthBarText;
        }

        if (startTimer)
        {
            float t = Time.time - startTime;
            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f0");
            timerText.text = minutes + ":" + seconds;
        }

        if ((playerController.isDead() && !gamePause) || (!gamePause && buildingHealthBar <= 0))
        {
            stopTheTimer();
            GO_waves.text = "Waves: " + currentLevel.ToString();
            GO_score.text = "Score: " + currentScore.ToString();
            GO_time.text = "Time: " + minutes + ":" + seconds;
            GameOverMenuUI.SetActive(true);
            UI.SetActive(false);
            Time.timeScale = 0;
        }
        else if (!playerController.isDead() && !gamePause) 
        {
            UI.SetActive(true);
            GameOverMenuUI.SetActive(false);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePause = !gamePause;
            if (gamePause)
            {
                Time.timeScale = 0;
                pauseMenuUI.SetActive(true);
                UI.SetActive(false);
            }
            else if (!gamePause)
            {
                Time.timeScale = 1;
                pauseMenuUI.SetActive(false);
                storeMenu.SetActive(false);
                UI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            gamePause = !gamePause;
            if (gamePause) 
            {
                storeMenu.SetActive(true);
                UI.SetActive(false);
                Time.timeScale = 0;
            }
            else if (!gamePause)
            {
                storeMenu.SetActive(false);
                UI.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }

    public void startTheTimer()
    {
        startTime = Time.time;
        startTimer = true;
    }
    public void stopTheTimer()
    {
        startTimer = false;
    }

    public void loadMenu()
    {
        GameOverMenuUI.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1f;
        gamePause = false;
    }
}
