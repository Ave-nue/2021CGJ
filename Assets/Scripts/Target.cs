using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool IsBingo
    {
        get
        {
            return m_targets.Count > 0;
        }
    }

    private List<GameObject> m_targets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_targets.Contains(collision.gameObject))
            return;
        m_targets.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_targets.Contains(collision.gameObject))
            m_targets.Remove(collision.gameObject);
    }

    public List<Carriage> GetCarriage()
    {
        List<Carriage> result = new List<Carriage>();

        foreach (var item in m_targets)
        {
            Carriage newCarriage = item.GetComponentInParent<Carriage>();
            if (newCarriage)
            {
                result.Add(newCarriage);
            }
        }

        return result;
    }
}
