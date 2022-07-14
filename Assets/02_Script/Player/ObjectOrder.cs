using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;   

public class ObjectOrder : PoolAble
{
    float _angle;
    float _orderTime;
    float _currnetTime;
    Vector3 dir;
    float speed = 20;
    [SerializeField]Image UI;
    Word _damageUI;
    bool isOrder = false;
    bool isAwaken = false;
    bool isPush = false;
    bool isiPlayer = false;

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
    private void Awake()
    {
        UI = GameObject.FindGameObjectWithTag("Lin").GetComponent<Image>();
    }
    bool GetEnemy = false;
    private void OnEnable()
    {
        
        speed = 10;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        dir = Vector2.up;
        isOrder = true;
        isPush = false;
        isAwaken = true;
        _currnetTime = 0;
        _orderTime = 0;
        GetEnemy = false;
        StartCoroutine(Attacking());

    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (GetEnemy == false)
        {
            if (isAwaken == true)
            {
                transform.localPosition += transform.up * 0.4f * Time.deltaTime;
            }
            if (isAwaken == false)
                Awaken();
        }
        //transform.position = new Vector3(Mathf.Clamp(Player.position.x, -5, 5), Mathf.Clamp(Player.position.y, -5, 5));
    }
    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.8f);
        isAwaken = false;
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
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
        if (isOrder == true)
        {
            _orderTime += Time.deltaTime;
            if (Mathf.Abs(point.x - transform.position.x) < _orderTime && Mathf.Abs(point.y - transform.position.y) < _orderTime)
            {
                if(isiPlayer == false)
                    _currnetTime = 0;

                transform.DOKill();
                this.transform.rotation = Quaternion.AngleAxis(_angle + 90, Vector3.forward);
                return;
            }
            _angle = Mathf.Atan2(transform.position.y - point.y, transform.position.x - point.x) * Mathf.Rad2Deg;
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
            _damageUI = PoolManager.Instance.Pop("DamageText") as Word;
            _damageUI.transform.position = transform.position;
            _damageUI.ShowText(1);
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
                UI.fillAmount += 0.05f;
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                _damageUI = PoolManager.Instance.Pop("DamageText") as Word;
                _damageUI.transform.position = transform.position;
                _damageUI.ShowText(3);
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
