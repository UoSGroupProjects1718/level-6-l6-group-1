using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    //Adds a header into the inspecter
    [Header("Layer 0")]
    //Gameobject which is the parent of all that rings hexes
    public GameObject RingHolder0;
    //Array to hold all hex gameobjects
    [SerializeField] GameObject[] HexRing0 = new GameObject[1];

    [Header("Layer 1")]
    public GameObject RingHolder1;
    [SerializeField] GameObject[] HexRing1 = new GameObject[6];

    [Header("Layer 2")]
    public GameObject RingHolder2;
    [SerializeField] GameObject[] HexRing2 = new GameObject[12];

    [Header("Layer 3")]
    public GameObject RingHolder3;
    [SerializeField] GameObject[] HexRing3 = new GameObject[18];

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

    }
	
	// Update is called once per frame
	void Update ()
    {
		




	}

    void GetHexChildren(GameObject RingParent, out GameObject[] HexArray)

    {
        HexArray = new GameObject[RingParent.transform.childCount];
        for (int i=0; i< RingParent.transform.childCount; i++)
        {
            HexArray[i]= RingParent.transform.GetChild(i).gameObject;
        }
        return;

    }
}
