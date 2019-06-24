using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{

    //
    enum PointOfInterest
    {
        DESK,
        WATER_COOLER
    }

    //Static
    static public List<Worker> Workers;
    public static int NumberAtWork
    {
        get
        {
            if(Workers == null)
            {
                Workers = new List<Worker>();
            }
            int n = 0;
            foreach(Worker w in Workers)
            {
                if (w.AtDesk)
                    ++n;
            }

            return n;
        }
    }
    public static float WorkProportion
    {
        get
        {
            if(Workers.Count == 0)
            {
                return 0f;
            }
            else
                return (float)NumberAtWork / Workers.Count;
        }
    }
    public static Worker CurrentlyHeld = null;

    //References
    public Desk OwnDesk;
    public Watercooler Cooler;

    //public properties
    public float MinWorkTime = 5f;
    public float MaxWorkTime = 15f;
    public float WalkSpeed = 5f;
    public float DeskStopDistance = 1f;
    public float CoolerStopDistance = 3f;
    public float TalkTime = 10f;

    public AudioSource AngryMeow;
    public AudioSource Complaining;
    public Animator AnimatorRef;
    //
    bool AtDesk = true;
    bool AtCooler = false;
    bool PickedUp = false;
    float Interest = 0f;

    Vector3 MoveTarget;
    PointOfInterest MoveInterest = PointOfInterest.DESK;

    //Methods

    void ReturnToDesk()
    {
        AtCooler = false;
        if (Cooler != null)
        {
            Cooler.RemoveWorker(this);
        }
        MoveTarget = OwnDesk.WorkLocation.transform.position;
        MoveInterest = PointOfInterest.DESK;
        Interest = Random.Range(MinWorkTime, MaxWorkTime);

        AnimatorRef.SetBool("Moving", true);
    }
    void GoToWaterCooler(Watercooler wc)
    {
        AtDesk = false;
        if (Cooler != null)
        {
            Cooler.RemoveWorker(this);
        }
        Cooler = wc;
        MoveInterest = PointOfInterest.WATER_COOLER;
        MoveTarget = wc.transform.position;

        Interest = TalkTime;
        AnimatorRef.SetBool("Moving", true);
    }
    //Returns the closest water cooler
    Watercooler GetNearestCooler()
    {
        if(Watercooler.coolers.Count > 0)
        {
            Watercooler nearest = Watercooler.coolers[0];
            float nearestDistance = Vector3.Distance(nearest.transform.position, transform.position);
            foreach(Watercooler wc in Watercooler.coolers)
            {
                float distance = Vector3.Distance(wc.transform.position, transform.position);
                if (distance < nearestDistance)
                {
                    nearest = wc;
                    nearestDistance = distance;
                }
            }

            return nearest;
        }
        else
            return null;
    }

    void LoseInterest()
    {
        if (MoveInterest == PointOfInterest.DESK)
        {
            GoToWaterCooler(GetNearestCooler());
        }
        else if (MoveInterest == PointOfInterest.WATER_COOLER)
        {
            ReturnToDesk();
        }
    }


    void PickUp()
    {
        PickedUp = true;
        OwnDesk.LightRef.EnableLight(true);
        CurrentlyHeld = this;
        AtDesk = false;
        AtCooler = false;

        Complaining.Play();
        Complaining.time = 1f;
        AngryMeow.Play();
        AnimatorRef.SetBool("Moving", true);

        UICursorManager.SetCursorPickup();
    }
    void Drop()
    {
        PickedUp = false;
        transform.position = Utils.MouseWorldPosition(0f);
        OwnDesk.LightRef.EnableLight(false);

        Watercooler nearbyCooler = GetNearestCooler();
        if (Vector3.Distance(transform.position, OwnDesk.WorkLocation.transform.position) <
            Vector3.Distance(transform.position, nearbyCooler.transform.position))
        {
            ReturnToDesk();
        }
        else
        {
            GoToWaterCooler(nearbyCooler);
        }
        CurrentlyHeld = null;

        Complaining.Stop();

        UICursorManager.SetCursorHover();
    }

    //Unity callbacks

    //Set initial objective and values
    private void OnEnable()
    {
        if(Workers == null)
        {
            Workers = new List<Worker>();
        }
        Workers.Add(this);

        AtDesk = true;
        Interest = Random.Range(-TalkTime, MaxWorkTime);

        MoveTarget = transform.position;
    }
    private void OnDisable()
    {
        Workers.Remove(this);
    }

    //Move towards target if present
    private void Update()
    {
        if (PickedUp)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Drop();
            }
            else
            {
                transform.position = Utils.MouseWorldPosition(4f) - Vector3.up * 2f;
            }
        }
        else
        {
            Vector3 Displacement = MoveTarget - transform.position;
            if (MoveInterest == PointOfInterest.DESK && Displacement.magnitude > DeskStopDistance)
            {
                transform.position += Displacement.normalized * WalkSpeed * Time.deltaTime;
                transform.LookAt(transform.position + Displacement);
            }
            else if (MoveInterest == PointOfInterest.DESK)
            {
                AtDesk = true;
                transform.LookAt(OwnDesk.FocusLocation.transform);
                AnimatorRef.SetBool("Moving", false);

                if (Interest <= 0f)
                {
                    LoseInterest();
                }
                else
                {
                    Interest -= Time.deltaTime;
                }
            }
            else if (MoveInterest == PointOfInterest.WATER_COOLER & Displacement.magnitude > CoolerStopDistance)
            {
                transform.position += Displacement.normalized * WalkSpeed * Time.deltaTime;
                transform.LookAt(transform.position + Displacement);
            }
            else if (MoveInterest == PointOfInterest.WATER_COOLER)
            {
                if (!AtCooler)
                {
                    Cooler.AddWorker(this);
                    AtCooler = true;
                }
                else
                {

                }
                transform.LookAt(Cooler.ConversationPosition);
                AnimatorRef.SetBool("Moving", false);

                if (Interest <= 0f)
                {
                    LoseInterest();
                }
                else
                {
                    Interest -= Time.deltaTime;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        PickUp();
    }

}
