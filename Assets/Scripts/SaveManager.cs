using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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


    public void saveToFile(SaveData saveData, string location)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + location;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public SaveData loadFromFile(string location)
    {
        string path = Application.persistentDataPath + location;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData ;
            stream.Close();
            return saveData;

        }
        else
        {
            Debug.LogError("no file at " + path);
            return null;
        }
    }

  

}
