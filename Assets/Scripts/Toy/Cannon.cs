using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    public GunPower pusher;
    public Vector2 angleRange = new Vector2(30f, 70f);
    public float FireDelay;
    public bool usePreset;
    public float presetAngle;
    public float presetPower;
    public List<AudioEffect> fireAudios = new List<AudioEffect>();

    private Sequence m_readySequence;

    // Start is called before the first frame update
    void Start()
    {
        float angle = Random.Range(angleRange.x, angleRange.y);
        float power = Random.Range(14f, 36f);
        if (usePreset)
        {
            angle = presetAngle;
            power = presetPower;
        }
        pusher.power = power;

        m_readySequence = DOTween.Sequence();
        m_readySequence.Append(transform.DOLocalRotate(Vector3.forward * angle, angle / 10));
        m_readySequence.Append(pusher.transform.DOLocalMoveX(0f, 2f));
        m_readySequence.onComplete += () => {
            AudioManager.Instance.PlayRandomAudio(fireAudios);
            pusher.Fire();
        };
        m_readySequence.Pause();

        Invoke("Ready", FireDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ready()
    {
        m_readySequence.Play();
    }
}
