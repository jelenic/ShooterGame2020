using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private int currentScore;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score:" + currentScore.ToString();
    }


    public void increaseScore(int n)
    {
        currentScore += n;

    }
}
