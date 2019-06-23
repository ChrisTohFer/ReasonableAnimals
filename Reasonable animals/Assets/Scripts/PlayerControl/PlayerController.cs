using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Public variables

    public float CameraSpeed = 5f; // PerSecond

    //
    int CurrentCooler = 0;

    //Camera movement

    void CameraControl()
    {
        //Get player input as vector
        Vector3 CameraVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            CameraVelocity += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CameraVelocity += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            CameraVelocity += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CameraVelocity += Vector3.back;
        }

        //Apply offset to camera and text if new position is within bounds
        Transform camTrans = Camera.main.transform;
        Vector3 offset = Utils.GetCameraZOffset(Camera.main) * Vector3.forward;
        Vector3 camFocus = camTrans.position - offset;
        
        camFocus = camFocus + CameraVelocity * camTrans.position.y * 0.4f * CameraSpeed * Time.deltaTime;

        if(camFocus.x < OfficeArchitect.CornerBL.x)
        {
            camFocus.x = OfficeArchitect.CornerBL.x;
        }
        else if(camFocus.x > OfficeArchitect.CornerTR.x)
        {
            camFocus.x = OfficeArchitect.CornerTR.x;
        }
        if (camFocus.z < OfficeArchitect.CornerBL.y)
        {
            camFocus.z = OfficeArchitect.CornerBL.y;
        }
        else if (camFocus.z > OfficeArchitect.CornerTR.y)
        {
            camFocus.z = OfficeArchitect.CornerTR.y;
        }

        //Set new camera position
        camTrans.position = camFocus + offset;
    }
    void SetCameraPosition(Vector3 WorldPoint)
    {
        Transform camTrans = Camera.main.transform;
        Vector3 offset = Utils.GetCameraZOffset(Camera.main) * Vector3.forward;
        camTrans.position = new Vector3(WorldPoint.x, camTrans.position.y, WorldPoint.z) + offset;
    }
    void CameraJump()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(Worker.CurrentlyHeld != null)
            {
                SetCameraPosition(Worker.CurrentlyHeld.OwnDesk.transform.position);
            }
            else
            {
                if(CurrentCooler >= Watercooler.coolers.Count)
                {
                    CurrentCooler = 0;
                }
                SetCameraPosition(Watercooler.coolers[CurrentCooler++].transform.position);
            }
        }
    }

    //Unity Callbacks

    private void Update()
    {
        CameraControl();
        CameraJump();
    }

}
