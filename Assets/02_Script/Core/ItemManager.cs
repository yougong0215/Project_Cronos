using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    bool _GiBal= false; // 기발한 발걸음
    bool _JungBokJa = false; // 정복자
    bool _Reverse = false ; // 불사의 토템
    bool _Liandri = false; //리안드리
    bool _Flying = false; // 활공
    bool _Punching = true; // 공격횟수 추가

    int FirstItem = 0;
    int SecondItem = 0;

    void Update()
    {
        
    }

    public bool GetGibal()
    {
        return _GiBal;
    }
    public bool GetJungBokJa()
    {
        return _JungBokJa;

    }
    public bool GetReverse()
    {
        return _Reverse;
    }
    public bool GetLiandri()
    {
        return _Liandri;
    }
    public bool GetFlying()
    {
        return _Flying;
    }
    public bool GetPunching()
    {
        return _Punching;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {

        }
    }
}
