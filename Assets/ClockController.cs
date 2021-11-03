using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool startTimer;
    private string minutes, seconds;

    // Start is called before the first frame update
    void Start()
    {
        startTheTimer();
    }
    public void startTheTimer()
    {
        Invoke("startTime", 10);
        startTime = Time.time;
        startTimer = true;
    }
    public void stopTheTimer()
    {
        startTimer = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            float t = Time.time - startTime;
            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
    }


}
