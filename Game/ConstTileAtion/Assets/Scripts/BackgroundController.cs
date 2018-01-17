using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public Sprite[] Backgrounds;
    SpriteRenderer CurrentBackground;

    void Awake()
    {
        //When the program first runs, set the sprite renderer component
        CurrentBackground = this.GetComponent<SpriteRenderer>();
    }

    public void ChangeBackground(int BackgroundID)
    {
        CurrentBackground.sprite = Backgrounds[BackgroundID];
    }
}
