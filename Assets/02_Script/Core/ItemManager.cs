using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    bool _GiBal= false; // ����� �߰���
    bool _JungBokJa = false; // ������
    bool _Reverse = false ; // �һ��� ����
    bool _Liandri = false; //���ȵ帮
    bool _Flying = false; // Ȱ��

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {

        }
    }
}
