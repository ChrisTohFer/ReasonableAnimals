using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursorManager : MonoBehaviour
{
    public static UICursorManager Singleton;
    public Texture2D Hover;
    public Texture2D Pickup;

    public static void SetCursorPickup()
    {
        Cursor.SetCursor(Singleton.Pickup, Vector2.one / 2f, CursorMode.Auto);
    }
    public static void SetCursorHover()
    {
        Cursor.SetCursor(Singleton.Hover, Vector2.one / 2f, CursorMode.Auto);
    }

    private void OnEnable()
    {
        Singleton = this;
    }
    private void OnDestroy()
    {
        SetCursorHover();
    }
}
