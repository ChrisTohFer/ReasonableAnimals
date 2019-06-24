using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerConversation : MonoBehaviour
{
    //
    public float MaxDistance = 15f;
    public float MinDistance = 5f;
    public AudioSource Conversation;

    private void Update()
    {
        Vector3 mousePos = Utils.MouseWorldPosition(0f);

        float distance = MaxDistance;
        for(int i = 0; i < Watercooler.coolers.Count; ++i)
        {
            if(Watercooler.coolers[i].NumberOfWorkers() >= 2)
                distance = Mathf.Min(Vector3.Distance(Watercooler.coolers[i].transform.position, mousePos), distance);
        }

        Conversation.volume = Mathf.Clamp(distance / (MinDistance - MaxDistance) - MaxDistance / (MinDistance - MaxDistance), 0f, 1f);

    }


}
