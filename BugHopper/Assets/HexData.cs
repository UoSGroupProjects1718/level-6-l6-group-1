using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexData : MonoBehaviour {


    public int HexID;
    GameObject GMaster, HexHolder;
    public GameObject ClickedOverlay;
    GameManager GMasterScript;


	// Use this for initialization
	void Start ()
    {
        GMaster = GameObject.Find("GameMaster");
        GMasterScript = GMaster.GetComponent<GameManager>();
        HexHolder = GameObject.Find("HexHolder");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void OnMouseDown()
    {
        foreach (Transform child in GMaster.transform)//Before we instantiate the object, destroy all previous ones
        {
            GameObject.Destroy(child.gameObject);
        }
        //Find the position of the current hex
        Vector3 Pos = transform.position;

        if (GMasterScript.CurrentlySelected == 0)
        {
            GMasterScript.CurrentlySelected = HexID;//Set the currently selected hex ID
            GameObject CickedOutline = Instantiate(ClickedOverlay, Pos, Quaternion.identity);//Create the clicked overlay here
            CickedOutline.transform.parent = GMaster.transform;//Set the just-created overlay as a child of the GameMaster
            Debug.Log("Clicked: " + HexID);
        }
        else
        {
            foreach (Transform child in HexHolder.transform)//Before we instantiate the object, destroy all previous ones
            {
                if (child.name == GMasterScript.CurrentlySelected.ToString())
                {
                    Vector3 OldPos = child.transform.position;//record old position
                    child.transform.position = this.transform.position;//Switch the second one with this
                    this.transform.position = OldPos;//Switch the new one

                    //Set the CurrentlySelected to 0
                    GMasterScript.CurrentlySelected = 0;
                    //GMasterScript.Moves++;
                    Debug.Log("Switched " + HexID + " And " + child.name);
                    break;
                }
                Debug.Log("Error, could not find hex to switch");
            }
        }
    }
}
