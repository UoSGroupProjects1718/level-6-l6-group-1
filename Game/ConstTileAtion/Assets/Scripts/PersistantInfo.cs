using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantInfo : MonoBehaviour
{
    private void Awake()
    {
    DontDestroyOnLoad(this);
    }
    public int LevelType;
}
