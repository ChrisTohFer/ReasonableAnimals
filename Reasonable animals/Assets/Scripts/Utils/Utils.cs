using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Utils
{
    public static float GetCameraZOffset(Camera c)
    {
        return c.transform.position.y / Mathf.Sin(c.transform.rotation.eulerAngles.x);
    }
    public static Vector3 MouseWorldPosition(float height)
    {
        Plane plane = new Plane(Vector3.up, Vector3.up * height);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        else
            return Vector3.zero;
    }
}
