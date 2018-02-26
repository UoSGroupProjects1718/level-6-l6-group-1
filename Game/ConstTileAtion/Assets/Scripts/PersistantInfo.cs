using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantInfo : MonoBehaviour
{
    public HexInfo.HexType LevelType;
    public int LevelDifficulty;
    public bool Continue;
    public int Lives = 3;

    private void Awake()
    {
        //Sets the orientation to Portrait
        Screen.orientation = ScreenOrientation.Portrait;
        DontDestroyOnLoad(this);
    }

}
