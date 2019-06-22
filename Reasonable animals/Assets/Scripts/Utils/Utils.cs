using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Utils
{
    public static float GetCameraZOffset(Camera c)
    {
        return c.transform.position.y / Mathf.Sin(c.transform.rotation.eulerAngles.x);
    }
}
