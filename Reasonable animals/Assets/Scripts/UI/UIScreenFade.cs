using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIScreenFade : MonoBehaviour
{
    public static UIScreenFade Singleton;

    //Properties
    public float FadeInDelay = 1f;
    public float FadeInDuration = 2f;
    public float FadeOutDuration = 2f;

    //Reference
    public Image ScreenCover;

    //Events
    public UnityEvent FadeInCompleted;
    public UnityEvent FadeOutCompleted;


    //Fade methods

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(CFadeOut());
    }

    IEnumerator CFadeIn(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        while (ScreenCover.color.a > 0f)
        {
            Color c = ScreenCover.color;
            c.a -= FadeOutDuration * Time.deltaTime / FadeInDuration;
            ScreenCover.color = c;
            yield return new WaitForEndOfFrame();
        }
        ScreenCover.gameObject.SetActive(false);

        FadeInCompleted.Invoke();
    }

    IEnumerator CFadeOut()
    {
        ScreenCover.gameObject.SetActive(true);

        while (ScreenCover.color.a < 1f)
        {
            Color c = ScreenCover.color;
            c.a += FadeInDuration * Time.deltaTime / FadeOutDuration;
            ScreenCover.color = c;
            yield return new WaitForEndOfFrame();
        }

        FadeOutCompleted.Invoke();
    }

    //Unity Callbacks
    void Awake()
    {
        Singleton = this;
        StartCoroutine(CFadeIn(FadeInDelay));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeOut();
        }
    }



}
