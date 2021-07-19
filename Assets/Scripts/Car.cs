using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float acc;
    public float backDuration;
    public AudioEffect goonAudio;
    private Rigidbody2D m_rigidbody2D;
    private float m_goonTime;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_goonTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D.AddRelativeForce(Vector2.left * acc);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > m_goonTime 
            && (Time.time - Time.deltaTime) <= m_goonTime)
        {
            AudioManager.Instance.PlayAudio(goonAudio.clip, goonAudio.volume);
        }
        //m_rigidbody2D.velocity = (Time.time > m_goonTime ? Vector2.left : Vector2.right) * 3f;
        //m_rigidbody2D.AddForce((Time.time > m_goonTime ? Vector2.left : Vector2.right) * acc);
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.AddRelativeForce((Time.time > m_goonTime ? Vector2.left : Vector2.right) * acc);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            m_goonTime = Time.time + backDuration;
        }
    }
}
