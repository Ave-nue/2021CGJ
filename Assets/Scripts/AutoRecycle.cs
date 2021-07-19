using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoRecycle : MonoBehaviour
{
    public float delay;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        if (sprite)
        {
            sprite.DOFade(0f, delay);
        }

        Invoke("Recycle", delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recycle()
    {
        if (sprite)
        {
            sprite.DOKill();
        }
        Destroy(gameObject);
    }
}
