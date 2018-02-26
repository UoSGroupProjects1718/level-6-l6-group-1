using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Overworld : MonoBehaviour {

    public GameObject Persistant, LevelLollipop;
    public GameObject[] OverworldSigns;
    public GameObject MainMenu, LevelSelect;

    //The logical order the levels should be played through in
    public int[] Levels;
	// Use this for initialization
	void Start ()
    {
        Persistant = GameObject.Find("PersistantObject");
        FindLevels();
	}

    //Function to find all levels and LVL ID's associated with star sign
    public void FindLevels()
    {
        //Load all the levels
        JSONLevel AllLevels = new JSONLevel();

        //Find and create the correct path to the JSON file holding the levels
        TextAsset JSONText = Resources.Load("Levels") as TextAsset;
        JsonUtility.FromJsonOverwrite(JSONText.ToString(), AllLevels);

        //Search through each of the levels to find how many there are of each
        for (int i = 0; i < 12; i++)
        {
            foreach (var Level in AllLevels.Levels)
            {
                //Compare the leveltype being looked at to the one we are trying to find
                if ((int)Level.Leveltype == i)
                {

                }
            }
        }



    }

    public void ButtonLevelShower(GameObject LevelHolder)
    {
        LevelHolder.SetActive(true);
        
    }

    public void ButtonContinue()
    {
        //SetLevelID(LevelToLoad);

    }

    //Shows the level select menu or the main menu
    public void ButtonLevelSelect()
    {
        MainMenu.SetActive(!MainMenu.activeSelf);
        LevelSelect.SetActive(!MainMenu.activeSelf);
    }



}
