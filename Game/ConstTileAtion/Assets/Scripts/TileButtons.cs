using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButtons : MonoBehaviour
{
    public HexInfo.HexType ButtonType;
    public GameObject GMScript;

    public void OnMouseDown()
    {
        GMScript.GetComponent<GameMaster>().CurrentlySelectedType = (int)ButtonType;
    }
}
