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
    public float CurrentMoney;
    public float Target;

    public AudioSource LevelComplete;

    bool levelover = false;

    //Method

    public void StartTracking()
    {
        tracking = true;
        CurrentMoney = StartingValuePerWorker * Mathf.Pow(Worker.Workers.Count, 0.9f);
        Target = TargetPerWorker * Mathf.Pow(Worker.Workers.Count, 0.9f);
    }
    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
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
            CurrentMoney += WorkerRevenue * nWorking * Time.fixedDeltaTime;
            CurrentMoney -= WorkerUpkeep * Worker.Workers.Count * Time.fixedDeltaTime;

            if(CurrentMoney > Target && !levelover)
            {
                UIScreenFade.Singleton.FadeOut();
                LevelComplete.Play();
                levelover = true;
            }
        }
    }

}
