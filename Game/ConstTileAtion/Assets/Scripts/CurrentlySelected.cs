using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentlySelected : MonoBehaviour
{
    public Sprite[] HexSprites;
    public string[] HexDescriptions;
    public Image HexImage;
    public Text HexDescription;

    public void ChangeHex(int HexNum)
    {
        HexImage.sprite = HexSprites[HexNum];
        HexDescription.text = HexDescriptions[HexNum];
        
    }
}
