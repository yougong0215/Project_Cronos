using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternType : MonoBehaviour
{
    private Movement movement;
    public float distance;
    private Transform player;
    private Animator animator;

    [SerializeField]
    private GameObject bulletPrefabs;

    //private bool isOnAttack = false;

    [SerializeField]
    private float groundPositionY; //지면과 Boss의 Collider가 맞닿는 지점


    //보스 궁극기 사용시 이동할 지점
    [SerializeField]
    private float middleOfMapX; //보스맵의 중앙 X
    [SerializeField]
    private float highPointY; //맵의 위쪽
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
        StartCoroutine(OpeningPattern());
    }
    private void Update()
    {
        //distance = Vector3.Distance(player.position, transform.position);
        //if (distance > 3f)
        //{
        //    Debug.Log("aaaaaaaaaaaaaaaaa");
        //    movement.ChasePlayer();   
        //} else if (distance < 3)
        //{
        //    StartCoroutine(CloseAttack());
        //}
    }
    private IEnumerator Pattern()
    {
        yield return null;
    }
    private IEnumerator OpeningPattern()
    {
        animator.SetTrigger("T");//Attack
        StartCoroutine(castBulletAtSide());
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("A");
        transform.position = new Vector3(player.position.x + 3, groundPositionY, 0);
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("I");
    }
    private IEnumerator CloseAttack()
    {
        Debug.Log("a");
        //isOnAttack = true;
        animator.SetTrigger("A");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("f");
        //isOnAttack = false;
        yield return null;
    }

    private IEnumerator castBulletThreeTime()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(bulletPrefabs, new Vector3(transform.position.x, transform.position.y + 2, 0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(bulletPrefabs, new Vector3(transform.position.x, transform.position.y + 2, 0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(bulletPrefabs, new Vector3(transform.position.x, transform.position.y + 2, 0), Quaternion.identity);
    }
    private IEnumerator castBulletAtSide()
    {
        Instantiate(bulletPrefabs, new Vector3(transform.position.x - 3, transform.position.y + 2, 0), Quaternion.identity);
        Instantiate(bulletPrefabs, new Vector3(transform.position.x + 3, transform.position.y + 2, 0), Quaternion.identity);
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
