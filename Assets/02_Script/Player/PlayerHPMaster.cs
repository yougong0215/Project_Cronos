using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPMaster : MonoBehaviour
{
    [SerializeField] int _hp = 3;
    [SerializeField] int _MaxHP = 10;
    float currentTime = 0;
    bool _hpRefill =false;
    bool _damaged =false;

    public void GetDamage(int value)
    {
        // 대충 에니메이션 trigger
        if (_damaged ==false)
        {
            _damaged = true;
            currentTime = 0;
            _hp -= value;
        }
    }
    public bool GetDamaged()
    {
        return _damaged;
    }


    void Update()
    {
        if(currentTime >= 0.5f)
        {
            _damaged = false;
        }
        currentTime += Time.deltaTime;
        if(_hp <= 0)
        {
            Debug.Log("쥬금");
        }
        if((GameManager.Instance.Timer() == true || GameManager.Instance.TimeArrange() == 10) && _hpRefill == false)
        {
            _hp += 2;
            _hpRefill = true;
        }
        else
        {
            _hpRefill = true;
        }

        if (_hp >= _MaxHP)
        {
            _hp = _MaxHP;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Item>())
        {

        }
    }
}
