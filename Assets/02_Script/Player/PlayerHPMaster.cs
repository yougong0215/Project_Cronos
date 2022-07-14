using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHPMaster : MonoBehaviour
{
    [SerializeField] float _hp = 10;
    [SerializeField] float _MaxHP = 10;
    float currentTime = 0;
    bool _hpRefill =false;
    bool _damaged =false;
    [SerializeField] AudioClip _au;
    bool guardTime = true;
    [SerializeField] Image _HPUI;
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
                GameManager.Instance.SoundPlay(_au);
                _damaged = true;
                currentTime = 0;
                _hp -= value;
            }

        }
    }
    private void Start()
    {
        guardTime = false;
    }
    public bool GetDamaged()
    {
        return _damaged;
    }

    public bool a =false;
    void Update()
    {
        _HPUI.fillAmount = (_hp / 10);

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
            a = true;
            SceneManager.LoadScene("GameClear");
            Debug.Log("쥬금");
        }
        if(GameManager.Instance.Timer() == true && _hpRefill == false)
        {
            _hp += 2;
            _hpRefill = true;
        }
        if (GameManager.Instance.Timer() == false )
        {
            _hpRefill = false;
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
