using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTexts : MonoBehaviour
{
    public TextMeshProUGUI levelComplete;
    public TextMeshProUGUI levelScore;
    public TextMeshProUGUI levelHighScore;
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
