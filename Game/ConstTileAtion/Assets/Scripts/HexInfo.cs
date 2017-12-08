using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.redblobgames.com/grids/hexagons/


public class HexInfo : MonoBehaviour {

    //Info for each hex
    public int X, Y;
    public int Layer;
    public GameObject GMaster, ClickedOverlay, OverlayParent;
    public GameMaster GMScript;

    public Sprite[] SpriteArray = new Sprite[13];

    public enum HexType
    {
        Aries,
        Taurus,
        Gemini,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Sagittarius,
        Capricorn,
        Aquarius,
        Pisces,
        Null
    }

    public HexType CurrentHexType;

    //Bool to set to true if the object has been checked 
    public bool Checked;

    //Sprite renderer, private because it will be set individually
    private SpriteRenderer SprRenderer;

    //List of neighbors
    public List<GameObject> Neighbors;

    //Dropdown Menu to get values
    public GameObject TileSetter;

    // Use this for initialization
    void Start ()
    {
        //Set the GameObject so we can use the HexGenerator script in it later
        GMaster = GameObject.Find("HexBaseHolder");
        GMScript = GMaster.GetComponent<GameMaster>();
        SprRenderer = GetComponent<SpriteRenderer>();

        if (Layer <=GMScript.LayersBeingUsed)
        {
            //SetHexSprite();

            //Find neighbors and add them to the neighbor list
            foreach (Transform Holder in GMaster.transform)
            {
                foreach (Transform Child in Holder.transform)
                {
                    //If the Tile is within 1 of the current, add it to the neighbors list
                    if (IsNeighbor(X, Y, Child.GetComponent<HexInfo>().X, Child.GetComponent<HexInfo>().Y, 1))
                    {
                        Neighbors.Add(Child.gameObject);
                    }
                }
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    //Set the sprites of the hexes, this will be run at the start of the game and every time it resets
    public void SetHexSprite()
    {
        SpriteChanger();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    //Checks if two tiles are neighbors within a certain range
    public bool IsNeighbor(int CurrentX, int CurrentY, int TargetX, int TargetY, int Offset)
    {
        if (CurrentX == TargetX + Offset && CurrentY == TargetY)
            return true;
        if (CurrentX == TargetX - Offset && CurrentY == TargetY)
            return true;
        if (CurrentX == TargetX && CurrentY == TargetY + Offset)
            return true;
        if (CurrentX == TargetX && CurrentY == TargetY - Offset)
            return true;
        if (CurrentX == TargetX + Offset && CurrentY == TargetY - Offset)
            return true;
        if (CurrentX == TargetX - Offset && CurrentY == TargetY + Offset)
            return true;
        else
            return false;
    }

    //Function to call whenever the sprite needs changing
    void SpriteChanger()
    {
        SprRenderer.sprite = SpriteArray[(int)CurrentHexType];
    }

    public void OnMouseDown()
    {
        //Skip the entire function if hexes are disabled
        if (GMScript.DisableHexes)
        {
            return;
        }
        //First check if editmode is on
        if (GMScript.EditMode)
        {
            //Set the clicked-on tile to selected (so we know which one we will be adding the symbol to)
            GMScript.CurrentlySelected = this.gameObject;

            //Set the dropdown value to be the hex type
            CurrentHexType = (HexType)GMScript.CurrentlySelectedType;

            //Then call SpriteChanger to set the sprite to the correct type
            SpriteChanger();

            return;
        }
        //If nothing has been selected yet
        if (GMScript.Clicked == false && !GMScript.EditMode)
        {
            //Check if the tile is null, and if it is then ignore the click
            if (CurrentHexType != HexType.Null)
            {
                //Set this as the currently selected hex
                GMScript.CurrentlySelected = this.gameObject;
                GMScript.CurrentlySelectedType = (int)CurrentHexType;
                GMScript.Clicked = true;

                //Search through the all Hex's and set them to unchecked
                foreach (Transform Parent in GMaster.transform)
                {
                    foreach (Transform Child in Parent.transform)
                    {
                        Child.GetComponent<HexInfo>().Checked = false;
                    }
                }
                HightlightMoves(this.GetComponent<HexInfo>());
            }

        }
        //If something has been selected, check if its possible to switch the two
        else if(!GMScript.EditMode)
        {
            //Get the Hexinfo of the currently selected hex
            HexInfo HexScript = GMScript.CurrentlySelected.GetComponent<HexInfo>();
            UnhighlightMoves();
            switch (HexScript.CurrentHexType)
            {
                //Basic Tile Movement
                case HexType.Aries:
                    if (IsNeighbor(X, Y, HexScript.X, HexScript.Y, 1))
                    {
                        Move(HexScript);
                        break;
                    }
                    CantMove(HexScript);
                    break;
                //Knight movement (Skips over a tile)
                case HexType.Taurus:
                    if (IsNeighbor(X, Y, HexScript.X, HexScript.Y, 2))
                    {
                        Move(HexScript);
                        break;
                    }
                    CantMove(HexScript);
                    break;
                case HexType.Gemini:
                    if (IsNeighbor(X, Y, HexScript.X, HexScript.Y, 1))
                    {
                        GeminiMove(HexScript);
                        break;
                    }
                    break;
                case HexType.Cancer:
                case HexType.Leo:  
                case HexType.Virgo:    
                case HexType.Libra:
                case HexType.Scorpio:
                case HexType.Sagittarius:    
                case HexType.Capricorn:    
                case HexType.Aquarius:    
                case HexType.Pisces:  
                case HexType.Null: 
                default:
                    CantMove(HexScript);
                    break;
                    
            }
        }
    }

    //Function to search hex's around it to see if they are connected
    public bool WinSearch()
    {
        //Flip the Checked bool on this object to true
        Checked = true;

        foreach (GameObject Hex in Neighbors)
        {
            //If the neighbor type isn't null
            if (Hex.GetComponent<HexInfo>().CurrentHexType != HexType.Null)
            {
                //And it hasn't already been checked
                if (!Hex.GetComponent<HexInfo>().Checked)
                {
                    Hex.GetComponent<HexInfo>().WinSearch();
                }
            } 
        }

        GMScript.CheckWin();

        return false;
    }

    //Basic Move function
    void Move(HexInfo OtherHex)
    {
        //record current hex type
        int TempType = (int)CurrentHexType;
        //Set current hex type to be the same as the currently selected one
        CurrentHexType = GMScript.CurrentlySelected.GetComponent<HexInfo>().CurrentHexType;
        //Set the CurrentHexType of the other hex to be what this one was
        GMScript.CurrentlySelected.GetComponent<HexInfo>().CurrentHexType = (HexType)TempType;
        GMScript.Clicked = false;

        //Switch to the new sprite
        SpriteChanger();

        //Switch old hex to the new sprite
        GMScript.CurrentlySelected.GetComponent<HexInfo>().SpriteChanger();

        Checked = WinSearch();
        Debug.Log("Switched: " + OtherHex.X + "," + OtherHex.Y + " With " + X + "," + Y);
        //Increment the moves counter
        GMScript.Moves++;
        GMScript.UpdateMoveCounter();
    }

    //Catapillar move for the gemini
    void GeminiMove(HexInfo Hex)
    {
        //If the clicked on Hex type is Null
        if (CurrentHexType == HexType.Null)
        {
            //Set this hex type to the other's
            this.CurrentHexType = Hex.CurrentHexType;
            GMScript.NumToWin++;

            GMScript.Clicked = false;

            //Switch to the new sprite
            SpriteChanger();

            //Switch old hex to the new sprite
            GMScript.CurrentlySelected.GetComponent<HexInfo>().SpriteChanger();

            Checked = WinSearch();
            Debug.Log("Switched: " + Hex.X + "," + Hex.Y + " With " + X + "," + Y);
            //Increment the moves counter
            GMScript.Moves += 2;
            GMScript.UpdateMoveCounter();
        }
    }

    public void CantMove(HexInfo OtherHex)
    {
        GMScript.Clicked = false;
        Debug.Log("Can't switch: " + OtherHex.X + "," + OtherHex.Y + " With " + X + "," + Y);
    }

    //Function to decide which type of highlighting should be used
    void HightlightMoves(HexInfo Hex)
    {
        switch (CurrentHexType)
        {
            case HexType.Aries:
                HighLightInstantiator(1, Hex);
                break;
            case HexType.Taurus:
                HighLightInstantiator(2, Hex);
                break;
            case HexType.Gemini:
                HighLightInstantiator(1, Hex);
                break;
            case HexType.Cancer:
                break;
            case HexType.Leo:
                break;
            case HexType.Virgo:
                break;
            case HexType.Libra:
                break;
            case HexType.Scorpio:
                break;
            case HexType.Sagittarius:
                break;
            case HexType.Capricorn:
                break;
            case HexType.Aquarius:
                break;
            case HexType.Pisces:
                break;
            case HexType.Null:
                break;
            default:
                break;
        }
    }

    void HighLightInstantiator(int I, HexInfo Hex)
    {
        //Run through all the Hex's and see if they are a viable target for the selected hex
        foreach (Transform Parent in GMaster.transform)
        {
            foreach (Transform Child in Parent.transform)
            {
                if (IsNeighbor(Hex.X, Hex.Y, Child.GetComponent<HexInfo>().X, Child.GetComponent<HexInfo>().Y, I))
                {
                    GameObject Overlay = Instantiate(ClickedOverlay, new Vector3(Child.transform.position.x, Child.transform.position.y, Child.transform.position.z), Quaternion.identity);
                    Overlay.transform.parent = OverlayParent.transform;
                }

            }
        }
    }

    void UnhighlightMoves()
    {
        foreach (Transform item in OverlayParent.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
