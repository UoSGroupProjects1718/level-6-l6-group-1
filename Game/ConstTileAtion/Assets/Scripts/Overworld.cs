using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overworld : MonoBehaviour {

    public GameObject Persistant, LevelLollipop;
    public GameObject[] OverworldSigns;

	// Use this for initialization
	void Start ()
    {
        Persistant = GameObject.Find("PersistantObject");
        FindLevels();

	}

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

        //Search through each of the levels to find how there are of each
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
        }



    }

    public void InstantiateLevelSelectors(int LevelID, GameObject Sign)
    {
        GameObject Level = Instantiate(LevelLollipop, Sign.transform);
        Level.GetComponent<OverworldLevelData>().LevelToLoad = LevelID;

    } 
    
    //Function to spread the levels equally around the circle
    public void SpreadLevels(GameObject Sign)
    {
        //Work out how far apart the signs should be
        float Spacing = 360 / Sign.transform.childCount;
        float Rotation = 0f;
        foreach (Transform Child in Sign.transform)
        {
            Vector3 FinalRotation = new Vector3(0, 0, Rotation);
            Child.transform.Rotate(FinalRotation);
            Rotation += Spacing;
        }
    }

}
