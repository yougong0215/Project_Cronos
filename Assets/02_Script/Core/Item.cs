using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    enum Abil
    { 
        Gibalhan = 1,
        JungBokJa = 2,
        Reverse = 3,
        Liandri = 4,
        Flying = 5
    }
    [SerializeField] int itemNumber;

    void Update()
    {
        

    }

    public int ReturnItem()
    {
        return itemNumber;
    }
}
