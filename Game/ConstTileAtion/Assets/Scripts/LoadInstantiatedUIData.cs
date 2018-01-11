using System;
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
    public int MaximumMoves;

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
        //Get the text component of child number 9 (MaxMoves)
        Text MaxMoves = this.transform.GetChild(9).GetComponent<Text>();
        //Set the text element
        MaxMoves.text = MaximumMoves.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Function that is called when the "Delete" button is clicked on the overlay
    public void Delete()
    {
        //Search through all the levels
        foreach (var item in SaveAndLoadScript.AllLevels.Levels)
        {
            //For a level with the same level ID as this
            if (item.LevelNumber == LvlID)
            {
                //And delete it from the array
                SaveAndLoadScript.AllLevels.Levels.Remove(item);
                //Then re-encode the JSON file
                SaveAndLoadScript.JSONEncode();
                break;
            }
        }
    }
    //Function that is called when the "Load" button is clicked on the overlay
    public void Load()
    {
        ClearLevel();

            //First set all of the Hexes
        //Find the correct level
        foreach (var item in SaveAndLoadScript.AllLevels.Levels)
        {
            if (item.LevelNumber == LvlID)
            {
                //Search through each hex in the stored array
                foreach (var JSONHex in item.Hexes)
                {
                    //Skip comparing it if the type is Null as we don't need to find which one it corresponds to
                    if (JSONHex.HexID == HexInfo.HexType.Null)
                        continue;
                    //And compare it to each hex in the scene
                    foreach (Transform Holder in GMaster.transform)
                    {
                        foreach (Transform Child in Holder.transform)
                        {
                            //Compare its X and Y
                            if (JSONHex.X == Child.GetComponent<HexInfo>().X &&
                                JSONHex.Y == Child.GetComponent<HexInfo>().Y)
                            {
                                //And if this is the correct Hex, assign it 
                                Child.GetComponent<HexInfo>().CurrentHexType = JSONHex.HexID;
                                Child.GetComponent<HexInfo>().SetHexSprite();
                            }
                        }
                    }
                }
            }
        }
        //Then set all of the 
    }

    //Sets all the hexes in the scene to Null
    private void ClearLevel()
    {
        foreach (Transform Holder in GMaster.transform)
        {
            foreach (Transform Child in Holder.transform)
            {
                HexInfo Hex = Child.GetComponent<HexInfo>();
                Hex.CurrentHexType = HexInfo.HexType.Null;
                Hex.SetHexSprite();
            }
        }
    }
}
