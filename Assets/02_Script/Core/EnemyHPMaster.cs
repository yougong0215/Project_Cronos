using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPMaster : PoolAble
{
    enum MonsterCode
    {
        Defualt = 0,
        Test = 1,
        Papuyrus = 2,
        Enemy1 = 3
    }
    Vector3 dir = new Vector3(1, 0, 0);
    bool _Liandri = false;
    float TimeDamager = 0;
    float _currentTime2 = 0;
    bool _Damaged = false;
    bool _triggerDamaged = false;
    bool _die =false;
    /// <summary>
    /// 0이거나 1이거나
    /// </summary>
    float _canMove = 1;
    float _MoveTimeArrange = 1;

    PurpleBullet _purpleBullet;
    Word _damageUI;
    [SerializeField] protected List<Vector4> _timeLeaf1;
    protected Sprite[] _timeLeaf2 = new Sprite[30];
    SpriteRenderer _spi;

    // x = 위치
    // y = 위치
    // z = 체력
    [SerializeField] float OriginHP;
    float _hp;
    [SerializeField] Vector2 vector2;
    Rigidbody2D rb;
    float currentTime = 0;
    Animator _ani;
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
        _spi = GetComponent<SpriteRenderer>();
        _ani = GetComponent<Animator>();
        for (int i = 0; i < 30; i++)
        {
            _timeLeaf1.Add(new Vector4(transform.position.x, transform.position.y, _hp, transform.localEulerAngles.z));
            _timeLeaf2[i] = _spi.sprite;
        }

    }
    private void OnEnable()
    {
        _Damaged = false;
        _triggerDamaged = false;
        _die = false;
        _canMove = 1;
        _MoveTimeArrange = 1;
        _timecode = 0;
        _attackTime = 0;
        _currentTime2 = 0;
        _hp = OriginHP;
        _attackTime = 0;
        _TimeDamaged = false;
        dir = new Vector3(1, 0, 0);
        currentTime = 0;
        TimeDamager = 0;
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
                _timeLeaf2[i] = _timeLeaf2[i - 1];
            }
            _timeLeaf1[0] = new Vector4(transform.position.x, transform.position.y, _hp, transform.rotation.z);
            _timeLeaf2[0] = _spi.sprite;
        }
    }
    private void TimeLeaf()
    {
        
        _ani.speed = GameManager.Instance.CanMove();
        if (GameManager.Instance.Timer() == false && _timecode >= 29)
        {
            _ani.enabled = true;
            _ani.Rebind();
            _timecode = 0;
        }
        if (GameManager.Instance.Timer() == false && GameManager.Instance.TimeArrange() != 10)
        {
            TimeSave();
            if (_die == false)
            {
                rb.gravityScale = 1 * GameManager.Instance.CanMove();
                MonsterAIMove();
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
        }
        if (GameManager.Instance.Timer() == true)
        {
            _die = false;
            
            rb.gravityScale = 1 * GameManager.Instance.CanMove();
            _ani.StopPlayback();
            _ani.enabled = false;
            _TimeDamaged = true;
            if (currentTime > 0.1f * GameManager.Instance.TimeArrange())
            {
                currentTime = 0;
                if (_timecode >= 29)
                {
                    
                    _ani.enabled = true;
                    _ani.Rebind();
                    rb.gravityScale = 1;
                    return;
                }

                _timecode++;
                _hp = (int)_timeLeaf1[_timecode - 1].z;
                transform.position = _timeLeaf1[_timecode - 1];
                transform.localEulerAngles = new Vector3(0, 0, _timeLeaf1[_timecode - 1].w);
                _spi.sprite = _timeLeaf2[_timecode - 1];
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
            if (_die==false)
            {
                _ani.SetBool("Die", true);
            }
            _die = true;

            if (_timeLeaf1[0] == _timeLeaf1[29])
                {
                    PoolManager.Instance.Push(this);
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
                rb.AddForce(new Vector3(Mathf.Sin(_currentTime2), 0, 0) * 2 * GameManager.Instance.CanMove());
                
                if (_attackTime >= 2f)
                {
                    _attackTime = 0;
                    _purpleBullet = PoolManager.Instance.Pop("BulletTest") as PurpleBullet;
                    _purpleBullet.Sex(GetComponent<Rigidbody2D>());
                    _purpleBullet.transform.position = transform.position;
                }
                if (rb.velocity.x >= 0.1f)
                {
                    _ani.SetBool("Run", true);
                    _spi.flipX = true;
                }
                else if (rb.velocity.x < -0.1f)
                {
                    _ani.SetBool("Run", true);
                    _spi.flipX = false;
                }
                else
                {
                    _ani.SetBool("Run", false);
                }

                break;
            case (int)MonsterCode.Papuyrus:
                break;
            case (int)MonsterCode.Enemy1:

                if(rb.velocity.x >= 4)
                {
                    rb.velocity = new Vector2(4,0);
                }
                else if (rb.velocity.x <= -4)
                {
                    rb.velocity = new Vector2(-4, 0);
                }
                RaycastHit2D _playerS;

                _playerS = Physics2D.Raycast(rb.position, dir, 1f, LayerMask.GetMask("Player"));
                if (_playerS.collider != null && _Damaged == false)
                {
                    _Damaged = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine(Damaged());
                }

                RaycastHit2D _platform;
                _platform = Physics2D.Raycast(rb.position, dir + Vector3.down, 1f, LayerMask.GetMask("Platform"));
                Debug.DrawRay(rb.position, dir + Vector3.down, new Color(255, 0, 0), 1f);
                if (_platform.collider == null)
                {
                    if (dir == new Vector3(1,0,0))
                    {
                        dir = new Vector3(-1, 0, 0);
                        _spi.flipX = true;
                    }
                    else if (dir == new Vector3(-1, 0, 0))
                    {
                        dir = new Vector3(1, 0, 0);
                        _spi.flipX = false;
                    }
                }
                if (_platform.collider != null)
                {
                    _ani.SetBool("Run", true);
                    if (_Damaged == false)
                        rb.AddForce(dir * 2, ForceMode2D.Impulse);
                }
                break;
        }
    }

    IEnumerator Damaged()
    {
        yield return new WaitForSeconds(0.2f);
        _ani.SetBool("Attack", true);
        _ani.SetBool("Run", false);
        yield return new WaitForSeconds(0.4f);
        _triggerDamaged = true;
        RaycastHit2D _playerS;
        _playerS = Physics2D.Raycast(rb.position, dir, 1f, LayerMask.GetMask("Player"));

        if (_playerS.collider != null)
        {
            if (_playerS.collider.gameObject.GetComponent<PlayerHPMaster>())
            {
                _playerS.collider.gameObject.GetComponent<PlayerHPMaster>().GetDamage(1);
                _playerS.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.x, 0, 0) + new Vector3(0, 1, 0), ForceMode2D.Impulse);

            }

        }
        yield return new WaitForSeconds(0.4f);
        if (dir == new Vector3(1, 0, 0))
        {
            dir = new Vector3(-1, 0, 0);
            _spi.flipX = true;
        }
        else
        {
            dir = new Vector3(1, 0, 0);
            _spi.flipX = false;
        }


        _ani.SetBool("Attack", false);
        yield return new WaitForSeconds(0.2f);

        _Damaged = false;


    }

    

    public void GetDamage(float damage)
    {
        if (_die == false)
        {
            _damageUI = PoolManager.Instance.Pop("DamageText") as Word;
            _damageUI.transform.position = transform.position;
            _damageUI.ShowText(damage);
            StopAllCoroutines();

            _Damaged = false;
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

}