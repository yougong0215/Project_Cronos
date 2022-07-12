using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeChu : MonoBehaviour
{
    BoxCollider2D _box;
    Rigidbody2D _rigid;
    const string Right = "MeChuRight";
    const string Left = "MeChuLeft";
    Vector2 MousePos;
    Vector3 OriginPos;
    Vector3 OriginRot;
    float currentTime = 0;

    float speed = 0;
    float Force;


    bool isAttack = false;

    enum AttackNumber
    {
        First,
        Second,
        Third
    }
    int _AttackNow = 1;

    void Start()
    {
        OriginPos = transform.localPosition;
        if (gameObject.name == Left)
            OriginRot = 20;
        if (gameObject.name == Right)
            OriginRot = 340;

        _box = GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _box.enabled = false;
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            isAttack = true;
            if ((int)AttackNumber.First == _AttackNow)
            {
                if (gameObject.name == Left)
                {

                }
                if (gameObject.name == Right)
                {
                    transform.localPosition = new Vector3(2, 1, 0);
                    transform.localEulerAngles = new Vector3(0, 95, 0);
                }
            }
            if ((int)AttackNumber.Second == _AttackNow)
            {
                if (gameObject.name == Left)
                {

                }
                if (gameObject.name == Right)
                {

                }
            }
            if ((int)AttackNumber.Third == _AttackNow)
            {

            }
            _AttackNow++;
        }
        _AttackNow = 1;
        NotAttacking();
     }

    void NotAttacking()
    {
        if(isAttack == true)
        {
            currentTime += Time.deltaTime;
        }
        if(currentTime>= 0.3f)
        {
            isAttack = false;
        }
        if(isAttack == false)
        {
            transform.localPosition = OriginPos ;
            transform.localEulerAngles = OriginRot;
            currentTime = 0;
        }
    }
}
