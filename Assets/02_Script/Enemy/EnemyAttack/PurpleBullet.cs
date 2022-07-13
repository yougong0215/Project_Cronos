using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBullet : PoolAble
{

    [SerializeField] protected List<Vector4> _timeLeaf1;
    protected List<Sprite> _timeLeaf2;

    float currentTime = 0;
    Rigidbody2D _masterEnemy;
    Vector3 dir = Vector2.zero;
    int _timecode = 0;
    int _timeRefer = 1;

    private void Awake()
    {
        for (int i = 0; i < 30; i++)
        {
            _timeLeaf1.Add(new Vector4(transform.position.x, transform.position.y, currentTime, transform.localEulerAngles.z));
        }
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
            _timeLeaf1[0] = new Vector4(transform.position.x, transform.position.y, currentTime, transform.rotation.z);
        }
    }

    private void TimeLeaf()
    {
        if (GameManager.Instance.Timer() == false)
        {
            TimeSave();
            _timeRefer = 1;
            _timecode = 0;
        }
        if (GameManager.Instance.Timer() == true)
        {
            _timeRefer = 0;
            if (currentTime > 0.1f * GameManager.Instance.TimeArrange())
            {
                currentTime = 0;
                if (_timecode > 29)
                {
                    
                    return;
                }
                _timecode++;
                currentTime = (int)_timeLeaf1[_timecode - 1].z;
                transform.position = _timeLeaf1[_timecode - 1];
                transform.localEulerAngles = new Vector3(0, 0, _timeLeaf1[_timecode - 1].w);
            }
        }
    }


    public void Sex(Rigidbody2D _ms)
    {
        for (int i = 29; i >= 0; i--)
        {
            if (i < 0)
                break;
            _timeLeaf1[i] = new Vector4(transform.position.x, transform.position.y, currentTime, transform.rotation.z);
        }
        _timeLeaf1[0] = new Vector4(transform.position.x, transform.position.y, currentTime, transform.rotation.z);
        _masterEnemy = _ms;
        if(_ms.velocity.x >= 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            dir = new Vector3(1, 0);
        }
        if (_ms.velocity.x < -0f)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
            dir = new Vector3(-1, 0);
        }
    }


    void Update()
    {
        TimeLeaf();
        currentTime += Time.deltaTime;
        transform.position += dir * 3 * GameManager.Instance.CanMove() * Time.deltaTime;
        if(currentTime>=3f)
        {
            transform.position = new Vector3(-1000, 1000);
            if (_timeLeaf1[0] == _timeLeaf1[30])
            {
                PoolManager.Instance.Push(this);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHPMaster>())
        {
            collision.GetComponent<PlayerHPMaster>().GetDamage(1);
        }
    }
}
