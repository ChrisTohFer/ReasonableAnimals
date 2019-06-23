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
    //

    private void FixedUpdate()
    {
        MoneyCount.text = "Money: £" + SharesTracker.Singleton.CurrentMoney.ToString("F2");
        WorkerCount.text = "Workers: " + Worker.NumberAtWork + "/" + Worker.Workers.Count;
        NextLevelMoney.text = "Target: £" + SharesTracker.Singleton.Target.ToString("F2");
    }
}
