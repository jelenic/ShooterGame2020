using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTexts : MonoBehaviour
{
    public Text levelComplete;
    public Text levelScore;
    // Start is called before the first frame update


    public void completed(string levelName)
    {
        levelComplete.text = levelName + " completed!";
    }

    public void scored(int score)
    {
        levelScore.text = "Score: " + score.ToString();
    }
}
