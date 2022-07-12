using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class Word : PoolAble
{
    [SerializeField]TextMesh tmp;
    // Start is called before the first frame update

    public void ShowText(int Damaged)
    {
        tmp.text = $"{Damaged}.0";
        transform.DOMove(new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 1.5f), 0), 0.3f)
            .OnComplete(() => {
            transform.DOMoveY(transform.position.y - 0.3f, 1f).OnComplete(() => { PoolManager.Instance.Push(this); });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
