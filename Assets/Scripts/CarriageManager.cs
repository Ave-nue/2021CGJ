using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageManager : MonoBehaviour
{
    private static CarriageManager m_instance;
    public static CarriageManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    private void Awake()
    {
        m_instance = this;
    }
}
