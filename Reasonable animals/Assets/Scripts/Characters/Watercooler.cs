using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watercooler : MonoBehaviour
{
    //Static references
    public static List<Watercooler> coolers;


    //Unity callbacks

    private void OnEnable()
    {
        coolers.Add(this);
    }
    private void OnDisable()
    {
        coolers.Remove(this);
    }
}
