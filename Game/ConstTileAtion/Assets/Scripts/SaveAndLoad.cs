﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class SaveAndLoad : MonoBehaviour
{
   
    public JSONLevel AllLevels = new JSONLevel();

    
    private string JSONFilePath;
    public GameObject OverlayParent;
    public GameMaster GMaster;
    public InputField InputName, MaxMovesInput, StardustRewards;
    public Dropdown InputType, BackgroundDropdown, DifficultyDropdown;
    public Text SaveErrorText;
    public PlayerData Player;
    public LevelData CurrentLevel;
    
    //On start, load the level-data that is currently saved
    void Awake()
    {
        Debug.Log("SaveAndLoad Awake");
        if (!GMaster.EditMode)
        {
            Player = GameObject.Find("PersistantObject").GetComponent<PlayerData>();
        }

        //If its the editor, load it in such a way that it can be edited at runtime
        if (Application.isEditor)
        {
            //At the start, create a version of the JSON file in memory and populate it
            JSONFilePath = Path.Combine(Application.dataPath, "Resources\\Levels.txt");
            string JsonString = File.ReadAllText(JSONFilePath);
            JsonUtility.FromJsonOverwrite(JsonString, AllLevels);
        }
        //Otherwise, just read the data into memory
        else
        {
            TextAsset JSONText = Resources.Load("Levels") as TextAsset;
            JsonUtility.FromJsonOverwrite(JSONText.ToString(), AllLevels);
            GMaster.LoadLevel(false);
        }
    }
    
    public void ResetGame()
    {
        //Loop through all current hexes and set them to Null
        foreach (Transform Ring in GMaster.transform)
        {
            foreach (Transform HexMarker in Ring.transform)
            {
                HexInfo hexInfo = HexMarker.GetComponent<HexInfo>();
                hexInfo.CurrentHexType = HexInfo.HexType.Null;
                hexInfo.SpriteChanger();

            }
        }
    }
    
    public void SaveLevel()
    {
        //Create a new level and add the correct data to it
        LevelData LVLData = new LevelData();
        //Search all levels to find out if it is identical to another level
        foreach (var item in AllLevels.Levels)
        {
            if (LVLData.LevelName == item.LevelName)
            {
                Debug.Log("Name Already taken");
                return;
            }
        }

        //Set the various variables in the level class based off data given to it
        LVLData.LevelName = InputName.text;
        LVLData.Leveltype = (HexInfo.HexType)InputType.value;
        LVLData.LevelNumber = FindClearID();
        LVLData.HexLayers = GMaster.LayersBeingUsed;
        LVLData.MaximumMoves = Convert.ToInt32(MaxMovesInput.text);
        LVLData.Background = BackgroundDropdown.value;
        LVLData.StardustRewardForLevel = Convert.ToInt32(StardustRewards.text);
        LVLData.Difficulty = DifficultyDropdown.value;

        //run the function to add the hex-data to the level
        SaveHexes(LVLData);
        //Add the Level to the list inside the JSON object
        AllLevels.Levels.Add(LVLData);

        JSONEncode();

        SaveErrorText.text = "Level Saved!";
    }

    public void JSONEncode()
    {
        //Check if the Save file exists, and if it does then delete it
        if (File.Exists(JSONFilePath))
        {
            File.Delete(JSONFilePath);
        }

        //Save the level to a JSON string
        string json = JsonUtility.ToJson(AllLevels, true);
        //And then write it to an actual textfile
        File.WriteAllText(JSONFilePath, json);
    }

    //finds an ID that is not being used, in case one has been deleted in the past
    private int FindClearID()
    {
        int CurrentInt = 0;
        foreach (var item in AllLevels.Levels)
        {
            if (item.LevelNumber > CurrentInt)
            {
                CurrentInt = item.LevelNumber;
            }
        }
        //If the foreach loop has searched all of the objects then they are taken, so give it an
        //  ID equal to the number of objects there are +1
        return CurrentInt+1;
    }

    //Loads a specific level from the JSON list
    public bool LoadLevel(HexInfo.HexType LevelType, int LevelDifficulty)
    {
        CurrentLevel = NextLevel(LevelType, LevelDifficulty);
        
        if (CurrentLevel == null)
            return false;

        //Reset the NumToWin - bugfix
        GMaster.NumToWin = 0;
        //Destroy all of the remaining highlights
        foreach (Transform item in OverlayParent.transform)
        {
            Destroy(item.gameObject);
        }

        //Load all of the Hexes
        foreach (HexData Hex in CurrentLevel.Hexes)
        {
            HexFinder(Hex);
        }
        //Set the number of Hex Layers that are going to be used
        GMaster.GetComponent<GameMaster>().LayersBeingUsed = CurrentLevel.HexLayers;
        //Call LayerSetter to put those changes into effect
        GMaster.GetComponent<GameMaster>().LayerSetter();

        //Set the MovesLeft variable in the gamemaster
        GMaster.MovesLeft = CurrentLevel.MaximumMoves;
        //Update the moves counter, so that it sets it correctly on the GUI
        GMaster.UpdateMoveCounter(0);

        GMaster.LevelTitle.text = CurrentLevel.LevelName;

        //Set the current background
        SetBackground(CurrentLevel.Background);

        return true;
    }

    private void SetBackground(int BackgroundID)
    {
        GameObject BCObject = GameObject.Find("BackgroundController");
        BackgroundController BCScript = BCObject.GetComponent<BackgroundController>();
        BCScript.ChangeBackground(BackgroundID);
    }

    //Make sure you ask for a difficulty equal to the current or one higher
    private LevelData NextLevel(HexInfo.HexType Type, int Diff)
    {
        //While we don't have a level 
        while (true)
        {
            //Search all the levels for one that matches what we want
            foreach (var Level in AllLevels.Levels)
            {
                if (Level.Leveltype == Type && Level.Difficulty == Diff)
                {
                    return Level;
                }
            }
            //If we haven't found a level yet, increment the difficulty as long as the difficulty isn't too high
            if (Diff < 3)
                Diff++;
            //If the difficulty is 3 or more, we need to reset the Difficulty and move on to the next level type
            else
            {
                Diff = 0;
                Type++;
            }
            //Make sure we bail out if we start looking at Null levels
            if (Type == HexInfo.HexType.Null)
            {
                return null;
            }
        }
    }

    //Find one specific level
    private LevelData FindIndividualLevel(HexInfo.HexType LevelType, int LevelDiff)
    {
        //Find a level which matches all of the criteria
        foreach (var item in AllLevels.Levels)
        {
            //If the level is of the correct type, and the correct difficulty
            if (item.Leveltype == LevelType && item.Difficulty == LevelDiff)
            {
                return item;
            }
        }
        Debug.Log("Level ID does not exist");
        return null;
    }

    //Find a hex that coresponds to the one stored in JSON
    private void HexFinder(HexData Hex)
    {
        foreach (Transform Ring in GMaster.transform)
        {
            foreach (Transform HexMarker in Ring.transform)
            {
                if (HexMarker.GetComponent<HexInfo>().X == Hex.X &&
                    HexMarker.GetComponent<HexInfo>().Y == Hex.Y)
                {
                    HexInfo HexScript = HexMarker.GetComponent<HexInfo>();
                    HexScript.CurrentHexType = Hex.HexID;
                    HexScript.SpriteChanger();

                    //If the hex loaded in is not a null hex, increase the number needed to win
                    if (Hex.HexID != HexInfo.HexType.Null)
                    {
                        GMaster.NumToWin++;
                    }
                }
            }
        }
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
                //Only save the hex if it is going to be seen, prevents impossible levels
                if (HexInfoScript.Layer <= Level.HexLayers)
                {
                    Hex.HexID = HexInfoScript.CurrentHexType;
                }
                else
                {
                    Hex.HexID = HexInfo.HexType.Null;
                }
                //Add the hex to the list
                Level.Hexes.Add(Hex);
            }
        }
    }    
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
    public HexInfo.HexType Leveltype;
    public int LevelNumber;
    public int HexLayers;
    public int MaximumMoves;
    public int Background;
    public int StardustRewardForLevel;
    public int StarsEarned;
    public int Difficulty;
    public int OptimalMoves;
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
