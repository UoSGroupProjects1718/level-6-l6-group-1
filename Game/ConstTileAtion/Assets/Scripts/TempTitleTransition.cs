using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class TempTitleTransition : MonoBehaviour
{
    private JSONLevel AllLevels = new JSONLevel();
    public Dropdown LevelSelect;
    private string JSONFilePath;
    public int LevelToStartID;
    public GameObject Persistant;

	// Use this for initialization
	void Start ()
    {
        //Clear the options currently in the dropdown menu
        LevelSelect.options.Clear();

        JSONFilePath = Path.Combine(Application.dataPath, "Levels.txt");
        string JsonString = File.ReadAllText(JSONFilePath);
        JsonUtility.FromJsonOverwrite(JsonString, AllLevels);

        List<string> LevelNames = new List<string>();

        foreach (var Level in AllLevels.Levels)
        {
            LevelNames.Add(Level.LevelName);
        }
        //Add the options to the dropdown
        LevelSelect.AddOptions(LevelNames);
        //Refresh the dropdown menu to show the values
        LevelSelect.RefreshShownValue();
    }

    public void LevelSelected()
    {
        string LevelName = LevelSelect.options[LevelSelect.value].text;

        foreach (var Level in AllLevels.Levels)
        {
            if (LevelName == Level.LevelName)
            {
                LevelToStartID = Level.LevelNumber;
                return;
            }
        }
    }

    public void StartGame()
    {
        Persistant.GetComponent<PersistantInfo>().LevelType = LevelToStartID;
        SceneManager.LoadSceneAsync("Base Level");
    }
}
