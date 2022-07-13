using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPMaster : MonoBehaviour
{
    [SerializeField] int _hp = 3;
    [SerializeField] int _MaxHP = 10;

    
    public void GetDamage(int value)
    {
        // 대충 에니메이션 trigger
        _hp -= value;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.Instance.Timer() == true || GameManager.Instance.TimeArrange() == 10)
        {
            _hp += 2;
            
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
