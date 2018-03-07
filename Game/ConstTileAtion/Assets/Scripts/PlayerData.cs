using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour
{
    public bool Next;

    private void Awake()
    {
        //Sets the orientation to Portrait
        Screen.orientation = ScreenOrientation.Portrait;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {
        //Initialise PLayerPrefs values
        //if (!PlayerPrefs.HasKey("Sign"))
            PlayerPrefs.SetInt("Sign", 0);
        //if (!PlayerPrefs.HasKey("Difficulty"))
            PlayerPrefs.SetInt("Difficulty", 0);

    }
}