using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectOrder : PoolAble
{
    float _angle;
    float _orderTime;
    float _currnetTime;
    Vector3 dir;
    float speed = 20;
    
    bool isOrder = false;
    bool isAwaken = false;
    private Transform _player = null;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }

            return _player;
        }
    }

    private void OnEnable()
    {
        speed = 20;
        dir = Vector2.up;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(isAwaken == false)
            Awaken();
        
    }
    void Awaken()
    {
        transform.localPosition += transform.up * speed * Time.deltaTime;
        if (isOrder == false)
        {
            _currnetTime += Time.deltaTime;
            if (_currnetTime >= 1f)
                isOrder = true;
        }
        if (isOrder == true)
        {
            _orderTime += Time.deltaTime;
            if (Mathf.Abs(Player.position.x - transform.position.x) < _orderTime && Mathf.Abs(Player.position.y - transform.position.y) < _orderTime)
            {
                transform.DOKill();
                Debug.Log("しけし");
                this.transform.rotation = Quaternion.AngleAxis(_angle + 90, Vector3.forward);
                return;
            }
            _currnetTime = 0;
            _angle = Mathf.Atan2(transform.position.y - Player.position.y, transform.position.x - Player.position.x) * Mathf.Rad2Deg;
            //_angle = Mathf.Abs(_angle);
            transform.DORotate(new Vector3(0, 0, _angle + 90), 1f);
            dir.Normalize();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyHPMaster>())
        {
            _orderTime = 0;
            Debug.Log("中宜");
            _currnetTime = 0;
            isOrder = false;
            GameManager.Instance._Camera.DOShakePosition(0.5f, 1, 3, 90);
            speed = 0;
            isAwaken = true;
            transform.SetParent(collision.transform);
            transform.localPosition = transform.up *0.3f;
        }
    }
}
