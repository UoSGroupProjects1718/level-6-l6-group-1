using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source code found here, one of the comments. Says it is C++. but is really C#
//https://answers.unity.com/questions/387800/click-holddrag-move-camera.html

public class OverworldDrag : MonoBehaviour
{
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Difference;
    private bool Drag = false;

    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Camera.main.transform.position = Origin - Difference;
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = ResetCamera;
        }
    }
}
 //   public float DragSpeed = 2;
 //   private Vector3 DragOrigin;

	
	//// Update is called once per frame
	//void LateUpdate ()
 //   {
 //       if (Input.GetMouseButtonDown(0))
 //       {
 //           DragOrigin = Input.mousePosition;
 //           return;
 //       }
 //       if (!Input.GetMouseButton(0))
 //           return;

 //       Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - DragOrigin);
 //       Vector3 move = new Vector3(pos.x * DragSpeed, pos.y * DragSpeed, 0);

 //       transform.Translate(move, Space.World);
	//}
