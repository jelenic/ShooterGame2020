using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public int sceneToLoad;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (sceneToLoad == 0) sceneToLoad = 1;
        SceneManager.LoadScene(sceneToLoad);
    }

}
