using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFade : MonoBehaviour
{

    public GameObject TopRight1, TopRight2, TopLeft1, TopLeft2, BottomLeft1, BottomLeft2, BottomRight1, BottomRight2;

    SpriteRenderer TRSprite1, TRSprite2, TLSprite1, TLSprite2, BLSprite1, BLSprite2, BRSprite1, BRSprite2;

    private float StartTime;


	// Use this for initialization
	void Start ()
    {
        //Get all of the sprites to start with
        TRSprite1 = TopRight1.GetComponent<SpriteRenderer>();
        TRSprite2 = TopRight2.GetComponent<SpriteRenderer>();
        TLSprite1 = TopLeft1.GetComponent<SpriteRenderer>();
        TLSprite2 = TopLeft2.GetComponent<SpriteRenderer>();
        BLSprite1 = BottomLeft1.GetComponent<SpriteRenderer>();
        BLSprite2 = BottomLeft2.GetComponent<SpriteRenderer>();
        BRSprite1 = BottomRight1.GetComponent<SpriteRenderer>();
        BRSprite2 = BottomRight2.GetComponent<SpriteRenderer>();


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
