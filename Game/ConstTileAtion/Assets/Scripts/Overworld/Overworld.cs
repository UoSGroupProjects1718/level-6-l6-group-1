using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Overworld : MonoBehaviour {

    public GameObject Persistant;
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
    }

    public void ButtonLevelShower(GameObject LevelHolder)
    {
        LevelHolder.SetActive(true);
        
    }

    public void ButtonContinue()
    {

        SceneManager.LoadSceneAsync("Base Level");

    }

    //Shows the level select menu or the main menu
    public void ButtonLevelSelect()
    {
        MainMenu.SetActive(!MainMenu.activeSelf);
        LevelSelect.SetActive(!MainMenu.activeSelf);
    }



}
