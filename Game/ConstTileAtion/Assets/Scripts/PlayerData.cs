using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour {


    public JSONPlayerData Player = new JSONPlayerData();
    private string JSONPlayerFile;

    // Use this for initialization
    void Start ()
    {
        JSONPlayerFile = Path.Combine(Application.dataPath, "Resources\\PlayerData.txt");
        ReadPlayerData();
	}

    void WritePlayerData()
    {
        if (File.Exists(JSONPlayerFile))
        {
            File.Delete(JSONPlayerFile);
        }
        string Json = JsonUtility.ToJson(Player, true);
        File.WriteAllText(JSONPlayerFile, Json);
    }


    //Load PlayerData
    void ReadPlayerData()
    {
        string JsonString = File.ReadAllText(JSONPlayerFile);
        JsonUtility.FromJsonOverwrite(JsonString, Player);
    }
}

//Class to save Level Data
[System.Serializable]
public class JSONPlayerData
{
    public int Stardust;
    public int LastPlayedSign, LastPlayedDiff;
    public int CurrentLives;
}