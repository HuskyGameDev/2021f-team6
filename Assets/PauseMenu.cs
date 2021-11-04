using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] 
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        canvas.SetActive(true);
        //GameObject.Find("Canvas").GetComponent<CanvasController>().ShowUI(); //does not come back for some reason
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        canvas.SetActive(false);
        //GameObject.Find("Canvas").GetComponent<CanvasController>().HideUI();
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
