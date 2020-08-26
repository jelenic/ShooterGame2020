using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cdbar;

    private void Awake()
    {
        Invoke("lol", 0.5f);
    }

    void lol()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ModuleScript>().OnCooldownChangedCallback += change;
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ModuleScript>().OnCooldownChangedCallback -= change;
    }

    private void change(float filled)
    {
        cdbar.fillAmount = filled;
    }
}
