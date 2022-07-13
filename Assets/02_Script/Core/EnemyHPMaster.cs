using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPMaster : PoolAble
{
    enum MonsterCode
    {
        Defualt = 0,
        Test = 1,
        Papuyrus = 2
    }

    bool _Liandri = false;

    float _currentTime2 = 0;
    bool _Damaged = false;
    /// <summary>
    /// 0이거나 1이거나
    /// </summary>
    float _canMove = 1;
    float _MoveTimeArrange = 1;

    PurpleBullet _purpleBullet;
    Word _damageUI;
    [SerializeField] protected List<Vector4> _timeLeaf1;
    protected List<Sprite> _timeLeaf2;
    // x = 위치
    // y = 위치
    // z = 체력
    [SerializeField] float _hp;
    [SerializeField] Vector2 vector2;
    Rigidbody2D rb;
    float currentTime = 0;

    [SerializeField] int _monsterType = 1;
    int _timecode = 0;
    MonsterCode _monsterCode;

    float _attackTime = 0;

    bool _TimeDamaged;

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
        for (int i = 0; i < 30; i++)
        {
            _timeLeaf1.Add(new Vector4(transform.position.x, transform.position.y, _hp, transform.localEulerAngles.z));
        }
    }
    private void OnEnable()
    {
        _attackTime = 0;
        _currentTime2 = 0;
        currentTime = 0;
    }

    private void TimeSave()
    {
        if (currentTime >= 0.1f * GameManager.Instance.TimeArrange())
        {
            currentTime = 0;
            for (int i = 29; i > 0; i--)
            {
                if (i <= 0)
                    break;
                _timeLeaf1[i] = _timeLeaf1[i - 1];
            }
            _timeLeaf1[0] = new Vector4(transform.position.x, transform.position.y, _hp, transform.rotation.z);
        }
    }
    private void TimeLeaf()
    {
        if (GameManager.Instance.Timer() == false && GameManager.Instance.TimeArrange() != 10)
        {
            rb.gravityScale = 1 * GameManager.Instance.CanMove();
            MonsterAIMove();
            MonsterAIAttack();
            TimeSave();
            _timecode = 0;
            if (_TimeDamaged == true)
            {
                _TimeDamaged = false;
                _hp -= TimeDamager;
                _damageUI = PoolManager.Instance.Pop("DamageText") as Word;
                _damageUI.transform.position = transform.position;
                if (TimeDamager > 500)
                {
                    _damageUI.ShowText(TimeDamager);
                }


            }
        }
        if (GameManager.Instance.Timer() == true)
        {
            rb.gravityScale = 1 * GameManager.Instance.CanMove();
            _TimeDamaged = true;
            if (currentTime > 0.1f * GameManager.Instance.TimeArrange())
            {
                currentTime = 0;
                if (_timecode > 29)
                {
                    rb.gravityScale = 1;
                    return;
                }

                _timecode++;
                _hp = (int)_timeLeaf1[_timecode - 1].z;
                transform.position = _timeLeaf1[_timecode - 1];
                transform.localEulerAngles = new Vector3(0, 0, _timeLeaf1[_timecode - 1].w);
                rb.velocity = Vector3.zero;
                rb.gravityScale = 0;
            }

            Debug.Log(_hp);

        }
    }

    IEnumerator LiandriTick(float damaged)
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GetDamage(damaged);
        }
        _Liandri = false;
    }


    void Update()
    {

        if (_hp < 0)
        {

            transform.position = new Vector3(-1000, 1000);
            rb.gravityScale = 0;
            if (_timeLeaf1[0] == _timeLeaf1[29])
            {
                Debug.Log("여서 푸쉬");
            }
        }
        TimeLeaf();

        if (transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        currentTime += Time.deltaTime;
    }

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
                transform.position -= new Vector3(Mathf.Sin(_currentTime2), 0, 0) * 4 * GameManager.Instance.CanMove() * _canMove * Time.deltaTime;
                break;
            case (int)MonsterCode.Papuyrus:
                break;
        }
    }

    private void MonsterAIAttack()
    {
        _attackTime += Time.deltaTime;
        switch (_monsterType)
        {
            case (int)MonsterCode.Test:
                if (_attackTime >= 2f)
                {
                    _attackTime = 0;
                    _purpleBullet = PoolManager.Instance.Pop("BulletTest") as PurpleBullet;
                    _purpleBullet.Sex(GetComponent<Rigidbody2D>());
                    _purpleBullet.transform.position = transform.position;
                }
                break;
            case (int)MonsterCode.Papuyrus:
                break;
        }
    }
    float TimeDamager = 0;

    public void GetDamage(float damage)
    {
        _damageUI = PoolManager.Instance.Pop("DamageText") as Word;
        _damageUI.transform.position = transform.position;
        _damageUI.ShowText(damage);

        if (ItemManager.Instance.GetLiandri() == true && _Liandri == false)
        {
            _Liandri = true;
            StopCoroutine(LiandriTick(0.5f));
            StartCoroutine(LiandriTick(0.5f));
        }

        if (GameManager.Instance.Timer() == false || GameManager.Instance.TimeArrange() != 10)
            _hp -= damage;
        if (GameManager.Instance.Timer() == true || GameManager.Instance.TimeArrange() == 10)
        {
            TimeDamager += damage;
        }
    }

}