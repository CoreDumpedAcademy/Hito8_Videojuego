using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public long coins;
    public float progress;
    public int gameCounter;

    public SaveData()
    {
        coins = 0;
        progress = 0;
        gameCounter = 0;
    }

    public void saveInLocal()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/testData.save");
        bf.Serialize(file, this);
        file.Close();
    }

    public SaveData getFromLocal()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/testData.save", FileMode.Open);
        SaveData save = (SaveData)bf.Deserialize(file);
        file.Close();

        return save;
    }

    /* For later
    public void saveInServer()
    {
        string json = JsonUtility.ToJson(this);
        Debug.Log(json);
        //POST in server
    }

    public SaveData getFromServer()
    {
        //Get json from server and deserialize
        return new SaveData();
    }
    */
}
