using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public GameObject HexPrefab;

    //Storage for the Hexbackground GameObjects
    public GameObject HexBackground_Center, HexBackground_1, HexBackground_2, HexBackground_3;

    //Gameobject which is the parent of all that rings hexes
    public GameObject RingHolder0, RingHolder1, RingHolder2, RingHolder3;
    public GameObject TileHolder;

    [Header("GameInfo")]
    [Range(0,3)]public int LayersBeingUsed;
    public int Moves, NumToWin;
    public bool Clicked = false;
    public int CurrentlySelectedType;
    public GameObject CurrentlySelected;
    public Text MoveCounter;
    public bool EditMode = false;

        // Use this for initialization
    void Start()
    {
        LayerSetter();
    }

    public void LayerSetter()
    {
        //Set the correct layer of the background to active
        if (LayersBeingUsed >= 0)
        {
            HexBackground_Center.SetActive(true);

            if (LayersBeingUsed >= 1)
            {
                HexBackground_1.SetActive(true);

                if (LayersBeingUsed >= 2)
                {
                    HexBackground_2.SetActive(true);

                    if (LayersBeingUsed >= 3)
                    {
                        HexBackground_3.SetActive(true);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //First check if the mouse-click was a right click
        if (EditMode)
        {
            GMScript.CurrentlySelected = this.gameObject;
            TileSetter.transform.position = this.transform.position;
            Debug.Log("Should Have Moved");
            //Return because we don't want the rest of this script executing
            return;
        }
    }

    //Checks if the game has been won yet
    bool WinCheck()
    {
        foreach (Transform Child in TileHolder.transform)
        {
            //Ignore if the child is Null
            if (Child.GetComponent<HexInfo>().CurrentHexType == 0)
                break;
            //Else, call the TileWinCheck function with the X/Y of the gameobject
            else
            {
                TileWinCheck(Child.GetComponent<HexInfo>().X, Child.GetComponent<HexInfo>().Y);
            }
        }
        return false;
    }

    bool TileWinCheck(int TileX, int TileY)
    {
        foreach (Transform Holder in this.transform)
        {
            foreach (Transform Hex in Holder.transform)
            {
                if (Hex.GetComponent<HexInfo>().X == TileX && Hex.GetComponent<HexInfo>().Y == TileY)
                {

                }
            }
        }
                return false;
    }
    
    public void CheckWin()
    {
        int Checkedcount = 0;
        foreach (Transform Parent in this.transform)
        {
            foreach (Transform Child in Parent.transform)
            {
                if (Child.GetComponent<HexInfo>().Checked)
                {
                    Checkedcount++;
                }
            }
        }
        if (Checkedcount >= NumToWin)
        {
            //Do thing that says you are going to win
            Debug.Log("Winner!");
            ResetGame();
        } 
    }

    private void ResetGame()
    {
        NumToWin = 0;
        Moves = 0;
        foreach (Transform Parent in this.transform)
        {
            foreach (Transform Child in Parent.transform)
            {
                Child.GetComponent<HexInfo>().SetHexSprite();
            }
        }
    }

    public void UpdateMoveCounter()
    {
        MoveCounter.text = Moves.ToString();
    }
}
