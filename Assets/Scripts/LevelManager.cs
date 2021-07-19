using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager m_instance;
    public static LevelManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    public bool isCannonLevel = false;
    public List<RailManager> railMgrs;
    public List<Person> allPersons = new List<Person>();
    public int DeadPerson;
    public bool Hard;
    public string NextLevelScene;

    private float m_startTime;
    public bool IsPass
    {
        get
        {
            if (isCannonLevel)
            {
                return Time.time >= (m_startTime + 27f) || Locomotive.Instance?.CatchCannonCount >= 20;
            }

            foreach (var curRailMgr in railMgrs)
            {
                if (curRailMgr.IsBingo)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public int Score
    {
        get
        {
            return GetSavePerson() - DeadPerson;
        }
    }

    private bool m_pause;
    public bool Pause
    {
        set
        {
            m_pause = value;
            Time.timeScale = m_pause ? 0f : 1f;
        }
        get
        {
            return m_pause;
        }
    }

    private void Awake()
    {
        m_instance = this;
        m_startTime = Time.time;
        Pause = false;
        AudioManager.Instance.ChangeBGMVolume(0.3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()//放到LateUpdate防止有些UI没刷新
    {
        if (IsPass)
        {
            UIManager.Instance.ShowPass();
            //Debug.Log("过关啦，你拯救了 " + Locomotive.Instance.GetSavePerson() + "个人！");
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoNext()
    {
        SceneManager.LoadScene(NextLevelScene);
    }

    public void BackHome()
    {
        SceneManager.LoadScene("MainScene");
    }

    public int GetSavePerson()
    {
        int result = 0;
        foreach (var curRailMgr in railMgrs)
        {
            result += curRailMgr.GetSavePerson();
        }

        return result;
    }
}
