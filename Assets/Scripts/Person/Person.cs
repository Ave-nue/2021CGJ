using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public GameObject DeadPrefab;
    public GameObject RevivePrefab;
    public List<AudioEffect> deadAudios = new List<AudioEffect>();
    public List<AudioEffect> reviveAudios = new List<AudioEffect>();

    private Animator m_animator;
    private bool m_dead;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        LevelManager.Instance.allPersons.Add(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 8 
            || collision.collider.gameObject.layer == 9)
        {
            Dead(collision.collider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8
            || collision.gameObject.layer == 9)
        {
            Dead(collision.attachedRigidbody);
        }
    }

    public void Dead(Collider2D killer)
    {
        if (m_dead)
            return;
        AudioManager.Instance.PlayRandomAudio(deadAudios);
        GameObject newEffect = Instantiate(DeadPrefab, transform.position, Quaternion.AngleAxis(Vector2.Angle(Vector2.right, killer.attachedRigidbody.velocity), Vector3.back));
        m_animator.SetBool("Dead", true);
        m_dead = true;
        LevelManager.Instance.DeadPerson++;
    }

    public void Dead(Rigidbody2D killer)
    {
        if (m_dead)
            return;
        AudioManager.Instance.PlayRandomAudio(deadAudios);
        GameObject newEffect = Instantiate(DeadPrefab, transform.position, Quaternion.AngleAxis(Vector2.Angle(Vector2.right, killer.velocity), Vector3.back));
        m_animator.SetBool("Dead", true);
        m_dead = true;
        LevelManager.Instance.DeadPerson++;
    }

    public void Revive()
    {
        if (!m_dead)
            return;
        AudioManager.Instance.PlayRandomAudio(reviveAudios);
        GameObject newEffect = Instantiate(RevivePrefab, transform.position, Quaternion.identity);
        m_animator.SetBool("Dead", false);
        m_dead = false;
        LevelManager.Instance.DeadPerson--;
    }
}
