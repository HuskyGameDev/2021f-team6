using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    public Image healthBarImage;
    private int textHealthSet;
    private float healthBar;
    private float healthbarSet;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        healthbarSet = playerController.hp;
        textHealthSet = playerController.hp;
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealthBar = playerController.hp;
        int currentHealthText = playerController.hp;
        healthBar = currentHealthBar / healthbarSet;
        healthBarImage.fillAmount = healthBar;
        int healthBarText = (currentHealthText * 100) / textHealthSet;
        if (healthBar <= 0)
        {
            healthText.text = "HP: " + 0;
        }
        else
        {
            healthText.text = "HP: " + healthBarText;
        }
    }
}
