using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldOffshootButton : MonoBehaviour {

    OverworldLevelData ParentScript;

    private void Awake()
    {
        //Get this object's parent
        ParentScript = this.transform.parent.GetComponent<OverworldLevelData>();
    }

    private void OnMouseDown()
    {
        ParentScript.LoadLevel();
    }
}
