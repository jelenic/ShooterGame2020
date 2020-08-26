using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
