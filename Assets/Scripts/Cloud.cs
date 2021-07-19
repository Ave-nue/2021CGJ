using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float xOffset = Random.Range(-10f, 10f);
        transform.DOLocalMoveX(transform.position.x + xOffset, xOffset * 3f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
