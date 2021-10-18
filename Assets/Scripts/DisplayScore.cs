using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text DisplayText; //Object reference to the text that reads "score shown here" in the game over panel
    public GameObject player; //Reference that the player object holds the script.
    private PlayerController playerScore;

    void Start()
    {
        playerScore = player.GetComponent<PlayerController>();
        
    }

    void Update()
    {
        DisplayText.text = playerScore.score.ToString() + " points."; //Shows the amount of points to the player on the panel.
    }
}
