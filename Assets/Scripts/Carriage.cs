using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : MonoBehaviour
{
    public float deadSpeed;
    public float deadDeceleration;
    public Animator animator;
    public AudioEffect collidAudio;
    public List<AudioEffect> catchAudios = new List<AudioEffect>();
    public List<AudioEffect> freeAudios = new List<AudioEffect>();
    public List<AudioEffect> deadAudios = new List<AudioEffect>();

    private Rigidbody2D m_rigidbody;
    private Rail m_rail;
    private bool m_dead;
    private float m_lastVelocity;

    public bool IsLocked
    {
        get
        {
            return m_rail;
        }
    }

    private void Awake()
    {
        m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
    }

    public void Catch()
    {
        AudioManager.Instance.PlayRandomAudio(catchAudios);
        Destroy(m_rigidbody);
    }

    public void Free()
    {
        AudioManager.Instance.PlayRandomAudio(freeAudios);
        transform.SetParent(CarriageManager.Instance.transform);
        m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
    }

    public void LockToRail(Rail rail)
    {
        //AudioManager.Instance.PlayRandomAudio(catchAudios);
        transform.SetParent(rail.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        m_rail = rail;
        //m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
    }

    public void Dead()
    {
        AudioManager.Instance.PlayRandomAudio(deadAudios);
        m_dead = true;
        animator.SetBool("Dead", m_dead);
    }

    public int GetAlivingPerson()
    {
        if (m_dead)
        {
            return 0;
        }
        else
        {
            return 10;
        }
    }

    private void Update()
    {
        if (m_rigidbody)
            m_lastVelocity = m_rigidbody.velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.PlayAudio(collidAudio.clip, collidAudio.volume);
        if (!m_dead 
            && collision.collider.gameObject.layer == 10 
            && m_rigidbody 
            && ((m_lastVelocity - m_rigidbody.velocity.magnitude) > deadDeceleration 
            || m_rigidbody.velocity.magnitude > deadSpeed))
        {
            //Debug.Log(m_lastVelocity - m_rigidbody.velocity.magnitude);
            //Debug.Log(m_rigidbody.velocity.magnitude);
            Dead();
        }
    }
}
