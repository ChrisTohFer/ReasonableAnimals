using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextManager : MonoBehaviour
{

    //
    public static UITextManager Singleton;

    public TextMeshProUGUI MoneyCount;
    public TextMeshProUGUI WorkerCount;
    public TextMeshProUGUI NextLevelMoney;
    public TextMeshProUGUI HelpText;
    //

    static bool HelpTextActive = true;


    private void Awake()
    {
        HelpText.gameObject.SetActive(HelpTextActive);
    }
    private void FixedUpdate()
    {
        MoneyCount.text = "Money: £" + SharesTracker.Singleton.CurrentMoney.ToString("F2");
        WorkerCount.text = "Workers: " + Worker.NumberAtWork + "/" + Worker.Workers.Count;
        NextLevelMoney.text = "Target: £" + SharesTracker.Singleton.Target.ToString("F2");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            HelpTextActive = !HelpTextActive;
            HelpText.gameObject.SetActive(HelpTextActive);
        }
    }
}
