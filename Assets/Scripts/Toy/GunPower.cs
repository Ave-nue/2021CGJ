using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPower : MonoBehaviour
{
    public float power;

    private Carriage m_target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_target = collision.collider.GetComponent<Carriage>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (m_target == collision.collider.GetComponent<Carriage>())
        {
            m_target = null;
        }
    }

    public void Fire()
    {
        if (m_target)
        {
            Rigidbody2D targetRigidbody2D = m_target.GetComponent<Rigidbody2D>();
            if (targetRigidbody2D)
                targetRigidbody2D.velocity += power * (targetRigidbody2D.position - new Vector2(transform.position.x, transform.position.y));
        }
    }
}
