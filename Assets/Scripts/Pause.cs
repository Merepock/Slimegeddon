using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool paused = false;
    public GameObject pausePanel;

    void Start() 
    {
        Time.timeScale = 1.0f;
    }

    void Update() 
    {
        if (Input.GetKeyDown("escape")) 
        {
            paused = pause();
        }
    }

    bool pause() 
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
            pausePanel.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            return true;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            return false;
        }
    }
}
