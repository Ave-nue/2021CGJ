using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotive : MonoBehaviour
{
    private static Locomotive m_instance;
    public static Locomotive Instance
    {
        get
        {
            return m_instance;
        }
    }

    public int CatchCannonCount;

    public AudioEffect collidAudio;

    private List<Carriage> m_carriages = new List<Carriage>();

    private void Awake()
    {
        m_instance = this;
    }

    private void Update()
    {
        if (m_carriages.Count > 0 && Input.GetKey(KeyCode.Space))
        {
            m_carriages[m_carriages.Count - 1].Free();
            m_carriages.RemoveAt(m_carriages.Count - 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.PlayAudio(collidAudio.clip, collidAudio.volume);
        Carriage curCarriage = collision.collider.GetComponent<Carriage>();
        if (LevelManager.Instance && LevelManager.Instance.isCannonLevel && curCarriage)
        {
            LevelManager.Instance.DeadPerson -= curCarriage.GetAlivingPerson();
            Destroy(curCarriage.gameObject);
            CatchCannonCount++;
        }
        else if (curCarriage && !curCarriage.IsLocked)
        {
            curCarriage.Catch();
            curCarriage.transform.SetParent(transform);
            m_carriages.Add(curCarriage);
        }
    }

    public void LockCarriage(Carriage carriage)
    {
        m_carriages.Remove(carriage);
    }
}
