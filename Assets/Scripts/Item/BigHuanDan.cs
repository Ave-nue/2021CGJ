using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHuanDan : MonoBehaviour
{
    public AudioEffect reviveAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.layer == 9)
        {
            DoThat();
        }
    }

    public void DoThat()
    {
        AudioManager.Instance.PlayAudio(reviveAudio.clip, reviveAudio.volume);
        foreach (var curPerson in LevelManager.Instance.allPersons)
        {
            curPerson.Revive();
        }

        Destroy(gameObject);
    }
}
