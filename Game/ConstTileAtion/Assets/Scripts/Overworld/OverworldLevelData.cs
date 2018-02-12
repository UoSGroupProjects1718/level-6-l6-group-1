using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldLevelData : MonoBehaviour {

    public int LevelToLoad;
    GameObject PersistantInfo;



    public void LoadLevel()
    {
        GameObject.Find("PersistantObject").GetComponent<PersistantInfo>().LevelType = LevelToLoad;
        SceneManager.LoadScene("Base Level");
    }
}
