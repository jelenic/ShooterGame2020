using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTexts : MonoBehaviour
{
    public Text levelComplete;
    public Text levelScore;
    public Text levelHighScore;
    // Start is called before the first frame update


    public void completed(string levelName)
    {
        levelComplete.text = levelName + " completed!";
    }

    public void scored(int score)
    {
        levelScore.text = "SCORE: " + score.ToString();
    }
    
    public void displayhighscore(int highscore)
    {
        levelHighScore.text = "HIGHSCORE: " + highscore.ToString();
    }
}
