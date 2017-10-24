using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public GameObject HexPrefab;

    //Adds a header into the inspecter
    [Header("Layers")]

    //Gameobject which is the parent of all that rings hexes
    public GameObject RingHolder0, RingHolder1, RingHolder2, RingHolder3;
    //Array to hold all hex gameobjects, serialised to show in the inspector
    [SerializeField] GameObject[]   HexRing0 = new GameObject[1],
                                    HexRing1 = new GameObject[6],
                                    HexRing2 = new GameObject[12],
                                    HexRing3 = new GameObject[18];
    [Header("GameInfo")]
    public int LayersBeingUsed;


    //Useful thing to use later on
    //HexInfo ChildScript = child.GetComponent<HexInfo>();
    
        
        // Use this for initialization
    void Start ()
    {
        //First, loop through each of the objects which are children of the ringholders and add them into the array
        GetHexChildren(RingHolder0, out HexRing0);
        GetHexChildren(RingHolder1, out HexRing1);
        GetHexChildren(RingHolder2, out HexRing2);
        GetHexChildren(RingHolder3, out HexRing3);

        //Then go through each of the children and instantiate a hex in its place
        if (LayersBeingUsed >= 1)
            InstantiateLayers(HexRing0);
        if (LayersBeingUsed >= 2)
            InstantiateLayers(HexRing1);
        if (LayersBeingUsed >= 3)
            InstantiateLayers(HexRing2);
        if (LayersBeingUsed >= 4)
            InstantiateLayers(HexRing3);
        
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

    //Called to instantiate each layer of InteractableHexs
    void InstantiateLayers(GameObject[] Hexs)
    {
        for (int i = 0; i < Hexs.Length; i++)
        {
            //Instantiate the Hex prefab in the location of the "hexs" guide
            GameObject NewHex = Instantiate(HexPrefab, new Vector3(Hexs[i].transform.position.x, Hexs[i].transform.position.y), Quaternion.identity);

            //Get the script from the hex guide to get its coordinates
            HexInfo GuideScript = Hexs[i].GetComponent<HexInfo>();

            //Get the script from the newly created hex
            InteractiveHexController Script = NewHex.GetComponent<InteractiveHexController>();

            //Set the X and Y to be the one in the guide
            Script.x = GuideScript.X;
            Script.y = GuideScript.Y;
        }
    }
}
