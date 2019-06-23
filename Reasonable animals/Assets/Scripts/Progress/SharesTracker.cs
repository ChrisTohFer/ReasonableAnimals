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
    public float WorkerUpkeep = .20f;
    public float WorkerRevenue = .5f;

    //
    bool tracking = false;
    float currentValue;

    //Method

    public void StartTracking()
    {
        tracking = true;
        currentValue = StartingValuePerWorker * Worker.Workers.Count;
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

            if(currentValue > TargetPerWorker * Worker.Workers.Count)
            {
                Debug.Log("Value reached");
                UIScreenFade.Singleton.FadeOut();
            }
        }
    }

}
