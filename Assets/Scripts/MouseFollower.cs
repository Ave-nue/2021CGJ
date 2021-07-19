using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private static MouseFollower m_instance;
    public static MouseFollower Instance
    {
        get
        {
            return m_instance;
        }
    }

    public Rigidbody2D rigidbody;

    private void Awake()
    {
        m_instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.Pause)
        {
            rigidbody.velocity = Vector2.up * 10f;
        }
        else
        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rigidbody.MovePosition(mouseWorldPoint);
            //rigidbody.velocity = mouseWorldPoint - transform.position;
            //rigidbody.velocity *= 30f;
        }
    }
}
