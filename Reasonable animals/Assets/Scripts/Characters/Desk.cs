using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    //Static references
    public static List<Desk> desks;


    //Unity callbacks

    private void OnEnable()
    {
        desks.Add(this);
    }
    private void OnDisable()
    {
        desks.Remove(this);
    }

}
