using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour {

    GameMaster GameMaster;
    public GameObject[] UICanvases;
    public GameMaster GM;
    public GameObject MainMenu, LoseMenu, WinMenu;

    private void Start()
    {
        CanvasReset();
    }

    //Sets all of the canvases to inactive
    public void CanvasReset()
    {
        GM.DisableHexes = false;
        foreach (GameObject canvas in UICanvases)
        {
            canvas.SetActive(false);
        }
    }


    public void ButtonReset()
    {
        CanvasReset();
        GM.LoadLevel();
    }

    //Button to take the user back to the overworld
    public void ButtonMainMenu()
    {
        SceneManager.LoadScene("Overworld");
    }

    //Button to open up the main menu canvas
    public void ButtonMenu()
    {
        GM.DisableHexes = true;
        MainMenu.SetActive(true);

    }

    public void ButtonContinue()
    {
        CanvasReset();
    } 


    public void ButtonShop()
    {

    }

    public void ButtonNextLevel()
    {
        //Get the persistant info
        PersistantInfo PInfo = GameObject.Find("PersistantObject").GetComponent<PersistantInfo>();
        //Increase the level by 1
        PInfo.LevelType++;
        //Reset the canvases
        CanvasReset();
        //Reset the level to the new level
        GM.ResetLevel();
    }

}
