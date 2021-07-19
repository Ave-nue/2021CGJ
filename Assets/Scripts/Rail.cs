using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public GameObject BingoNode;
    public GameObject NoBingoNode;
    public float bingoAngle;
    public float bingoDistance;
    public AudioEffect lockAudio;
    //public Target target1;
    //public Target target2;

    private Carriage m_lockCarriage;

    public bool IsBingo
    {
        get
        {
            return m_lockCarriage;
        }
    }

    public int SavePerson
    {
        get
        {
            if (m_lockCarriage)
                return m_lockCarriage.GetAlivingPerson();
            else
                return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.Instance.Pause)
        {
            BingoNode.SetActive(IsBingo);
            NoBingoNode.SetActive(!IsBingo);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_lockCarriage)
            return;

        Carriage curCarriage = collision.GetComponent<Carriage>();
        if (!curCarriage || curCarriage.IsLocked)
            return;

        float angle = Mathf.Abs(curCarriage.transform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z) % 180;
        if (angle >= 90f)
            angle = 180f - angle;

        if (angle > bingoAngle)
            return;

        float distance = (curCarriage.transform.position - transform.position).magnitude;
        if (distance > bingoDistance)
            return;

        Lock(curCarriage);
    }

    private void Lock(Carriage curCarriage)
    {
        m_lockCarriage = curCarriage;
        AudioManager.Instance.PlayAudio(lockAudio.clip, lockAudio.volume);
        curCarriage.LockToRail(this);
        Locomotive.Instance.LockCarriage(curCarriage);
    }
}
