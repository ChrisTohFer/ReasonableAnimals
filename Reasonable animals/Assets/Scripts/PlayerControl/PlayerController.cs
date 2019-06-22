using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Public variables

    public float CameraSpeed = 5f; // PerSecond

    //Camera movement

    void CameraControl()
    {
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

        Transform camTrans = Camera.main.transform;
        Vector3 offset = camTrans.position.y / Mathf.Sin(camTrans.rotation.eulerAngles.x) * Vector3.forward;
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
            Debug.Log("Bracket1");
            camFocus.z = OfficeArchitect.CornerBL.y;
        }
        else if (camFocus.z > OfficeArchitect.CornerTR.y)
        {
            Debug.Log("Bracket2");
            camFocus.z = OfficeArchitect.CornerTR.y;
        }

        camTrans.position = camFocus + offset;
    }


    //Unity Callbacks

    private void Update()
    {
        CameraControl();
    }

}
