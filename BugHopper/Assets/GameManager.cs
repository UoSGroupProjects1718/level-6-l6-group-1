using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int Columns;
    public int Rows;
    public int CurrentlySelected;
    public int Moves;

    public GameObject RedHexObject, GreenHexObject, BlueHexObject;
    public GameObject HexParent;
    HexData HexDataScript;

    void Hexcreation(GameObject Hex, float x, float y, int ID)
    {
        GameObject HexChild = Instantiate(Hex, new Vector3(x, y, 0), Quaternion.identity); //Create the hex given to it
        HexChild.transform.parent = HexParent.transform;//Set the just-created hex as a child
        HexDataScript = HexChild.GetComponent<HexData>();//Get the HexData script from the newly instantiated child
        HexDataScript.HexID = ID;//Give it an ID
        HexChild.name = ID.ToString();
    }

    // Use this for initialization
    void Start (){

        //Calculate the position to start at for this level
        float X = -Rows / 2f;
        float Y = -Columns / 2f;
        int Alternater = 1;
        bool Shifter = false;
        int IDCounter = 1;

        //Create a grid of hexes

        for (int i = 0; i < Columns; i++)
        {
            //If the column needs shifting then shift it slightly, if not then don't.
            if (Shifter)
                X -= 0.5f;
            else
                X += 0.5f;
            Shifter = !Shifter;

            for (int j = 0; j < Rows; j++)
            {
                //Make sure that all of the hexes are cycled
                switch (Alternater)
                {
                    case 1:
                        Hexcreation(RedHexObject, X, Y, IDCounter);
                        Alternater++;
                        Debug.Log("Red");
                        break;

                    case 2:
                        Hexcreation(GreenHexObject, X, Y, IDCounter);
                        Alternater++;
                        Debug.Log("Green");
                        break;

                    case 3:
                        Hexcreation(BlueHexObject, X, Y, IDCounter);
                        Alternater = 1;
                        Debug.Log("Blue");
                        break;
                }
                IDCounter++;

                X += 2f;//Shift the next hex over by two so they don't overlap
            }
            X = -Rows / 2f;//Reset rows

            Y += 1.5f;//Move the column
        }



	}
	
	// Update is called once per frame
	void Update ()
    {


	}
}
