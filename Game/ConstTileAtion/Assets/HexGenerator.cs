using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Helpful website: https://www.redblobgames.com/grids/hexagons/


public class HexGenerator : MonoBehaviour {

    //Number of layers the hexagons will generate to
    public int HexLayers;

    //Hex Prefab
    public GameObject HexPrefab;
    public Sprite HexSprite;

	// Use this for initialization
	void Start ()
    {
        //Get the initial sprite info from the Hex
        HexSprite = HexPrefab.GetComponent<Sprite>();




        //Instantiate initial Hex
        Instantiate(HexPrefab);

        //Check if the number of layers is more than one
        if (HexLayers >= 2)
        {
            //Instantiate the 2nd layer of hexes
            for (int i = 0; i < 6; i++)
            {
                PlaceHex();
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlaceHex(  int X,
                    int Y,
                    int ArbiraryX,
                    int ArbitatyY,
                    float Facing,
                    GameObject child)
    {

    }
}
