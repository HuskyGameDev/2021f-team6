using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] 
    public GameObject Canvas;
    public GameObject canvasUI;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Time.timeScale = 0;
                //Canvas.GetComponent<CanvasController>().gamePause = true;
                canvasUI.SetActive(false);
                //Canvas.GetComponent<CanvasController>().HideUI();

            }
            else if (!gameIsPaused) 
            {
                Time.timeScale = 1;
                //Canvas.GetComponent<CanvasController>().gamePause = false;
                Canvas.SetActive(true);
                //Canvas.GetComponent<CanvasController>().ShowUI();
            }
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void loadMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
    }
}
