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
    //The logical order the levels should be played through in
    public int[] Levels;
	// Use this for initialization
	void Start ()
    {
        Persistant = GameObject.Find("PersistantObject");
        FindLevels();
	}

    //Sets the Level ID correctly in the lollipop
    public void SetLevelID(int LevelID)
    {
        Persistant.GetComponent<PersistantInfo>().LevelType = LevelID;
        SceneManager.LoadScene("Base Level");
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
                    InstantiateLevelSelectors(Level.LevelNumber, OverworldSigns[i]);

                }
            }
            SpreadLevels(OverworldSigns[i]);
            OverworldSigns[i].SetActive(false);
        }



    }

    //Instantiates the lollipops where they need to be
    public void InstantiateLevelSelectors(int LevelID, GameObject Sign)
    {
        GameObject Level = Instantiate(LevelLollipop, Sign.transform);
        Level.GetComponent<OverworldLevelData>().LevelToLoad = LevelID;

    } 
    
    //Function to spread the levels equally around the circle
    public void SpreadLevels(GameObject Sign)
    {
        float Spacing = 0;
        if (Sign.transform.childCount != 0)
        {
            Spacing = 360 / Sign.transform.childCount;
        }
        //Work out how far apart the signs should be
         
        float Rotation = 0f;
        foreach (Transform Child in Sign.transform)
        {
            Vector3 FinalRotation = new Vector3(0, 0, Rotation);
            Child.transform.Rotate(FinalRotation);
            Rotation += Spacing;
        }
    }

    public void ButtonLevelShower(GameObject LevelHolder)
    {
        LevelHolder.SetActive(true);
        Debug.Log("Taurus Pressed");
        
    }

    public void ButtonContinue()
    {
        //Find out the level we need to load
        int LevelToLoad = Persistant.GetComponent<PlayerData>().Player.CurrentLevel;
        //And then load it
        SetLevelID(LevelToLoad);

    } 


}
