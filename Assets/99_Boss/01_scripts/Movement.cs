using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private SpriteRenderer _renderer;

    private bool isMoving = true;

    [SerializeField]
    private float moveSpeed;

    private float movedirection;

    [SerializeField]
    private int findPlayer;
    private void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        FindPlayer();
        Flip();
    }
    private void Flip()
    {
        if (isMoving)
        {
            if (player.transform.position.x > transform.position.x)
        {
            _renderer.flipX = false;
            movedirection = 1;
        } else if (player.transform.position.x < transform.position.x)
        {
            _renderer.flipX = true;
            movedirection = -1;
        }
        }
    }
    private void FindPlayer()
    {
        if (player.transform.position.x < transform.position.x + findPlayer && player.transform.position.x > transform.position.x - findPlayer)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    private void ChasePlayer()
    {
        if(isMoving == true)
        {
            transform.position += new Vector3(moveSpeed * movedirection * Time.deltaTime, 0, 0);
        }
    }
    
}
