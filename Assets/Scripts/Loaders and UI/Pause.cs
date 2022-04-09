using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool paused = false;
    public GameObject pausePanel;

    private PlayerController playerFunctions;

    void Start() 
    {
        Time.timeScale = 1.0f;
        playerFunctions = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update() 
    {
        if (Input.GetKeyDown("escape") && (playerFunctions.health > 0)) 
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
            playerFunctions.enabled = false;
            return true;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            playerFunctions.enabled = true;
            return false;
        }
    }
}
