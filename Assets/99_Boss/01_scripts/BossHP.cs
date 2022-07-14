using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    private float bossHP = 1;

    public float GetbossHP()
    {
        return bossHP;
    }

    public void SetbossHP(float bossHP)
    {
        this.bossHP -= bossHP;
    }

    private void Update()
    {
        Debug.Log(bossHP);
        if(bossHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
