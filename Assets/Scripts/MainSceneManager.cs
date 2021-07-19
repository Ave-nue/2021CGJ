using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public Transform background;
    public Image imgAudioOn;
    public Image imgAudioOff;

    private float lastframeWidth;

    private void Awake()
    {
        Time.timeScale = 1f;
        lastframeWidth = Screen.width;
        background.localScale = new Vector3(lastframeWidth / 762f, lastframeWidth / 762f, 1);
    }

    private void Start()
    {
        AudioManager.Instance.ChangeBGMVolume(0.7f);
        imgAudioOn.enabled = !AudioManager.Instance.SoundOff;
        imgAudioOff.enabled = AudioManager.Instance.SoundOff;
    }

    private void Update()
    {
        if (Screen.width != lastframeWidth)
        {
            lastframeWidth = Screen.width;
            background.localScale = new Vector3(lastframeWidth / 762f, lastframeWidth / 762f, 1);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void CannonGame()
    {
        SceneManager.LoadScene("Level Cannon");
    }

    public void KillGame()
    {
        SceneManager.LoadScene("Level 6");
    }

    public void ChangeSoundState()
    {
        AudioManager.Instance.SoundOff = !AudioManager.Instance.SoundOff;
        imgAudioOn.enabled = !AudioManager.Instance.SoundOff;
        imgAudioOff.enabled = AudioManager.Instance.SoundOff;
    }
}
