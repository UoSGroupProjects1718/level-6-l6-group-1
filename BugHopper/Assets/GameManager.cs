using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int Columns;
    public int Rows;
    public int CurrentlySelected;
    public int Moves;

    public GameObject RedHexObject, GreenHexObject, BlueHexObject;
    public GameObject HexParent, MovesTextObject;
    HexData HexDataScript;
    Text MovesText;

    void HexBoardCreation()
    {
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
                        HexInstantiation(RedHexObject, X, Y, IDCounter);
                        Alternater++;
                        Debug.Log("Red");
                        break;

                    case 2:
                        HexInstantiation(GreenHexObject, X, Y, IDCounter);
                        Alternater++;
                        Debug.Log("Green");
                        break;

                    case 3:
                        HexInstantiation(BlueHexObject, X, Y, IDCounter);
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

    void HexInstantiation(GameObject Hex, float x, float y, int ID)
    {
        GameObject HexChild = Instantiate(Hex, new Vector3(x, y, 0), Quaternion.identity); //Create the hex given to it
        HexChild.transform.parent = HexParent.transform;//Set the just-created hex as a child
        HexDataScript = HexChild.GetComponent<HexData>();//Get the HexData script from the newly instantiated child
        HexDataScript.HexID = ID;//Give it an ID
        HexChild.name = ID.ToString();
    }

    public void OnMouseDown()
    {
        foreach (Transform child in HexParent.transform)//Destroy the previous hexes
        {
            GameObject.Destroy(child.gameObject);
        }
        HexBoardCreation();//Then call HexBoardCreation to re-instantiate the hexes
        Moves = 0;
    }

    // Use this for initialization
    void Start ()
    {
        MovesText = MovesTextObject.GetComponent<Text>();
        HexBoardCreation();

	}
	
	// Update is called once per frame
	void Update ()
    {
        MovesText.text = Moves.ToString();


    }

}
