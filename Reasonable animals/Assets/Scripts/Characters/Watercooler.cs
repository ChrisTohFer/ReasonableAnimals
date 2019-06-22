using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watercooler : MonoBehaviour
{
    //Static references
    public static List<Watercooler> coolers;

    //Private information
    List<Worker> Audience;

    //Returns the position of the conversation centre (of all workers talking at this cooler)
    public Vector3 ConversationPosition
    {
        get
        {
            Vector3 position = Vector3.zero;
            foreach(Worker w in Audience)
            {
                position += w.transform.position;
            }
            position /= Audience.Count;
            return position;
        }
    }

    //Methods
    public void AddWorker(Worker w)
    {
        if (Audience == null)
            Audience = new List<Worker>();
        Audience.Add(w);
    }
    public void RemoveWorker(Worker w)
    {
        if (Audience == null)
            Audience = new List<Worker>();
        Audience.Remove(w);
    }

    //Unity callbacks

    private void OnEnable()
    {
        if (coolers == null)
        {
            coolers = new List<Watercooler>();
        }
        coolers.Add(this);
    }
    private void OnDisable()
    {
        coolers.Remove(this);
    }
}
