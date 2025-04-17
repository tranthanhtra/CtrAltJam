using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private float timer = 0f;
    private bool running;
    
    public bool IsRunning => running;
    
    public void StartTimer()
    {
        running = true;
    }

    public void SetTimer(float time)
    {
        timer = time;
        timerText.text = string.Format("{0:00}:{1:00}", (int)timer / 60, (int)timer % 60);
    }

    private void Update()
    {
        if (!running) return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
            running = false;
        }
        timerText.text = string.Format("{0:00}:{1:00}", (int)timer / 60, (int)timer % 60);
    }
}
