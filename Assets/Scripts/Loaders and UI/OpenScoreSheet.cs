using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class OpenScoreSheet : MonoBehaviour
{
    public void ViewScores()
    {
        System.Diagnostics.Process.Start(Application.dataPath + "/" + "ScoreSheet.csv");    
    }
}
