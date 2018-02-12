using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    public GameObject Persistant;
    public Scene Level;


    public void Aries()
    {
        Persistant.GetComponent<PersistantInfo>().LevelType = 0;
        LoadScene();
    }

    public void Taurus()
    {
        Persistant.GetComponent<PersistantInfo>().LevelType = 1;
        LoadScene();
    }

    public void Gemini()
    {
        Persistant.GetComponent<PersistantInfo>().LevelType = 2;
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("Base Level");
    }
}
