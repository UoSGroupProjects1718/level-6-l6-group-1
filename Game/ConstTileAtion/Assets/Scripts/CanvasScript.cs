//Each canvas should have this script on it so that it can be added to the Canvases List of the UI controller

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour {

    public bool EnableOnStart;

    //In start, add it to the canvaslist and then, if it is supposed to be, disable it on start
    private void Start()
    {
        GameObject.Find("OverworldMaster").GetComponent<Overworld>().Canvases.Add(this.gameObject);
        this.gameObject.SetActive(EnableOnStart);
    }
}
