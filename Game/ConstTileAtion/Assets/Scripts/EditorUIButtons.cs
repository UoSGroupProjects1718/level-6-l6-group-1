using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//http://benhumphreys.co.uk/blog/2015/09/06/how-to-make-a-scrollable-list-in-unitys-new-gui/

public class EditorUIButtons : MonoBehaviour
{
    public Canvas SaveCanvas;
    public Canvas LoadCanvas;
    public GameObject GMaster;
    public GameMaster GMScript;

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
    }

    public void LoadCanvasToggle()
    {
        //Flick the SaveCanvas to whatever it wasn't before
        LoadCanvas.gameObject.SetActive(!LoadCanvas.gameObject.activeSelf);
        GMScript.DisableHexes = !GMScript.DisableHexes;
    }
}
