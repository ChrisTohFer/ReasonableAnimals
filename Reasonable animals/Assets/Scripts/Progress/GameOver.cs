using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //
    public static GameOver Singleton;
    public AudioSource LevelOverSound;

    private void OnEnable()
    {
        Singleton = this;
    }

    public void LevelLost()
    {
        MusicPlayer.Singleton.MusicSource.volume = 0f;
        StartCoroutine(ChangeSceneAfterTime(5f));
        LevelOverSound.Play();
    }
    private IEnumerator ChangeSceneAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
