using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class Word : PoolAble
{
    [SerializeField]TextMesh tmp;
    float _currentTime = 0;
    // Start is called before the first frame update

    public void ShowText(float Damaged)
    {
        _currentTime = 0;
        if (Damaged < 1000 && Damaged >= 500)
        {
            tmp.color = new Color(255, 0, 255);
            tmp.text = $"{Damaged.ToString("F1")}";
            tmp.characterSize = 2f;
            tmp.offsetZ = -9f;
        }
        if (Damaged >= 1000)
        {
            tmp.color = new Color(255, 0, 0);
            tmp.text = $"{Damaged.ToString("F1")}";
            tmp.characterSize = 2f;
            tmp.offsetZ = -8f;
        }
        if( Damaged < 500)
        {
            tmp.color = new Color(250, 200, 0);
            tmp.text = $"{Damaged.ToString("F1")}";
            tmp.characterSize = 0.8f;
            tmp.offsetZ = -7f;
        }
        transform.DOMove(new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 1.5f), 0), 0.3f)
            .OnComplete(() => {
            transform.DOMoveY(transform.position.y - 0.3f, 1f).OnComplete(() => { PoolManager.Instance.Push(this); });
        });
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, -14);
        if(_currentTime >= 1.5f)
        {
            PoolManager.Instance.Push(this);
        }
    }
}
