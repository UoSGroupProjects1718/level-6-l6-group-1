using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvasLoader : MonoBehaviour {

    public Image NameImage;
    public Sprite[] Names;

    private int CurrentSign;




    public void SwitchName(int Name)
    {
        NameImage.sprite = Names[Name];
        CurrentSign = Name;
    }
}
