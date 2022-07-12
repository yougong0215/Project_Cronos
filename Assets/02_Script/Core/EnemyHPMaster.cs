using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPMaster : PoolAble
{
    enum MonsterCode
    {
        Test = 1,
        Papuyrus = 2
    }


    Word _damageUI;
    [SerializeField] protected List<Vector4> _timeLeaf1;
    protected List<Sprite> _timeLeaf2;
    // x = 위치
    // y = 위치
    // z = 체력
    [SerializeField] int _hp;
    [SerializeField] Vector2 vector2;
    Rigidbody2D rb;
    float currentTime = 0;

    [SerializeField] int _monsterType = 1;
    bool _bTimereaf = false;
    int _timecode = 0;
    MonsterCode _monsterCode;

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
        rb = GetComponent<Rigidbody2D>();
        for (int i = 0; i < 300; i++)
        {
            _timeLeaf1.Add(new Vector4(transform.position.x, transform.position.y, _hp, transform.localEulerAngles.z));
        }
    }
    private void TimeSave()
    {
        if (currentTime >= 0.01f)
        {
            currentTime = 0;
            for (int i = 299; i > 0; i--)
            {
                if (i <= 0)
                    break;
                _timeLeaf1[i] = _timeLeaf1[i - 1];
            }
            _timeLeaf1[0] = new Vector4(transform.position.x, transform.position.y, _hp, transform.rotation.z);
        }
    }
    private void TimeControl()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (_bTimereaf == false)
            {


                rb.gravityScale = 1;
                _bTimereaf = true;
            }
            else if (_bTimereaf == true)
            {

                rb.gravityScale = 1;
                _bTimereaf = false;
                _hp -= TimeDamager;
                Debug.Log(_hp);
                TimeDamager = 0;
            }
        }
    }
    private void TimeLeaf()
    {
        if (_bTimereaf == false)
        {
            MonsterAIMove();
                TimeSave();
            _timecode = 0;
        }
        if (_bTimereaf == true)
        {
            if (currentTime > 0.01f)
            {
                currentTime = 0;
                if (_timecode > 299)
                {
                    rb.gravityScale = 1;
                    _bTimereaf = false;
                    return;
                }

                _timecode++;
                _hp = (int)_timeLeaf1[_timecode - 1].z;
                transform.position = _timeLeaf1[_timecode - 1];
                transform.localEulerAngles = new Vector3(0, 0, _timeLeaf1[_timecode - 1].w);
                rb.velocity = Vector3.zero;
                rb.gravityScale = 0;
            }
        }
    }
    void Update()
    {
        if (_hp < 0)
        {
            Debug.Log(_hp);
        }
        TimeControl();
        TimeLeaf();
        if(transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
                currentTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Debug.Log(_bTimereaf);
    }
    float _currentTime2 = 0;
    /// <summary>
    /// 0이거나 1이거나
    /// </summary>
    int _canMove = 1;

    public void SetMove(int value)
    {
        _canMove = value;
    }

    private void MonsterAIMove()
    {
        switch (_monsterType)
        {
            case (int)MonsterCode.Test:
                _currentTime2 += Time.deltaTime;
                transform.position -= new Vector3(Mathf.Sin(_currentTime2), 0, 0) * 4 * _canMove * Time.deltaTime;
                break;
            case (int)MonsterCode.Papuyrus:
                break;
        }
    }
    private void MonsterAIAttack()
    {
        switch (_timecode)
        {
            case (int)MonsterCode.Test:
                break;
            case (int)MonsterCode.Papuyrus:
                break;
        }
    }
    int TimeDamager = 0;

    public void GetDamage(int damage)
    {
        _damageUI = PoolManager.Instance.Pop("DamageText") as Word;
        _damageUI.transform.position = transform.position;
        _damageUI.ShowText(damage);

        if (_bTimereaf == false)
            _hp -= damage;
        if (_bTimereaf == true)
        {
            TimeDamager += damage;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


    }
}