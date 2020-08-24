using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    public List<string> levels = new List<string>();


    private void Start()
    {
        levels.Add("DummyLevel");
        levels.Add("DummyLevel2");
    }


    public void loadLevel(string levelName)
    {
        if (levels.Contains(levelName))
        {
            currentlyPlayed = levelName;
            Loader.Load(levelName);
        }
    }


    public void scoreLevel(int newScore)
    {
        Debug.Log("new score " + newScore);
        if (!scores.ContainsKey(currentlyPlayed) || newScore > scores[currentlyPlayed]) scores[currentlyPlayed] = newScore;
        foreach (string key in scores.Keys) Debug.LogFormat("level {0} score: {1}", key, scores[key]);

    }

    public void finishLevel()
    {
        Loader.Load(Loader.Scene.MenuScene.ToString());
        currentlyPlayed = "";
    }



    //public delegate void OnLevelsChanged();
    //public OnLevelsChanged OnLevelsChangedCallback;
}
