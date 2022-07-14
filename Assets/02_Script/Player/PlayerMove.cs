using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpPower;
    Rigidbody2D rigid;
    MeChu _mechu;
    EdgeCollider2D _edge;
    PlayerHPMaster _hp;
    int _jumpCount = 0;
    int _maxJump = 1;
    int _DirectValue = 1;
    bool Move = false;
    float _coljumpCount = 0;
    [SerializeField] Image _iamge;

    void Awake()
    {
        _mechu = GameObject.Find("MeChuLeft").GetComponent<MeChu>();
        rigid = GetComponent<Rigidbody2D>();
        _edge = GetComponent<EdgeCollider2D>();
        _hp = GetComponent<PlayerHPMaster>();
    }
    public void DownPlatformT()
    {
       GameObject.FindWithTag("DownPlatform").GetComponent<DownPlatform>().ChangeLayer();
    }

    void Update()
    {
        _iamge.fillAmount = _coljumpCount;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S))
        {
            DownPlatformT();

        }

        if (Input.GetButtonDown("Jump") && _jumpCount < _maxJump && _hp.GetDamaged() == false)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            _jumpCount++;
            _coljumpCount+= 0.01f;
            _iamge.rectTransform.anchoredPosition = new Vector3(_iamge.rectTransform.position.x, 480, 0);
        }
        if (Input.GetButtonUp("Horizontal") && _hp.GetDamaged() == false)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            rigid.gravityScale = 1;
        }
        if(_coljumpCount >= 100 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && rigid.velocity.y <-0.1f)
        {
            
            rigid.gravityScale = 0.05f;
        }
        if((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space)))
        {
            rigid.gravityScale = 1;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            rigid.gravityScale = 2;
        }

        if (_mechu.GetBool() == false)
        {
            if (rigid.velocity.x >= 0.1f && _hp.GetDamaged() == false)
            {
                _DirectValue = 1;
                transform.localScale = new Vector3(_DirectValue * 0.4f, 1 * 0.4f, 1);
            }
            else if (rigid.velocity.x < -0.1f && _hp.GetDamaged() == false)
            {
                _DirectValue = -1;
                transform.localScale = new Vector3(_DirectValue * 0.4f, 1 * 0.4f, 1);
            }
        }
    }


    public int GetDirect()
    {
        return _DirectValue;
    }
    public bool GetMove()
    {
        return Move;
    }

    void FixedUpdate()
    {
        float h = 0;
        if (_hp.GetDamaged() == false)
        {
            h = Input.GetAxisRaw("Horizontal");

            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }

        if (h != 0 )
        {
            Move = true;
        }
        else if (h == 0)
            Move = false;

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);



        Debug.DrawRay(transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 20 && (Mathf.Abs(rigid.velocity.y) < 0.1f || Mathf.Abs(rigid.velocity.y) == 0))
        {
            _jumpCount = 0;


        }
        if (collision.gameObject.layer == 21 && (Mathf.Abs(rigid.velocity.y) < 0.1f || Mathf.Abs(rigid.velocity.y) == 0))
        {
            _jumpCount = 0;
        }

    }
}