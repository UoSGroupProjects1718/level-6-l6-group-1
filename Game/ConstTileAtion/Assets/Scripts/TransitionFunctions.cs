using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionFunctions : MonoBehaviour
{
    public void TitleToOverworld()
    {
        SceneManager.LoadScene("Overworld");
    }
    
}
