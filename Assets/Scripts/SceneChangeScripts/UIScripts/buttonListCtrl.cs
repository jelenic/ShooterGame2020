using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class buttonListCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    private List<string> lvl;

    private void Start()
    {
        lvl = Levels.instance.levels;
        foreach (string level in lvl)
        {
            //Debug.Log("populating list of btns: " + level);
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<buttonListBtn>().setText(level);

            button.transform.SetParent(buttonTemplate.transform.parent, false);

            button.GetComponent<Button>().onClick.AddListener(() => loadLvl(level));
        }
    }

    public void loadLvl(string sceneName)
    {
        Debug.Log("loading " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
