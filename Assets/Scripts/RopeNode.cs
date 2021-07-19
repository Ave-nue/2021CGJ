using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeNode : MonoBehaviour
{
    public Rigidbody2D followRoot;
    public float maxDistance;

    private Rigidbody2D m_rigidbody;
    private DistanceJoint2D m_distanceJoint2D;
    private Vector2 m_offset;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_distanceJoint2D = GetComponent<DistanceJoint2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (m_rigidbody)
        //{
        //    Vector2 dis = followRoot.position - m_rigidbody.position;
        //    if (dis.magnitude >= maxDistance)
        //    {
        //        m_rigidbody.position += dis * (dis.magnitude - maxDistance);
        //        //Vector2 radiusDec = (followRoot.position - m_rigidbody.position).normalized;
        //        //Vector2 tangentialDec = Quaternion.AngleAxis(90, Vector3.back) * radiusDec;
        //        //float angle = Vector2.Angle(tangentialDec, m_rigidbody.velocity);
        //        //m_rigidbody.velocity = tangentialDec * m_rigidbody.velocity.magnitude * Mathf.Cos(angle);
        //    }
        //    //Debug.Log(radiusDec + " " + tangentialDec + " " + m_rigidbody.velocity + " " + angle);
        //}

        if (m_distanceJoint2D && m_distanceJoint2D.distance != maxDistance)
        {
            m_distanceJoint2D.distance = maxDistance;
        }

        m_rigidbody.isKinematic = LevelManager.Instance.Hard;
        m_distanceJoint2D.enabled = !LevelManager.Instance.Hard;
        if (LevelManager.Instance.Hard == true)
        {
            m_rigidbody.MovePosition(MouseFollower.Instance.rigidbody.position + m_offset);
        }
        else
        {
            m_offset = m_rigidbody.position - MouseFollower.Instance.rigidbody.position;
        }
    }
}
