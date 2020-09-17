using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    LevelManager levelManager;
    CombatVariables cv;
    void Start()
    {
        levelManager = LevelManager.instance;
        cv = GetComponent<CombatVariables>();
    }

    private void OnDestroy()
    {
        if (cv.hp == 0) levelManager.die();
    }
}
