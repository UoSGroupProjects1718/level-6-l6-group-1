using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantInfo : MonoBehaviour
{
    private void Awake()
    {
        //Sets the orientation to Portrait
        Screen.orientation = ScreenOrientation.Portrait;
        DontDestroyOnLoad(this);
    }
    public int LevelType;
}
