using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float maxSpeed;// 최대속도 설정
    [SerializeField] float jumpPower; // 점프파워 
    Rigidbody2D rigid;

    int _jumpCount = 0;
    int _maxJump =1;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && _jumpCount < _maxJump)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            _jumpCount++;
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }

    }


    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");  
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); 


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
        Debug.DrawRay(transform.position, new Vector3(transform.position.x, transform.position.y - 1,transform.position.z));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 20 )
        {
            _jumpCount = 0;
        }
    }
}