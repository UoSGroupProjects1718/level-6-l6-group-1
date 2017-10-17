﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : MonoBehaviour {

    //Info for each hex
    public float X, Y, Z;
    public int Layer;
    public GameObject HexGenerator;

	// Use this for initialization
	void Start ()
    {
        //Set the GameObject so we can use the HexGenerator script in it later
        HexGenerator = GameObject.Find("HexBaseHolder");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
