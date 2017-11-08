using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.redblobgames.com/grids/hexagons/


public class HexInfo : MonoBehaviour {

    //Info for each hex
    public int X, Y;
    public int Layer;
    public GameObject GMaster;
    public GameMaster GMScript;

    public Sprite[] SpriteArray = new Sprite[13];

    public enum HexType
    {
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

    // Use this for initialization
    void Start ()
    {
        //Set the GameObject so we can use the HexGenerator script in it later
        GMaster = GameObject.Find("HexBaseHolder");
        GMScript = GMaster.GetComponent<GameMaster>();
        SprRenderer = GetComponent<SpriteRenderer>();

        if (Layer <=GMScript.LayersBeingUsed)
        {
            SetHexSprite();

            //Find neighbors and add them to the neeighbor list
            foreach (Transform Holder in GMaster.transform)
            {
                foreach (Transform Child in Holder.transform)
                {
                    //If the Tile is within 1 of the current, add it to the neighbors list
                    if (IsNeighbor1(X, Y, Child.GetComponent<HexInfo>().X, Child.GetComponent<HexInfo>().Y))
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

    public void SetHexSprite()
    {
        //Randomly assign a star sign to a tile, only 1/3 of the tiles will have signs
        int random = Random.Range(0, 4);

        if (random == 0)
        {
            CurrentHexType = (HexType)Random.Range(0, 13);
            //Add this to the number of non-null active tiles, to work out how many there need to be to win.
            GMScript.NumToWin++;
        }
        else
        {
            CurrentHexType = HexType.Null;
        }
        SpriteChanger();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public bool IsNeighbor1(int CurrentX, int CurrentY, int TargetX, int TargetY)
    {
        if (CurrentX == TargetX + 1 && CurrentY == TargetY)
            return true;
        if (CurrentX == TargetX - 1 && CurrentY == TargetY)
            return true;
        if (CurrentX == TargetX && CurrentY == TargetY + 1)
            return true;
        if (CurrentX == TargetX && CurrentY == TargetY - 1)
            return true;
        if (CurrentX == TargetX + 1 && CurrentY == TargetY - 1)
            return true;
        if (CurrentX == TargetX - 1 && CurrentY == TargetY + 1)
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
        //If nothing has been selected yet
        if (GMScript.Clicked == false)
        {
            //Check if the tile is null, and if it is then ignore the click
            if (CurrentHexType != HexType.Null)
            {
                //Set this as the currently selected hex
                GMScript.CurrentlySelected = this.gameObject;
                GMScript.CurrentlySelectedType = (int)CurrentHexType;
                GMScript.Clicked = true;

                //Search through the entire array of Hex's and set them all to unchecked
                foreach (Transform Parent in GMaster.transform)
                {
                    foreach (Transform Child in Parent.transform)
                    {
                        Child.GetComponent<HexInfo>().Checked = false;
                    }
                }
            }

        }
        //If something has been selected, check if its possible to switch the two
        else
        {
            HexInfo HexScript = GMScript.CurrentlySelected.GetComponent<HexInfo>();
            //And if it is possible, switch them
            if (IsNeighbor1(X, Y, HexScript.X, HexScript.Y))
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

                Debug.Log("Switched: " + HexScript.X + "," + HexScript.Y + " With " + X + "," + Y);
            }

            else
            {
                GMScript.Clicked = false;
                Debug.Log("Can't switch: " + HexScript.X + "," + HexScript.Y + " With " + X + "," + Y);
                return;
            }
            //Increment the moves counter
            GMScript.Moves++;
        }
    }


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
}
