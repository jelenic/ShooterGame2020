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


    public void save()
    {
        Debug.Log("SAVING");

        Equipement[] eq = EquipementManager.instance.currentlyEquiped;

        Debug.LogFormat("saving {0} equipement", eq.Length);

        int[] equipementForSaving = new int[eq.Length];
        for(int i = 0; i < eq.Length; i++)
        {
            equipementForSaving[i] = Inventory.instance.itemToId.IndexOf(eq[i]);
        }

        saveToFile(new SaveData(equipementForSaving));

    }

    private void saveToFile(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public SaveData loadFromFile()
    {
        string path = Application.persistentDataPath + "/save.data";
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
