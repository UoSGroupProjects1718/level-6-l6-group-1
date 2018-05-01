using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickDisable : MonoBehaviour {

    public GameObject disable;
    public GameObject[] enable;

    public void OnClick()
    {
        disable.SetActive(false);
    }

    public void OnClickEnable()
    {
        foreach (var item in enable)
        {
            item.SetActive(true);
        }
        
    }
}
