using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailManager : MonoBehaviour
{
    public List<Rail> rails;

    public bool IsBingo
    {
        get
        {
            foreach (var curTarget in rails)
            {
                if (!curTarget.IsBingo)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public int GetSavePerson()
    {
        int result = 0;
        foreach (var item in rails)
        {
            result += item.SavePerson;
        }

        return result;
    }
}
