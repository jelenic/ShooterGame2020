using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region SaveManagerSingleton
    public static SaveManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("more than one instance of SaveManager found!");
        }
        instance = this;

    }
    #endregion


    public void save()
    {

    }

    public void load()
    {

    }
}
