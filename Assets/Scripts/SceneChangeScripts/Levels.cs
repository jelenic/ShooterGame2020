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

        int i = 0;
        foreach(LevelDetails l in levelDetails)
        {
            _levelDetails.Add(l.name, i++);
        }

    }
    #endregion

    private void Start()
    {
        audioManager = AudioManager.instance;

    }

    public LevelDetails[] levelDetails;
    private Dictionary<string, int> _levelDetails = new Dictionary<string, int>();

    public string currentlyPlayed;

    public Dictionary<string, int> scores = new Dictionary<string, int>();


    private string dataPath = "/lvl.data";

    public AudioManager audioManager;


    public LevelDetails getCurrentLevelDetails()
    {
        return levelDetails[_levelDetails[currentlyPlayed]];
    }



    public void loadLevel(string levelName)
    {
        if (_levelDetails.ContainsKey(levelName))
        {
            currentlyPlayed = levelName;
            Loader.Load(levelName);
            audioManager.PlayMusic(getCurrentLevelDetails().music_name);


        }
    }

    public void restartLevel()
    {
        Debug.Log("restarting " + currentlyPlayed);
        loadLevel(currentlyPlayed);
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
            scores.Add(saveData.levelNames[i], saveData.levelScores[i]);
        }
    }

    public void finishLevel()
    {
        Loader.Load(Loader.Scene.MenuScene.ToString());
    }

    public string nextLevel()
    {
        int next = _levelDetails[currentlyPlayed] + 1;
        if (next == levelDetails.Length) return "Message about next level not being available";

        currentlyPlayed = levelDetails[next].name;
        Loader.Load(currentlyPlayed);
        audioManager.PlayMusic(getCurrentLevelDetails().music_name);

        return "";
    }
}
