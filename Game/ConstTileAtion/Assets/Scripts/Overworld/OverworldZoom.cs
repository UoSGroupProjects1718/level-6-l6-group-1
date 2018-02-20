using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldZoom : MonoBehaviour
{
    [Header("Camera sizes")]
    public float MainCameraSize;
    public float ZoomedCameraSize;

    public GameObject Aries,
        Taurus,
        Gemini,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Sagittarius,
        Capricorn,
        Aquarius,
        Pisces;
    public float MoveSpeed, StopSpeed;
    public GameObject Target, MainCanvas;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Target != null)
        {
            if (MainCanvas.activeSelf)
            {
                MainCanvas.SetActive(false);
            }
            this.transform.position = Vector3.Lerp(this.transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
            this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(this.GetComponent<Camera>().orthographicSize, ZoomedCameraSize, MoveSpeed * Time.deltaTime);
            if (MoveSpeed * Time.deltaTime < StopSpeed)
            {
                Target = null;
            }
        }

    }
    
    public void ZoomToLevels(GameObject NewTarget)
    {
        Target = NewTarget;
    }

}
