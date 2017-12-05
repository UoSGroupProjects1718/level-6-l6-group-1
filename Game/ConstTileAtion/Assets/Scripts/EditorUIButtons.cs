using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
//http://benhumphreys.co.uk/blog/2015/09/06/how-to-make-a-scrollable-list-in-unitys-new-gui/
//

public class EditorUIButtons : MonoBehaviour
{
    public Canvas SaveCanvas;
    public GameObject LoadObject;
    public Canvas LoadCanvas;
    public GameObject GMaster;
    public GameMaster GMScript;
    public GameObject LoadUI;
    public Text ErrorText;

    public InputField LevelNameTextField;

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

        //Blank out the Text field
        LevelNameTextField.Select();
        LevelNameTextField.text = "";

        if (LoadCanvas.gameObject.activeSelf)
        {
            LoadCanvas.gameObject.SetActive(false);
        }
        ErrorText.text = "";
    }

    public void LoadCanvasToggle()
    {
        //Flick the LoadCanvas to whatever it wasn't before
        LoadCanvas.gameObject.SetActive(!LoadCanvas.gameObject.activeSelf);
        GMScript.DisableHexes = !GMScript.DisableHexes;
        
        if (SaveCanvas.gameObject.activeSelf)
        {
            SaveCanvas.gameObject.SetActive(false);
        }

        PopulateLoadCanvas();

    }

    //Add gameobjects to the load canvas
    private void PopulateLoadCanvas()
    {
        //Delete all objects that are already a part of this canvas
        foreach (Transform item in LoadObject.transform)
        {
            Destroy(item.gameObject);
        }
        //find each instance of a level stored and add a UI gameobject for that
        foreach (var item in GMaster.GetComponent<TestSaveandLoadScript>().AllLevels.Levels)
        {
            //Create a UI element from a prefab
            GameObject UIElement = Instantiate(LoadUI, LoadObject.transform);
            //Set the parent
            UIElement.transform.SetParent(LoadObject.transform, false);
            //Set the instantiated UI object to be the correct scale
            UIElement.transform.localScale = LoadObject.transform.localScale;

            //Get the LoadInstantiatedUIData script component
            LoadInstantiatedUIData NewUIObject = UIElement.GetComponent<LoadInstantiatedUIData>();
            //Set the data of the script from the JSON file
            NewUIObject.LvlID = item.LevelNumber;
            NewUIObject.LvlName = item.LevelName;
            NewUIObject.LvlType = item.Leveltype.ToString();
        }
    }

    //Deletes an entry based on its ID
    public void DeleteEntry(int DeleteID)
    {
        foreach (var item in GMaster.GetComponent<TestSaveandLoadScript>().AllLevels.Levels)
        {
            if (item.LevelNumber == DeleteID)
            {
                GMaster.GetComponent<TestSaveandLoadScript>().AllLevels.Levels.Remove(item);
            }
        }
    }
}
