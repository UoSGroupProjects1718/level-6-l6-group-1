using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour
{
    public bool Next;

    private void Awake()
    {
        //Sets the orientation to Portrait
        Screen.orientation = ScreenOrientation.Portrait;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {


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

