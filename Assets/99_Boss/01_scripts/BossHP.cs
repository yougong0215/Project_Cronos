using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHP : MonoBehaviour
{
     private float bossHP = 2000;
    Word _damageUI;
    public float GetbossHP()
    {
        return bossHP;
    }

    public void SetbossHP(float bossHP)
    {
        this.bossHP -= bossHP;
        _damageUI = PoolManager.Instance.Pop("DamageText") as Word;
        _damageUI.transform.position = transform.position;
        _damageUI.ShowText(bossHP);

    }

    private void Update()
    {
        Debug.Log(bossHP);
        if(bossHP <= 0)
        {
            SceneManager.LoadScene("GameClear");
            Destroy(gameObject);
        }
    }
}
