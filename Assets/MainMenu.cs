using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;

    public AudioMixer volMixer;
    public Dropdown resDropdown;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        //Resolution Stuff
        resolutions = Screen.resolutions; //get resolutions from user device

        resDropdown.ClearOptions(); // clear current options from box

        List<string> options = new List<string>(); //string list to hold resolution names

        int currentResIndex = 0; //to set the default resolution

        for (int i = 0; i < resolutions.Length; i++)//adds each res name to list
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(options);//adds names to box
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Main Menu
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSettings()
    {
        SettingsMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
    }

    // Settings Menu
    public void ExitSettings()
    {
        SettingsMenu.SetActive(false);
    }
    public void SetMasterVol(float volume)
    {
        volMixer.SetFloat("Master", volume);
    }
    public void SetSfxVol(float volume)
    {
        volMixer.SetFloat("SFX", volume);
    }
    public void SetMusicVol(float volume)
    {
        volMixer.SetFloat("Music", volume);
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)//pass index from box
    {
        Resolution resolution = resolutions[resolutionIndex]; //var to hold index values at index
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
