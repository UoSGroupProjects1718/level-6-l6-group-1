using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveandLoadScript : MonoBehaviour
{



    public bool SaveLevel(string Name)
    {

        return true;
    }
}

public enum LevelType
{
    Aries,
    Taurus,
    Gemini,
    Cancer,
    Leo,
    Virgo,
    Libra,
    Scorpio,
    Sagittarius,
    Capricorn,
    Aquarius,
    Pisces,
    Null
}


//Class to store the different types of 
public class HexData
{
    public int X, Y;
    public HexInfo.HexType HexID;
}

public class LevelData
{
    public string LevelName;
    public LevelType Leveltype;
    public int Level;

}

public class JSONLevels
{
        Dictionary<string, LevelData> Levels = new Dictionary<string, LevelData>();
}
