using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

public class ManagerBaseData : Singleton<ManagerBaseData>
{
    public bool Load(string savePath, MonoBehaviour saveObject)
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), saveObject);
            file.Close();

            Debug.Log("load " + saveObject.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Load(string savePath, ScriptableObject saveObject)
    {

        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), saveObject);
            file.Close();

            Debug.Log("load " + saveObject.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Save(string savePath, MonoBehaviour saveObject)
    {
        string saveData = JsonUtility.ToJson(saveObject, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("save " + saveObject.name);
    }

    public void Save(string savePath, ScriptableObject saveObject)
    {
        string saveData = JsonUtility.ToJson(saveObject, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("save " + saveObject.name);
    }
}
