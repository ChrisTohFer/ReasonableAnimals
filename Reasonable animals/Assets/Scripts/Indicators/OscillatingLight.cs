using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingLight : MonoBehaviour
{

    //Public properties

    public Light LightRef;
    public float MinValue = 2f;
    public float MaxValue = 4f;
    public float Period = 2f;

    //variables
    float Phase = 0f;

    //Methods

    public void EnableLight(bool value)
    {
        LightRef.enabled = value;
    }

    //Unity callbacks
    
    void Update()
    {
        Phase += 2f * Mathf.PI * Time.deltaTime / Period;

        float midPoint = (MaxValue + MinValue) / 2f;
        float amplitude = MaxValue - MinValue;

        LightRef.intensity = midPoint + amplitude * Mathf.Sin(Phase);
    }
}
