using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternType : MonoBehaviour
{
    private Movement movement;
    private float distance;
    private Transform player;
    private Animator animator;

    private bool isOnAttack = false;

    private void Start()
    {
        movement = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        //Pattern();
        CloseAttack();
    }
    private void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
    }
    private IEnumerator Pattern()
    {
        yield return null;
    }
    private IEnumerator CloseAttack()
    {
        isOnAttack = true;
        while (distance > 3)
        {
            movement.ChasePlayer();
        }
        animator.SetTrigger("A");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("f");
        yield return null;
    }
    private IEnumerator Cast()
    {
        yield return null;
    }
    private IEnumerator Telleport()
    {
        yield return null;
    }
}
