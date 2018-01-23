using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButtons : MonoBehaviour
{
    public HexInfo.HexType ButtonType;
    public GameObject GMScript, CurrentlySelectedUI;

    public void OnMouseDown()
    {
        GMScript.GetComponent<GameMaster>().CurrentlySelectedType = (int)ButtonType;
        CurrentlySelectedUI.GetComponent<CurrentlySelected>().ChangeHex((int)ButtonType);
    }
}
