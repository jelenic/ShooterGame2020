using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    GameObject levelManager;
    CombatVariables cv;
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        cv = GetComponent<CombatVariables>();
    }

    private void OnDestroy()
    {
        if (cv.hp == 0) levelManager.GetComponent<LevelManager>().die();
    }
}
