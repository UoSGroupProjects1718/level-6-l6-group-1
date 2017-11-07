using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public GameObject HexPrefab;

    //Storage for the Hexbackground GameObjects
    public GameObject HexBackground_Center, HexBackground_1, HexBackground_2, HexBackground_3;

    //Gameobject which is the parent of all that rings hexes
    public GameObject RingHolder0, RingHolder1, RingHolder2, RingHolder3;
    
    //Array to hold all hex gameobjects, serialised to show in the inspector
    [SerializeField] GameObject[]   HexRing0 = new GameObject[1],
                                    HexRing1 = new GameObject[6],
                                    HexRing2 = new GameObject[12],
                                    HexRing3 = new GameObject[18];
    public GameObject TileHolder;

    [Header("GameInfo")]
    [Range(0,3)]public int LayersBeingUsed;
    public int Moves, NumToWin;
    public bool Clicked = false;
    public int CurrentlySelectedType;
    public GameObject CurrentlySelected;


    //Useful thing to use later on
    //HexInfo ChildScript = child.GetComponent<HexInfo>();


    // Use this for initialization
    void Start()
    {
        //First, loop through each of the objects which are children of the ringholders and add them into the array
        GetHexChildren(RingHolder0, out HexRing0);
        GetHexChildren(RingHolder1, out HexRing1);
        GetHexChildren(RingHolder2, out HexRing2);
        GetHexChildren(RingHolder3, out HexRing3);

        NumToWin = CheckActiveTiles();

        //Then go through each of the children and instantiate a hex in its place.
        //At the same time, set the correct layer of the background to active
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
		
	}

    //
    void GetHexChildren(GameObject RingParent, out GameObject[] HexArray)
    {
        HexArray = new GameObject[RingParent.transform.childCount];
        for (int i=0; i< RingParent.transform.childCount; i++)
        {
            HexArray[i]= RingParent.transform.GetChild(i).gameObject;
        }
        return;

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

    int CheckActiveTiles()
    {
        int ActiveTiles = 0;
        int LayerCounter = 0;
        foreach (Transform Holder in TileHolder.transform)
        {
            if (LayerCounter < LayersBeingUsed)
            {
                foreach (Transform Hex in Holder.transform)
                {
                    if (Hex.GetComponent<HexInfo>().CurrentHexType != HexInfo.HexType.Null)
                    {
                        ActiveTiles++;
                    }
                }
            }
            LayerCounter++;
        }
        return ActiveTiles;
    }
}
