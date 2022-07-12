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
    bool isPush = false;

    public Vector3 PlayerCamPos
    {
        get
        {
            return new Vector3(Player.position.x, Player.position.y + 2, -10);
        }
    }

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

    private Transform _enemy = null;
    public Transform Enemy
    {
        get
        {
            if (_enemy == null)
            {
                _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
            }

            return _enemy;
        }
    }

    private void OnEnable()
    {
        speed = 20;
        dir = Vector2.up;
        isOrder = false;
        isAwaken = false;
        isPush = false;
        _currnetTime = 0;
        _orderTime = 0;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isAwaken == false)
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
            if (Mathf.Abs(Enemy.position.x - transform.position.x) < _orderTime && Mathf.Abs(Enemy.position.y - transform.position.y) < _orderTime)
            {
                transform.DOKill();
                Debug.Log("¤·¤±¤·");
                this.transform.rotation = Quaternion.AngleAxis(_angle + 90, Vector3.forward);
                return;
            }
            _currnetTime = 0;
            _angle = Mathf.Atan2(transform.position.y - Enemy.position.y, transform.position.x - Enemy.position.x) * Mathf.Rad2Deg;
            //_angle = Mathf.Abs(_angle);
            transform.DORotate(new Vector3(0, 0, _angle + 90), 1f);
            dir.Normalize();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHPMaster>())
        {
            _orderTime = 0;
            Debug.Log("Ãæµ¹");
            _currnetTime = 0;
            isOrder = false;
            collision.GetComponent<EnemyHPMaster>().GetDamage(1);
            speed = 0;
            isAwaken = true;
            transform.SetParent(collision.transform);
            //transform.localPosition = transform.up * 0.3f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHPMaster>())
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("»Ì±â");
                isPush = true;
                transform.localPosition = transform.up * -1.3f;
                StartCoroutine(Push());
                collision.GetComponent<EnemyHPMaster>().GetDamage(3);
            }
            if (isPush == false)
            {
                transform.position = collision.transform.position;
            }
        }
    }

    IEnumerator Push()
    {
        yield return new WaitForSeconds(0.3f);
        PoolManager.Instance.Push(this);
    }
}
