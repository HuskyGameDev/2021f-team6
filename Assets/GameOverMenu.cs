using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool gameIsOver = false;
    public GameObject GameOverMenuUI;

    public Text waves, score, time;

    // Start is called before the first frame update
    void Start()
    {
 
    }   

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            GameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
            waves.text = "Waves: "+ GameObject.Find("Canvas").GetComponent<CanvasController>().currentLevel;
            score.text = "Score: "+ GameObject.Find("Canvas").GetComponent<CanvasController>().currentScore;
            time.text = "Time: " +GameObject.Find("Canvas").GetComponent<CanvasController>().minutes + ":" + GameObject.Find("Canvas").GetComponent<CanvasController>().seconds;
        }
    }

    public void loadMenu()
    {
        gameIsOver = false;
        GameOverMenuUI.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
    }
}
