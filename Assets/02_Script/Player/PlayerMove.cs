using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        _mechu = GameObject.Find("MeChuLeft").GetComponent<MeChu>();
        rigid = GetComponent<Rigidbody2D>();
        _edge = GetComponent<EdgeCollider2D>();
        _hp = GetComponent<PlayerHPMaster>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S))
        {
            try
            {

                GameObject.FindWithTag("DownPlatform").GetComponent<DownPlatform>().ChangeLayer();
            }
            catch
            {

            }
        }

        if (Input.GetButtonDown("Jump") && _jumpCount < _maxJump && _hp.GetDamaged() == false)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            _jumpCount++;

        }
        if (Input.GetButtonUp("Horizontal") && _hp.GetDamaged() == false)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

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


        if (rigid.velocity.y < 0)
        {
            RaycastHit2D _platForm = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            RaycastHit2D _downPlatForm = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("DownPlatform"));
            if (_platForm.collider != null)
            {
                if (_platForm.distance < 0.5f)
                {
                    //anim.SetBool("isJumping", false);
                }
            }
            if (_downPlatForm.collider != null)
            {
                if (_downPlatForm.distance < 0.5f)
                {
                    //anim.SetBool("isJumping", false);
                }
            }
        }
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