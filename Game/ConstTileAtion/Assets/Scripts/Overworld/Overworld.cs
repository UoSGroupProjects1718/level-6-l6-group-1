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
