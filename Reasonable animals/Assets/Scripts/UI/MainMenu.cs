using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static float MasterVolume = .5f;
    public UnityEngine.UI.Slider Slider;
    
    public void StartGame()
    {
        UIScreenFade.Singleton.FadeOut();
        OfficeArchitect.OfficeWidth = 3;
        OfficeArchitect.OfficeLength = 2;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        Slider.value = MasterVolume;
    }
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        MasterVolume = value;
    }

}
