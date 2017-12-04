﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TestSaveandLoadScript : MonoBehaviour
{
    public JSONLevel AllLevels = new JSONLevel();

    private string JSONFilePath;

    public GameMaster GMaster;
    public InputField InputName;
    public Dropdown InputType;
    public Text SaveErrorText;



    //On start, load the level-data that is currently saved
    void Start()
    {
        //Create a string to the JSON text file
        JSONFilePath = Path.Combine(Application.dataPath, "Levels.txt");
        //At the start, create a version of the JSON file in memory and populate it
        LoadLevelJSONFile();
    }

    private void LoadLevelJSONFile()
    {
        string JsonString = File.ReadAllText(JSONFilePath);
        JsonUtility.FromJsonOverwrite(JsonString, AllLevels);
    }

    public void SaveLevel()
    {
        //Ceate a new level and add the correct data to it
        LevelData LVLData = new LevelData();
        LVLData.LevelName = InputName.text;
        LVLData.Leveltype = (LevelType)InputType.value;
        LVLData.LevelNumber = FindClearID();

        //run the function to add the hex-data to the level
        SaveHexes(LVLData);
        //Add the Level to the list inside the JSON object
        AllLevels.Levels.Add(LVLData);

        //Check if the Save file exists, and if it does then delete it
        if (File.Exists(JSONFilePath))
        {
            File.Delete(JSONFilePath);
        }

        //Save the level to a JSON string
        string json = JsonUtility.ToJson(AllLevels, true);
        //And then write it to an actual textfile
        File.WriteAllText(JSONFilePath, json);

        SaveErrorText.text = "Level Saved!";
        Debug.Log("Created JSON text file at" + JSONFilePath);
    }

    //finds an ID that is not being used, in case one has been deleted in the past
    private int FindClearID()
    {
        int CurrentInt = 0;
        foreach (var item in AllLevels.Levels)
        {
            if (item.LevelNumber == CurrentInt)
            {
                CurrentInt++;
                continue;
            }
            else
            {
                return CurrentInt;
            }
        }
        //If the foreach loop has searched all of the objects then they are taken, so give it an
        //  ID equal to the number of objects there are +1
        return CurrentInt++;
    }

    //Loads a specific level from the JSON list
    public bool LoadLevel(int LevelToLoadID)
    {
        //Call function to find out which level we should be loading
        LevelData LoadedLevel = FindLevel(LevelToLoadID);
        if ( LoadedLevel == null)
            return false;


        return true;
    }


    private LevelData FindLevel(int LevelToLoadID)
    {
        //Find the correct level ID
        foreach (var item in AllLevels.Levels)
        {
            if (item.LevelNumber == LevelToLoadID)
            {
                return item;
            }
        }
        Debug.Log("Level ID does not exist");
        return null;
    }

    public void SaveHexes(LevelData Level)
    {
        foreach (Transform Holder in GMaster.transform)
        {
            foreach (Transform Child in Holder.transform)
            {
                //Create a new hex object to be stored in the List
                HexData Hex = new HexData();
                //Get the data from the hex we are currently looking at
                HexInfo HexInfoScript = Child.GetComponent<HexInfo>();

                //Put the data into the object
                Hex.X = HexInfoScript.X;
                Hex.Y = HexInfoScript.Y;
                Hex.HexID = HexInfoScript.CurrentHexType;

                //Add the hex to the list
                Level.Hexes.Add(Hex);
            }
        }
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


//Class to store the data of all hexes
[System.Serializable]
public class HexData
{
    public int X, Y;
    public HexInfo.HexType HexID;
}

//Public class to hold all level data, including a list of hexes  
[System.Serializable]
public class LevelData
{
    public string LevelName;
    public LevelType Leveltype;
    public int LevelNumber;
    //List to hold all hex data for this level
    public List<HexData> Hexes = new List<HexData>();
}
//Public class that holds all levels ready for serialization and saving to JSON
[System.Serializable]
public class JSONLevel
{
    //List of level-data
    public List<LevelData> Levels = new List<LevelData>();
}
