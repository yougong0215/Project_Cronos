using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPMaster : MonoBehaviour
{
    [SerializeField] float _hp = 3;
    [SerializeField] float _MaxHP = 10;
    float currentTime = 0;
    bool _hpRefill =false;
    bool _damaged =false;
    bool guardTime = true;
    public void GetDamage(float value)
    {
        // 대충 에니메이션 trigger
        if (guardTime == true)
        {
            _hp -= (value / 2);
        }
        else
        {
            if (_damaged == false)
            {
                _damaged = true;
                currentTime = 0;
                _hp -= value;
            }

        }
    }
    public bool GetDamaged()
    {
        return _damaged;
    }


    void Update()
    {

        if(Input.GetKey(KeyCode.LeftShift))
        {
            guardTime = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            guardTime = false;
        }
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
