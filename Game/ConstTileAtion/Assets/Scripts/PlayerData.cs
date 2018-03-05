using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour {


    public JSONPlayerData Player = new JSONPlayerData();
    private string JSONPlayerFile;
    public int Stamina, Sign, Difficulty, Stardust;

    private void Awake()
    {
        //Sets the orientation to Portrait
        Screen.orientation = ScreenOrientation.Portrait;
        DontDestroyOnLoad(this);
    }

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
        //Set all of the variables
        Player.CurrentStamina = Stamina;
        Player.LastPlayedSign = Sign;
        Player.LastPlayedDiff = Difficulty;
        Player.Stardust = Stardust;


        string Json = JsonUtility.ToJson(Player, true);
        File.WriteAllText(JSONPlayerFile, Json);
    }


    //Load PlayerData
    void ReadPlayerData()
    {
        string JsonString = File.ReadAllText(JSONPlayerFile);
        JsonUtility.FromJsonOverwrite(JsonString, Player);

        //Set all of the values
        Stamina = Player.CurrentStamina;
        Sign = Player.LastPlayedSign;
        Difficulty = Player.LastPlayedDiff;
        Stardust = Player.Stardust;

    }
}

//Class to save Level Data
[System.Serializable]
public class JSONPlayerData
{
    public int Stardust;
    public int LastPlayedSign, LastPlayedDiff;
    public int CurrentStamina;
}