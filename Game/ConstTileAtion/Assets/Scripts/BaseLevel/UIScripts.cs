using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScripts : MonoBehaviour {

    GameMaster GameMaster;
    public GameObject[] UICanvases;

    private void Start()
    {
        CanvasReset();
    }

    //Sets all of the canvases to inactive
    public void CanvasReset()
    {
        foreach (GameObject canvas in UICanvases)
        {
            canvas.SetActive(false);
        }
    }


    public void ButtonReset()
    {


    }

    public void ButtonMainMenu()
    {

    }

    public void ButtonShop()
    {

    }


}
