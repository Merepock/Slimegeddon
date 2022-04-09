using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveScores : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerScore;
    public Text PlayerName;
    public Text SaveLabel; //The label of the save button


    void Start()
    {
        playerScore = player.GetComponent<PlayerController>();

    }

    public void SaveScoreToFile()
    {
        if (PlayerName.text != "")
        {
            string path = Application.dataPath + "/" + "ScoreSheet.txt";
           /* if (!File.Exists(path))
            {
                File.Create(path);
            } */
            StreamWriter saver = File.AppendText(path);
            File.SetAttributes(path, FileAttributes.Hidden);
            saver.WriteLine(PlayerName.text + "," + playerScore.score.ToString());
            gameObject.GetComponent<Image>().enabled = false; //Disables the button's image.
            gameObject.GetComponent<Button>().enabled = false; //Disables the interactivity of the save button while the image is invisible.
            SaveLabel.text = "Score Saved!";
            saver.Close();
            SortScores();

        }
    }

    void SortScores()
    {
        string path = Application.dataPath + "/" + "ScoreSheet.txt";
        string[] lines = File.ReadAllLines(path); //Reads through the entries of the score sheet
        List<KeyValuePair<string, int>> scores = new List<KeyValuePair<string, int>>();
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitLine = lines[i].Split(','); //Splits each line into the name and the corresponding score
            int score = Int32.Parse(splitLine[1]); //Converts the score from a string back into an integer           
            scores.Insert(0, new KeyValuePair<string, int>(splitLine[0].ToUpper(), score));
        }
        List<KeyValuePair<string, int>> entriesOrdered = scores.OrderByDescending(i => i.Value).ToList(); //Sorts the entries into descending order and appends them to a new list

        string path2 = Application.dataPath + "/" + "Leaderboard.txt";
        //File.SetAttributes(path2, FileAttributes.Normal);
        StreamWriter writer = new StreamWriter(path2);
        for (int i = 0; i < entriesOrdered.Count; i++)
        {
            writer.WriteLine(entriesOrdered[i]); //This list is written to a separate text file, which will be read to a leaderboards menu.              
        }
        writer.Flush();
        writer.Close();
        //File.SetAttributes(path2, FileAttributes.Hidden);
        
    }
}
