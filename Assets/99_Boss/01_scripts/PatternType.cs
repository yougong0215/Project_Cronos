using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternType : MonoBehaviour
{
    private Movement movement;
    public float distance;
    private Transform player;
    private Animator animator;

    private bool isOnAttack = false;

    private void Awake()
    {
        //Pattern();
        movement = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        distance = Vector3.Distance(player.position, transform.position);
    }

    private void Start()
    {
        //StartCoroutine(CloseAttack());
    }
    private void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance > 3f)
        {
            Debug.Log("aaaaaaaaaaaaaaaaa");
            movement.ChasePlayer();   
        } else if (distance < 3)
        {
            StartCoroutine(CloseAttack());
        }
    }
    private IEnumerator Pattern()
    {
        yield return null;
    }
    private IEnumerator CloseAttack()
    {
        Debug.Log("a");
        isOnAttack = true;
        animator.SetTrigger("A");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("f");
        isOnAttack = false;
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
