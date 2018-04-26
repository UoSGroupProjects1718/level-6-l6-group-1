using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour
{
    public bool Next;
    public Player player;
    string PlayerFilePath;

    private void Awake()
    {
        //Sets the orientation to Portrait
        Screen.orientation = ScreenOrientation.Portrait;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {
        //Set the datapath
        PlayerFilePath = Path.Combine(Application.dataPath, "PlayerData.txt");
        LoadPlayerData();
    }

    void SavePlayerData()
    {
        if (File.Exists(PlayerFilePath))
        {
            File.Delete(PlayerFilePath);
        }
        //And then save the data, using pretty printed formatting
        string JSON = JsonUtility.ToJson(player, true);
        File.WriteAllText(PlayerFilePath, JSON);
    }

    void LoadPlayerData()
    {
        string JSON = File.ReadAllText(PlayerFilePath);
        JsonUtility.FromJsonOverwrite(JSON, player);
    }
}

//class to represent player-data 
[System.Serializable]
public class Player
{
    public int Stardust;
    public int Energy;
    public List<PlayerScores> scores;
    public int NumHints;
    public int NumExtraMoves;
    public int NumExtraSymbol;
}

//Class to represent Player's scores on levels
[System.Serializable]
public class PlayerScores
{
    public int Stars;
    public int NumberOfMovesTaken;
    public HexInfo.HexType LevelType;
    public int LevelNumber;
}

