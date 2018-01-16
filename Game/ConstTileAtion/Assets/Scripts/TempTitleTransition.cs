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
    public int LevelToStartID;
    public GameObject Persistant;

    //Awake runs once before anything else
    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        //Clear the options currently in the dropdown menu
        LevelSelect.options.Clear();
        
        JsonUtility.FromJsonOverwrite(ReadJSONText(), AllLevels);

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

    private string ReadJSONText()
    {

        TextAsset JSONText = Resources.Load("Levels") as TextAsset;
        return JSONText.ToString();
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
