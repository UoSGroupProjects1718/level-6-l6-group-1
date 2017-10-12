using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchDirection : MonoBehaviour {

    private SpriteRenderer ThisObjectRenderer;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    void Awake()
    {
        ThisObjectRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        //Flip this objects X when the object is clicked on
        ThisObjectRenderer.flipX = !ThisObjectRenderer.flipX;
    }
}
