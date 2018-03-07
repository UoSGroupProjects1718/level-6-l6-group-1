﻿using System.Collections;
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
        GM.ResetLevel();
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
        //Change the player's current level to be equal to the one they just won
        PlayerPrefs.SetInt("Difficulty", PlayerPrefs.GetInt("Difficulty")+1);
        //Reset the canvases
        CanvasReset();
        //Reset the level to the new level
        GM.LoadLevel(false);
    }

}
