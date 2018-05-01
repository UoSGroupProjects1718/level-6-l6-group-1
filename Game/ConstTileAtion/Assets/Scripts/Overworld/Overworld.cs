using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Overworld : MonoBehaviour {

    public GameObject Persistant;
    //public GameObject[] OverworldSigns;
    public List<GameObject> Canvases = new List<GameObject>();
    PlayerData playerData;

    public GameObject LevelScrollViewContent;
    public GameObject LevelPrefab;

    JSONLevel AllLevels = new JSONLevel();

    // Use this for initialization
    void Start ()
    {
        Persistant = GameObject.Find("PersistantObject");
        playerData = Persistant.GetComponent<PlayerData>();
        FindLevels();
	}

    //Function to find all levels and LVL ID's associated with star sign
    public void FindLevels()
    {
        //Find and create the correct path to the JSON file holding the levels
        TextAsset JSONText = Resources.Load("Levels") as TextAsset;
        JsonUtility.FromJsonOverwrite(JSONText.ToString(), AllLevels);
    }

    public void InstantiateLevels(int type)
    {
        //Search through all the levels and instantiate them into the game
        foreach (var level in AllLevels.Levels)
        {
            //Initialise state to overwrite later
            LevelTransitionScript.LevelStates state = LevelTransitionScript.LevelStates.Locked;

            //Check it is the correct type
            if (level.Leveltype == (HexInfo.HexType)type)
            {
                foreach (var item in playerData.player.scores)
                {
                    //Check they are the same type
                    if (item.LevelType == level.Leveltype)
                    {
                        //Check they are the same level number
                        if (item.LevelNumber == level.LevelNumber)
                        {
                            //If we find the information we need then assign it and break out of the foreach
                            state = item.state;
                            break;
                        }
                    }
                }
                //If the level is 0 and is locked, unlock it
                if (level.LevelNumber == 0 && state == LevelTransitionScript.LevelStates.Locked)
                {
                    state = LevelTransitionScript.LevelStates.Unlocked;
                }
                //instantiate new level
                GameObject newLevelObject = Instantiate(LevelPrefab);
                newLevelObject.GetComponent<LevelTransitionScript>().Initialise(state, level.LevelNumber, type);
                newLevelObject.transform.parent = LevelScrollViewContent.transform;
            }

        }
    }



    public void ButtonLevelShower(GameObject LevelHolder)
    {
        LevelHolder.SetActive(true);
        
    }

    public void ButtonContinue()
    {
        SceneManager.LoadSceneAsync("Base Level");
    }


    //Shows the correct canvas
    public void ButtonLevelSelect(GameObject CanvasToShow)
    {
        //Disable all of the canvases
        foreach (var item in Canvases)
        {
            item.SetActive(false);
        }
        GameObject CanvasToExamine = CanvasToShow;
        //Search through all of the parents of the canvas to be enabled and enable them
        while (CanvasToExamine.transform.parent != null && CanvasToExamine.transform.parent.GetComponent<Canvas>() != null )
        {
            //Set the parent to enabled
            CanvasToExamine.transform.parent.gameObject.SetActive(true);
            //Then set it as the next thing to examine the parent of
            CanvasToExamine = CanvasToExamine.transform.parent.gameObject;
        }
        CanvasToShow.SetActive(true);
    }

}
