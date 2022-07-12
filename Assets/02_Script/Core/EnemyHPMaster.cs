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



    [SerializeField] protected List<Vector4> _timeLeaf1;
    protected List<Sprite> _timeLeaf2;
    // x = 위치
    // y = 위치
    // z = 체력
    [SerializeField] int _hp;
    [SerializeField] Vector2 vector2;
    Rigidbody2D rb;
    float currentTime = 0;

    [SerializeField ]int _monsterType = 1;
    bool _bTimereaf = false;
    int _timecode = 0;
    MonsterCode _monsterCode;

    private Transform _player = null;
    public Transform Player
    {
        get
        {
            if(_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }

            return _player;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        for(int i = 0; i< 30; i++)
        {
            _timeLeaf1.Add(new Vector4(transform.position.x, transform.position.y, _hp, transform.rotation.z));
        }
    }
    private void TimeSave()
    {
        if(currentTime >= 0.1f)
        {
            currentTime = 0;
            for (int i =29; i> 0; i--)
            {
                if (i == 0)
                    break;
                _timeLeaf1[i] = _timeLeaf1[i-1];
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
                _bTimereaf = true;
            }
            else if (_bTimereaf == true)
            {
                _bTimereaf = false;
            }
        }
    }
    private void TimeLeaf()
    {
        if (_bTimereaf == false)
        {
            MonsterAIMove();
            if (currentTime >= 0.1f)
            {
                TimeSave();
            }
            _timecode = 0;
        }
        if (_bTimereaf == true)
        {
            if (currentTime >= 0.1f)
            {
                currentTime = 0;
                if (_timecode > 29)
                    return;

                Debug.Log(_timecode);
                _timecode++;
                _hp = (int)_timeLeaf1[_timecode - 1].z;
                transform.position = _timeLeaf1[_timecode - 1];
                transform.localEulerAngles = new Vector3(0, 0, _timeLeaf1[_timecode - 1].w);

            }
        }
    }
    void Update()
    {
        if(_hp < 0)
        {
            Debug.Log("뒤짐");
        }
    }
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        TimeControl();
        TimeLeaf();
    }
    float _currentTime2 = 0;
    private void MonsterAIMove()
    {
        switch (_monsterType)
        {
            case (int)MonsterCode.Test:
                _currentTime2 += Time.deltaTime;
                transform.position -= new Vector3(Mathf.Sin(_currentTime2), 0, 0) * 4 * Time.deltaTime;
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
    

    public void GetDamage(int damage)
    {
        _hp -= damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    
    }
}