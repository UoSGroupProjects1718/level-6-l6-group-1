using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveHexController : MonoBehaviour {

    public Sprite Aries, Taurus, GeminiGood, GeminiBad, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces;

    public enum HexType
    {
        Null,
        Aries,
        Taurus,
        GeminiGood,
        GeminiBad,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Sagittarius,
        Capricorn,
        Aquarius,
        Pisces
    }

    public HexType CurrentHexType;

    //Hex's current arbitrary coordinates on the hex grid
    public int x, y;

    //Sprite renderer, private because it will be set indevidually
    private SpriteRenderer SprRenderer;


	// Use this for initialization
	void Start ()
    {
        //Access the sprite renderer on startup
        SprRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    //Function to call SpriteChanger with the information already in the GameObject
    void CallSpriteChanger()
    {
        int TempNo = (int)CurrentHexType;
        SpriteChanger(TempNo);
    }

    //Function to call whenever the sprite needs changing
    void SpriteChanger(int SpriteNum)
    {
        switch (SpriteNum)
        {
            case 0:
                SprRenderer.sprite = null;
                break;
            case 1:
                SprRenderer.sprite = Aries;
                break;
            case 2:
                SprRenderer.sprite = Taurus;
                break;
            case 3:
                SprRenderer.sprite = GeminiGood;
                break;
            case 4:
                SprRenderer.sprite = GeminiBad;
                break;
            case 5:
                SprRenderer.sprite = Cancer;
                break;
            case 6:
                SprRenderer.sprite = Leo;
                break;
            case 7:
                SprRenderer.sprite = Virgo;
                break;
            case 8:
                SprRenderer.sprite = Libra;
                break;
            case 9:
                SprRenderer.sprite = Scorpio;
                break;
            case 10:
                SprRenderer.sprite = Sagittarius;
                break;
            case 11:
                SprRenderer.sprite = Capricorn;
                break;
            case 12:
                SprRenderer.sprite = Aquarius;
                break;
            case 13:
                SprRenderer.sprite = Pisces;
                break;
            default:
                SprRenderer.sprite = null;
                break;
        }
    }
    
}
