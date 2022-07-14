using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer _renderer;

    private Animator _animator;

    private bool _isOnAttack = false;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(ComboAttack());
    }
    private void Update()
    {
        _FlipX();
    }

    //연속공격
    private IEnumerator ComboAttack()
    {
        _isOnAttack = true;
        //destory every attack collider at 0.05s after
        Debug.Log("as");
        _animator.SetTrigger("ComboAttack");
        yield return new WaitForSeconds(0.65f);
        //First attack point
        yield return new WaitForSeconds(0.3f);
        //Second attack point
        yield return new WaitForSeconds(0.8f);
        //last attack point
        //camera shake
        _isOnAttack = false;
    }

    //원거리 공격
    private IEnumerator RangeAttack()
    {
        yield return null;
    }
    private void _FlipX()
    {
        if (!_isOnAttack)
        {
            if (player.position.x > transform.position.x)
            {
                _renderer.flipX = false;
            }
            else if (player.position.x < transform.position.x)
            {
                _renderer.flipX = true;
            }
        }
    }
}
