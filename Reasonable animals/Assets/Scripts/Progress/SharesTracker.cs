using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharesTracker : MonoBehaviour
{
    //
    public static SharesTracker Singleton;

    //Public properties

    public float StartingValuePerWorker = 1.5f;
    public float TargetPerWorker = 5f;
    public float WorkerUpkeep = .35f;
    public float WorkerRevenue = .8f;
    public float BoundaryPowerScaling = 0.9f;

    //
    bool tracking = false;
    float currentValue;
    float target;

    //Method

    public void StartTracking()
    {
        tracking = true;
        currentValue = StartingValuePerWorker * Mathf.Pow(Worker.Workers.Count, 0.9f);
        target = TargetPerWorker * Mathf.Pow(Worker.Workers.Count, 0.9f);
    }

    //UnityCallbacks

    private void OnEnable()
    {
        Singleton = this;
    }
    private void FixedUpdate()
    {
        if(tracking)
        {
            int nWorking = Worker.NumberAtWork;
            currentValue += WorkerRevenue * nWorking * Time.fixedDeltaTime;
            currentValue -= WorkerUpkeep * Worker.Workers.Count * Time.fixedDeltaTime;

            Debug.Log(currentValue + ", " + TargetPerWorker * Worker.Workers.Count);

            if(currentValue > target)
            {
                Debug.Log("Value reached");
                UIScreenFade.Singleton.FadeOut();
            }
        }
    }

}
