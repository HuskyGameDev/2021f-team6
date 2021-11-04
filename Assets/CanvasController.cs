using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject CanvasUI;
    private GameObject player;
    private PlayerController playerController;
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
    private string minutes, seconds;

    public Text levelText;
    [HideInInspector]
    public int currentLevel;
    [HideInInspector]
    public int currentScore;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
        currentScore = 0;
        
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerHealthbarSet = playerController.hp;
        playerTextHealthSet = playerController.hp;
        buildingHealthbarSet = 100;
        buildingTextHealthSet = 100;
        startTheTimer();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " +currentLevel.ToString();
        scoreText.text = "Score: " +currentScore.ToString();
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
        if (playerHealthBar <= 0)
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
            seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
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
    public void HideUI()
    {
        CanvasUI.SetActive(false);
    }
    public void ShowUI()
    {
        CanvasUI.SetActive(true);
    }
}
