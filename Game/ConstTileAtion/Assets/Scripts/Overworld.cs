using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overworld : MonoBehaviour {

    GameObject Persistant;


	// Use this for initialization
	void Start ()
    {
        Persistant = GameObject.Find("PersistantObject");
	}

    public void SetLevelID(int LevelID)
    {
        Persistant.GetComponent<PersistantInfo>().LevelType = LevelID;
        SceneManager.LoadScene("Base Level");
    }
}
