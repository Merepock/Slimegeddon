using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadScores : MonoBehaviour
{
    public Text[] topScores = new Text[15];
    
    void Update()
    {
        string path = Application.dataPath + "/" + "Leaderboard.txt";
        if (System.IO.File.Exists(path) == false)
        {
            System.IO.File.WriteAllText(path, "No scores have been saved."); //Creates the text file if it is not in the game files.
        }
        //File.SetAttributes(path, FileAttributes.Hidden);
        string[] lines = File.ReadAllLines(path);
        for (int i = 0; i < lines.Length; i++)
        {
            if(i > 14) //exits the loop when the first 15 lines in the leaderboard text file have been read.
            {
                break;
            }
            else
            {
                topScores[i].text = lines[i];
            }
        }
    }
}
