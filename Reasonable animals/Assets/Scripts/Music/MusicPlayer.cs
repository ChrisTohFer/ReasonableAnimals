using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //
    public static MusicPlayer Singleton;

    //
    public string MusicName;
    public AudioSource MusicIntro;
    public AudioSource MusicSource;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }
        else if (Singleton.MusicName == MusicName)
        {
            Destroy(this);
        }
        else
        {
            Destroy(Singleton);
            Singleton = this;
            DontDestroyOnLoad(this);
        }
    }

    private void FixedUpdate()
    {
        if (Singleton == this && !MusicSource.isPlaying && (MusicIntro == null || !MusicIntro.isPlaying))
            MusicSource.Play();
    }

    private void OnDestroy()
    {
        MusicSource.Stop();
        if (MusicIntro != null)
            MusicIntro.Stop();
    }

    public static void FadeOut()
    {
        Singleton.StartCoroutine(Singleton.CFadeOut(4f));
    }
    IEnumerator CFadeOut(float Duration)
    {
        while(MusicSource.volume > 0f)
        {
            MusicSource.volume -= Time.deltaTime / Duration;
            yield return new WaitForEndOfFrame();
        }
    }
}
