using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    public GameObject RedToad, BlueToad, PinkToad;
    public GameObject RedPad, BluePad, PinkPad;
    

    public int NumberOfToads;//Sets the number of toads to spawn that level

    //List of gameobjects
    private List<GameObject> ToadList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update()
    {

        if (ToadList.Count <= 0)
        {
            GameObject NewToad = Instantiate(RedToad, transform.position, Quaternion.identity);
            ToadList.Add(NewToad);
            Debug.Log(ToadList.Count);
        }
	}


}
