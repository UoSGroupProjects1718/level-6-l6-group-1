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
    private string JSONFilePath;
    public int LevelToStartID;
    public GameObject Persistant;

	// Use this for initialization
	void Start ()
    {

    }

    public void StartGame()
    {
        Persistant.GetComponent<PlayerData>().Sign = LevelToStartID;
        SceneManager.LoadSceneAsync("Base Level");
    }
}
