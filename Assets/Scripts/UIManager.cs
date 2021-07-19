using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    public static UIManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    public GameObject uiPass;
    public GameObject uiPause;
    public Text txtPass;
    public Text txtScore;
    public List<AudioEffect> newLevelAudios = new List<AudioEffect>();
    public List<AudioEffect> passAudios = new List<AudioEffect>();

    private void Awake()
    {
        m_instance = this;
        AudioManager.Instance.PlayRandomAudio(newLevelAudios);
    }

    void Update()
    {
        txtScore.text = LevelManager.Instance.Score.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelManager.Instance.Pause)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ShowPass()
    {
        if (uiPass.gameObject.activeSelf)
            return;

        AudioManager.Instance.PlayRandomAudio(passAudios);
        txtPass.text = "厉害啦，你拯救了 " + LevelManager.Instance.Score + " 个人！";
        LevelManager.Instance.Pause = true;
        uiPass.gameObject.SetActive(true);
    }

    public void Pause()
    {
        LevelManager.Instance.Pause = true;
        uiPause.gameObject.SetActive(true);
    }

    public void Continue()
    {
        uiPause.gameObject.SetActive(false);
        LevelManager.Instance.Pause = false;
    }

    public void OnRetryBtn()
    {
        LevelManager.Instance.Retry();
    }

    public void OnNextBtn()
    {
        LevelManager.Instance.GoNext();
    }

    public void OnBackBtn()
    {
        LevelManager.Instance.BackHome();
    }
}
