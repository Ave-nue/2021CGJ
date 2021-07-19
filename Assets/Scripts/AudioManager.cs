using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class AudioEffect
{
    public AudioClip clip;
    public int weight = 1;
    public float volume = 0.7f;
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_instance;
    public static AudioManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    private AudioSource[] m_audioSources;
    private int m_BGMIndex;
    private int m_curIndex;
    private bool m_isSoundOff;
    public bool SoundOff
    {
        set
        {
            if (m_isSoundOff != value)
            {
                for (int i = 0; i < m_audioSources.Length; i++)
                {
                    if (m_BGMIndex != i)
                    {
                        m_audioSources[i].enabled = !value;
                    }
                }
                if (value)
                {
                    m_audioSources[m_BGMIndex].Pause();
                }
                else
                {
                    m_audioSources[m_BGMIndex].Play();
                }
                m_isSoundOff = value;
            }
        }
        get
        {
            return m_isSoundOff;
        }
    }

    private void Awake()
    {
        if (m_instance)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;
        DontDestroyOnLoad(this);
        m_audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < m_audioSources.Length; i++)
        {
            if (m_audioSources[i].loop)
            {
                m_BGMIndex = i;
            }
            m_audioSources[i].volume = 0.7f;
        }
    }

    public void ChangeBGMVolume(float newVolume)
    {
        if (m_audioSources[m_BGMIndex].volume != newVolume)
            m_audioSources[m_BGMIndex].volume = newVolume;
    }

    public void PlayAudio(AudioClip audio, float volume)
    {
        if (m_curIndex == m_BGMIndex)
            m_curIndex = (++m_curIndex) % m_audioSources.Length;
        m_audioSources[m_curIndex].volume = volume;
        m_audioSources[m_curIndex].clip = audio;
        m_audioSources[m_curIndex].Play();
        m_curIndex = (++m_curIndex) % m_audioSources.Length;
    }

    public void PlayRandomAudio(List<AudioEffect> randomList)
    {
        int totalWeight = 0;
        foreach (var item in randomList)
        {
            totalWeight += item.weight;
        }
        int randomWeight = Random.Range(0, totalWeight) + 1;
        foreach (var item in randomList)
        {
            randomWeight -= item.weight;

            if (randomWeight <= 0)
            {
                PlayAudio(item.clip, item.volume);
                return;
            }
        }
    }
}
