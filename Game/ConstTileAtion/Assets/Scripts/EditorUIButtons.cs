using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//http://benhumphreys.co.uk/blog/2015/09/06/how-to-make-a-scrollable-list-in-unitys-new-gui/

public class EditorUIButtons : MonoBehaviour
{
    public Canvas SaveCanvas;
    public GameObject LoadObject;
    public Canvas LoadCanvas;
    public GameObject GMaster;
    public GameMaster GMScript;
    public GameObject LoadUI;

    private void Start()
    {
        GMScript = GMaster.GetComponent<GameMaster>();
        SaveCanvas.gameObject.SetActive(false); //Sets the canvas to false at the start of the play
        LoadCanvas.gameObject.SetActive(false);
    }

    //
    public void SaveCanvasToggle()
    {
        //Flick the SaveCanvas to whatever it wasn't before
        SaveCanvas.gameObject.SetActive(!SaveCanvas.gameObject.activeSelf);
        GMScript.DisableHexes = !GMScript.DisableHexes;
        if (LoadCanvas.gameObject.activeSelf)
        {
            LoadCanvas.gameObject.SetActive(false);
        }
    }

    public void LoadCanvasToggle()
    {
        //Flick the SaveCanvas to whatever it wasn't before
        LoadCanvas.gameObject.SetActive(!LoadCanvas.gameObject.activeSelf);
        GMScript.DisableHexes = !GMScript.DisableHexes;
        //Set the Save canvas to 
        if (SaveCanvas.gameObject.activeSelf)
        {
            SaveCanvas.gameObject.SetActive(false);
        }

        PopulateLoadCanvas();

    }

    //Add gameobjects to the load canvas
    private void PopulateLoadCanvas()
    {
        //find each instance of a level stored in 
        foreach (var item in GMaster.GetComponent<TestSaveandLoadScript>().AllLevels.Levels)
        {
            GameObject UIElement = Instantiate(LoadUI, LoadObject.transform);
            UIElement.transform.SetParent(LoadObject.transform, false);
            UIElement.transform.localScale = LoadObject.transform.localScale;
        }
    }
}
