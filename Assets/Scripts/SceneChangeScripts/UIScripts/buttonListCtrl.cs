using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class buttonListCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private void Start()
    {
        foreach (LevelDetails level in Levels.instance.levelDetails)
        {
            //Debug.Log("populating list of btns: " + level);
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<buttonListBtn>().setText(level.name);

            button.transform.SetParent(buttonTemplate.transform.parent, false);

            button.GetComponent<Button>().onClick.AddListener(() => loadLvl(level.name));
        }
    }

    public void loadLvl(string levelName)
    {
        Debug.Log("loading " + levelName);
        Levels.instance.loadLevel(levelName);
    }
}
