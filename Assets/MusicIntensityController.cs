using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicIntensityController : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.StudioEventEmitter mainMenuMusic;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private CanvasController canvasController;

    private int intensityLevel;

    // Update is called once per frame
    void Update()
    {
        // Find lower hp
        if (player.hp <= canvasController.totalBuildingHealth)
        {
            intensityLevel = player.hp;
        }
        else
        {
            intensityLevel = canvasController.totalBuildingHealth;
        }

        // Apply intensity to track
        mainMenuMusic.SetParameter("Health", intensityLevel);
    }
}
