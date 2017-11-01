using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveHexController : MonoBehaviour {

    //Sprites for the various types of tile
    [Header("Tile Sprites")]
    public Sprite Aries;
    public Sprite Taurus;
    public Sprite GeminiGood;
    public Sprite GeminiBad;
    public Sprite Cancer;
    public Sprite Leo;
    public Sprite Virgo;
    public Sprite Libra;
    public Sprite Scorpio;
    public Sprite Sagittarius;
    public Sprite Capricorn;
    public Sprite Aquarius;
    public Sprite Pisces;
   

    //Enum to identify which tile type this is
    public enum HexType
    {
        Null,
        Aries,
        Taurus,
        GeminiGood,
        GeminiBad,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Sagittarius,
        Capricorn,
        Aquarius,
        Pisces
    }

    //Enum holder for this tile
    public HexType CurrentHexType;

    //Hex's current arbitrary coordinates on the hex grid
    public int x, y;

    //Sprite renderer, private because it will be set individually
    private SpriteRenderer SprRenderer;

    //
    GameObject GMaster;
    GameMaster GMScript;



	// Use this for initialization
	void Start ()
    {
        //Access the sprite renderer on startup
        SprRenderer = GetComponent<SpriteRenderer>();
        
        //Find the gamemaster object
        GMaster = GameObject.Find("HexBaseHolder");
        GMScript = GMaster.GetComponent<GameMaster>();
        SpriteChanger(CurrentHexType);

    }
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    //Function to call SpriteChanger with the information already in the GameObject
    void CallSpriteChanger()
    {
        HexType TempNo = CurrentHexType;
        SpriteChanger(TempNo);
    }

    //Function to call whenever the sprite needs changing
    void SpriteChanger(HexType SpriteNum)
    {
        int intSpriteNum = (int)SpriteNum;
        switch (intSpriteNum)
        {
            case 0:
                SprRenderer.sprite = null;
                break;
            case 1:
                SprRenderer.sprite = Aries;
                break;
            case 2:
                SprRenderer.sprite = Taurus;
                break;
            case 3:
                SprRenderer.sprite = GeminiGood;
                break;
            case 4:
                SprRenderer.sprite = GeminiBad;
                break;
            case 5:
                SprRenderer.sprite = Cancer;
                break;
            case 6:
                SprRenderer.sprite = Leo;
                break;
            case 7:
                SprRenderer.sprite = Virgo;
                break;
            case 8:
                SprRenderer.sprite = Libra;
                break;
            case 9:
                SprRenderer.sprite = Scorpio;
                break;
            case 10:
                SprRenderer.sprite = Sagittarius;
                break;
            case 11:
                SprRenderer.sprite = Capricorn;
                break;
            case 12:
                SprRenderer.sprite = Aquarius;
                break;
            case 13:
                SprRenderer.sprite = Pisces;
                break;
            default:
                SprRenderer.sprite = null;
                break;
        }
    }

    public void OnMouseDown()
    {


        //If nothing has been selected yet
        if (GMScript.Clicked == false)
        {
            //Check if the tile is null, and if it is then ignore the click
            if (CurrentHexType != 0)
            {
                //Set this as the currently selected hex
                GMScript.CurrentlySelected = this.gameObject;
                GMScript.Clicked = true;
            }

        }
        //If something has been selected, check if its possible to switch the two
        else
        {
            InteractiveHexController CurrentlySelectedScript = GMScript.CurrentlySelected.GetComponent<InteractiveHexController>();
            //Record old position and X/Y
            Vector3 OldPos = this.transform.position;
            int OldX = x;
            int OldY = y;
            int NewX = CurrentlySelectedScript.x;
            int NewY = CurrentlySelectedScript.y;

            //And if it is possible, switch them
            if (CompareX(CurrentlySelectedScript, 1) && CompareY(CurrentlySelectedScript, 1))
            {
                //Switch the two tiles around
                this.transform.position = GMScript.CurrentlySelected.transform.position;
                GMScript.CurrentlySelected.transform.position = OldPos;

                //Switch the X round
                int t = x;
                x = CurrentlySelectedScript.x;
                CurrentlySelectedScript.x = t;
                //Switch the Y around
                t = y;
                y = CurrentlySelectedScript.y;
                CurrentlySelectedScript.y = t;


                GMScript.Clicked = false;
                Debug.Log("Switched: " + CurrentlySelectedScript.x + "," + CurrentlySelectedScript.y + " With " + x + "," + y);
            }
            else
            {
                GMScript.Clicked = false;
                Debug.Log("Can't switch: " + CurrentlySelectedScript.x + "," + CurrentlySelectedScript.y + " With " + OldX + "," + OldY);
                return;
            }
            //Increment the moves counter
            GMScript.Moves++;
        }
    }

    //Compares this x to the x of the currently selected tile
    public bool CompareX(InteractiveHexController Tile, int Tolerance)
    {
        //Compare this x to the 
        if (this.x <= Tile.x + Tolerance
            &&
            this.x >= Tile.x - Tolerance)
        {
            return true;
        }
        
        return false;
    }
    //Compares this y to the y of the currently selected tile
    public bool CompareY(InteractiveHexController Tile, int Tolerance)
    {
        //Compare this x to the 
        if (this.y <= Tile.y + Tolerance
            &&
            this.y >= Tile.y - Tolerance)
        {
            return true;
        }
        return false;
    }
}
