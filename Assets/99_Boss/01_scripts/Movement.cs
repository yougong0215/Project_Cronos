using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer _renderer;

    private bool isMoving = true;

    [SerializeField]
    private float moveSpeed;

    private float movedirection;

    private void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        Flip();
    }
    private void Flip()
    {
        if (isMoving)
        {
            if (player.position.x > transform.position.x)
            {
                _renderer.flipX = false;
                movedirection = 1;
            } 
            else if (player.position.x < transform.position.x)
            {
                _renderer.flipX = true;
                movedirection = -1;
            }
        }
    }

    public void ChasePlayer()
    {
        if(isMoving && GameManager.Instance.Timer() == false)
        {
            transform.position += new Vector3(moveSpeed * movedirection * Time.deltaTime, 0, 0);
        }
    }
    
}
