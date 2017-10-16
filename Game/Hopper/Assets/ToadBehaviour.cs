using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadBehaviour : MonoBehaviour {

    private Rigidbody2D ThisRigidBody;
    public float ToadSpeed = 1f;
    public Vector2 Movement = new Vector2(ToadSpeed, ToadSpeed);
    

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Awake is called whenever the object is activated
    void Awake()
    {
        //Get the rigidbody of this toad
        ThisRigidBody = this.GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is where all physics code should go
    void FixedUpdate()
    {
        ThisRigidBody.AddForce();
    }
}
