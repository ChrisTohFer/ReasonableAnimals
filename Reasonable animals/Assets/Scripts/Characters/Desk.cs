using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    //Static references
    public static List<Desk> desks;

    //
    public GameObject WorkLocation;
    public GameObject FocusLocation;


    //Unity callbacks

    private void OnEnable()
    {
        if(desks == null)
        {
            desks = new List<Desk>();
        }

        desks.Add(this);
    }
    private void OnDisable()
    {
        desks.Remove(this);
    }

}
