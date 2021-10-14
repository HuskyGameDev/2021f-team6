using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerHealthbarSet = playerController.hp;
        playerTextHealthSet = playerController.hp;
        buildingHealthbarSet = 100;
        buildingTextHealthSet = 100;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
