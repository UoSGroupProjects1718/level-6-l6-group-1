using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadInstantiatedUIData : MonoBehaviour {

    public GameObject GMaster;
    public TestSaveandLoadScript SaveAndLoadScript;
    public string LvlName;
    public int LvlID;
    public string LvlType;

	// Use this for initialization
	void Start ()
    {
            //Find the GameMaster script
        GMaster = GameObject.Find("HexBaseHolder");
        SaveAndLoadScript = GMaster.GetComponent<TestSaveandLoadScript>();

            //Set the text values
        //Get the text component of child number 3 (LevelName)
        Text NameNo = this.transform.GetChild(3).GetComponent<Text>();
        //Set the text element
        NameNo.text = LvlName;
        //Get the text component of child number 5 (LevelNumber)
        Text IDNo = this.transform.GetChild(5).GetComponent<Text>();
        //Set the text element
        IDNo.text = LvlID.ToString();
        //Get the text component of child number 7 (LevelName)
        Text LevelNo = this.transform.GetChild(7).GetComponent<Text>();
        //Set the text element
        LevelNo.text = LvlType;

            //Set the button controls
        //Set the load button

        //set the delete button

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Function that is called when the "Delete" button is clicked on the overlay
    void Delete()
    {
        //Search through all the levels
        foreach (var item in SaveAndLoadScript.AllLevels.Levels)
        {
            //For a level with the same level ID as this
            if (item.LevelNumber == LvlID)
            {
                //And delete it

            }
        }
    }
    //Function that is called when the "Load" button is clicked on the overlay
    void Load()
    {

    }

}
