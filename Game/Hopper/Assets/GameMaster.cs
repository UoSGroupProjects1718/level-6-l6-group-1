using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    public GameObject RedToad, BlueToad, PinkToad;
    public GameObject RedPad, BluePad, PinkPad;
    private Rigidbody2D RedToadBody, BlueToadBody, PinkToadBody;

    public int NumberOfToads;//Sets the initial array size to store the toads

    //Array of gameobjects
    private List<GameObject> ToadList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        //Get the rigidbodies for each of the toads we are using
        RedToadBody = RedToad.GetComponent<Rigidbody2D>();
        BlueToadBody = BlueToad.GetComponent<Rigidbody2D>();
        PinkToadBody = PinkToad.GetComponent<Rigidbody2D>();
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

    //FixedUpdate is called at a fixed interval and is where all physics code should go
    void FixedUpdate()
    {

    }
}
