using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    private float bossHP = 20;

    public float GetbossHP()
    {
        return bossHP;
    }

    public void SetbossHP(float bossHP)
    {
        this.bossHP = bossHP;
    }
}
