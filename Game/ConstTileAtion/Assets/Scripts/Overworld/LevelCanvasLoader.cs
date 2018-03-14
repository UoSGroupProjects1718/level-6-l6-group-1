using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvasLoader : MonoBehaviour {

    public Image NameImage;



    public Sprite[] Names;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SwitchName(int Name)
    {
        NameImage.sprite = Names[Name];
    }








}
