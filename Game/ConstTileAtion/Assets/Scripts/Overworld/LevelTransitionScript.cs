using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionScript : MonoBehaviour {

    public Sprite[] LevelNumberSprites;
    public int LevelNumber;
    public HexInfo.HexType LevelType;

    public Image LevelNumImage;
    public Image Star1, Star2, Star3, Locked, Completed;

    public enum LevelStates
    {
        Locked,
        Unlocked,
        Complete,
        OneStar,
        TwoStars,
        ThreeStars
    }
    public LevelStates LevelState;
    private GameObject Persistant;

	
	void Awake ()
    {
        Persistant = GameObject.Find("Persistant");

        //Set all variable elements of the canvas to inactive
        Star1.gameObject.SetActive(false);
        Star2.gameObject.SetActive(false);
        Star3.gameObject.SetActive(false);
        Locked.gameObject.SetActive(false);
        Completed.gameObject.SetActive(false);

        //And then, depending on what the level should be when it is instantiated, enable various elements
        switch (LevelState)
        {
            case LevelStates.Locked:
                Locked.gameObject.SetActive(true);
                break;
            case LevelStates.Unlocked:
                break;
            case LevelStates.Complete:
                Completed.gameObject.SetActive(true);
                break;
            case LevelStates.OneStar:
                Completed.gameObject.SetActive(true);
                Star1.gameObject.SetActive(true);
                break;
            case LevelStates.TwoStars:
                Completed.gameObject.SetActive(true);
                Star1.gameObject.SetActive(true);
                Star2.gameObject.SetActive(true);
                break;
            case LevelStates.ThreeStars:
                Completed.gameObject.SetActive(true);
                Star1.gameObject.SetActive(true);
                Star2.gameObject.SetActive(true);
                Star3.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonLoadLevel()
    {
        //Persistant.GetComponent<>
    }

}
