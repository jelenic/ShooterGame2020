using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Levels : MonoBehaviour
{
    #region LevelsManagerSingleton
    public static Levels instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of Levels found!");
        }
        instance = this;

    }
    #endregion

    public string currentlyPlayed;

    public Dictionary<string, int> scores = new Dictionary<string, int>();

    public Dictionary<string, float> difficultyModifiers = new Dictionary<string, float>();

    public List<string> levels = new List<string>();

    private string dataPath = "/lvl.data";


    private void Start()
    {
        levels.Add("DummyLevel");
        difficultyModifiers.Add("DummyLevel", 1f);

        levels.Add("DummyLevel2");
        difficultyModifiers.Add("DummyLevel2", 0.55f);
        
        levels.Add("PerformanceTestLevel");
        difficultyModifiers.Add("PerformanceTestLevel", 1.2f);

    }


    public void loadLevel(string levelName)
    {
        if (levels.Contains(levelName))
        {
            currentlyPlayed = levelName;
            Loader.Load(levelName);
        }
    }


    public int scoreLevel(int newScore)
    {
        Debug.Log("new score " + newScore);
        if (!scores.ContainsKey(currentlyPlayed) || newScore > scores[currentlyPlayed]) scores[currentlyPlayed] = newScore;
        foreach (string key in scores.Keys) Debug.LogFormat("level |{0}| score: {1}", key, scores[key]);

        SaveManager.instance.saveToFile(new SaveData(scores), dataPath);

        return scores[currentlyPlayed];

    }

    public void loadScores()
    {
        SaveData saveData = SaveManager.instance.loadFromFile(dataPath);
        Debug.Log("loading level data");
        for(int i = 0; i < saveData.levelNames.Length; i++)
        {
            Debug.Log(saveData.levelNames[i] + " : " + saveData.levelScores[i]);
            scores.Add(saveData.levelNames[i], saveData.levelScores[i]);
        }
    }

    public void finishLevel()
    {
        Loader.Load(Loader.Scene.MenuScene.ToString());
    }

    public string nextLevel()
    {
        int next = levels.IndexOf(currentlyPlayed) + 1;
        if (next == levels.Count) return "Message about next level not being available";

        currentlyPlayed = levels[next];
        Loader.Load(levels[next]);
        return "";
    }



    //public delegate void OnLevelsChanged();
    //public OnLevelsChanged OnLevelsChangedCallback;
}
