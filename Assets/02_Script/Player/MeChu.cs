using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeChu : MonoBehaviour
{
    BoxCollider2D _box;
    Rigidbody2D _rigid;
    PlayerMove _playerdirect;
    const string Right = "MeChuRight";
    const string Left = "MeChuLeft";
    Vector2 MousePos;
    Vector3 OriginPos;
    Vector3 OriginRot;
    float currentTime = 0;

    float speed = 0;
    float Force;

    bool isCanAttack = false;
    bool isAttack = false;
    bool isDownAttack = false;
    bool isDownAttackUpper = false;
    bool isDownAttakb = false;

    enum AttackNumber
    {
        First = 1,
        Second = 2,
        Third = 3
    }
    int _AttackNow = 0;

    void Start()
    {
        OriginPos = transform.localPosition;
        if (gameObject.name == Left)
            OriginRot = transform.localEulerAngles;
        if (gameObject.name == Right)
            OriginRot = transform.localEulerAngles;
        
        _box = GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _box.enabled = false;
        _playerdirect = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
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

    // Update is called once per frame
    void Update()
    {
        NormalAttack();
        NotAttacking();
    }
    void NormalAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isCanAttack == false)
        {
            isAttack = true;
            if(isDownAttack == true)
            {
                isDownAttackUpper = true;
                if (gameObject.name == Left)
                {
                    _box.enabled = true;
                    transform.localPosition = new Vector3(2, 0.5f, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 90);
                }
                if (gameObject.name == Right)
                {
                    _box.enabled = true;
                    transform.localPosition = new Vector3(2.3f, 0.4f, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 90);
                }
                currentTime = 0;
            }

            if (Input.GetKey(KeyCode.S) && isDownAttack == false && isDownAttakb ==false)
            {
                isDownAttack = true;
                isDownAttakb = true;
                if (gameObject.name == Left)
                {
                    _box.enabled = true;
                    transform.localPosition = new Vector3(-1.5f, -0.5f, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 290);
                }
                if (gameObject.name == Right)
                {
                    _box.enabled = true;
                    transform.localPosition = new Vector3(1.5f, -0.5f, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 70);
                }
                _box.size = new Vector2(0.32f* 2, 0.64f*2);
                currentTime = 0;
            }

            _AttackNow++;
            if ((int)AttackNumber.First == _AttackNow && isDownAttack == false)
            {
                if (gameObject.name == Left)
                {
                    transform.localPosition = OriginPos;
                    transform.localEulerAngles = OriginRot;
                }
                if (gameObject.name == Right)
                {
                    _box.enabled = true;
                    Debug.Log("펀취");
                    transform.localPosition = new Vector3(2f, -0.2f, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 95);
                    _box.size = new Vector2(0.32f, 0.64f);
                }
                currentTime = 0;
            }

            if ((int)AttackNumber.Second == _AttackNow && isDownAttack == false)
            {

                if (gameObject.name == Left)
                {
                    _box.enabled = true;
                    transform.localPosition = new Vector3(2.5f, 0f, 0);
                    transform.localEulerAngles = new Vector3(0, 0, 85);
                    _box.size = new Vector2(0.32f, 0.64f);
                }
                if (gameObject.name == Right)
                {
                    transform.localPosition = OriginPos;
                    transform.localEulerAngles = OriginRot;
                }
                currentTime = 0;
            }

        }
    }
    void NotAttacking()
    {
        if(currentTime>=0.1f)
        {
            _box.enabled = false;
        }
        if (isAttack == true)
        {
            currentTime += Time.deltaTime;
        }
        if (currentTime >= 0.5f)
        {
            isCanAttack = true;
            StartCoroutine(CoolDown());
            isAttack = false;
            isDownAttack = false;
            isDownAttackUpper = false;
            isDownAttakb = false;
            _AttackNow = 0;
        }
        if (isAttack == false)
        {
            transform.localPosition = OriginPos;
            transform.localEulerAngles = OriginRot;
            currentTime = 0;
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isCanAttack = false;
    }

    public bool GetBool()
    {
        return isAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("몬가있음");
        if (collision.GetComponent<EnemyHPMaster>() )
        {
            if ((_AttackNow == 1 || _AttackNow == 2))
            {
                if(isDownAttack==false)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0) * 0.3f * _playerdirect.GetDirect(), ForceMode2D.Impulse);
                    collision.GetComponent<EnemyHPMaster>().GetDamage(30);
                    StartCoroutine(EnemyMove(collision.GetComponent<EnemyHPMaster>()));
                }

                
            }
            if(isDownAttack == true)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10) * 0.5f * 1, ForceMode2D.Impulse);
                collision.GetComponent<EnemyHPMaster>().GetDamage(30);
                StartCoroutine(EnemyMove(collision.GetComponent<EnemyHPMaster>()));
            }
            if (isDownAttackUpper == true)
            {
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0) * 3f * _playerdirect.GetDirect(), ForceMode2D.Impulse);
                collision.GetComponent<EnemyHPMaster>().GetDamage(30);
                StartCoroutine(EnemyMove(collision.GetComponent<EnemyHPMaster>()));
            }
        }
    }
    IEnumerator EnemyMove(EnemyHPMaster enemy)
    {
        enemy.SetMove(0);
        yield return new WaitForSeconds(0.6f);
        enemy.SetMove(1);
    }
}
