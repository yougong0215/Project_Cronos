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
    private float middleOfMapY; //맵의 위쪽

    private void Awake()
    {
        movement = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(3.23f, 1.28f, player.position.z);
        animator = GetComponent<Animator>();
        distance = Vector3.Distance(player.position, transform.position);
    }

    private void Start()
    {
        StartCoroutine(OpeningPattern());
    }
    private IEnumerator OpeningPattern()
    {
        int num = 3;
        for(int i = 0; i < 3; i++)
        {
            animator.SetTrigger("T");//Attack
            StartCoroutine(castBulletAtSide());
            yield return new WaitForSeconds(0.5f);
            animator.SetTrigger("A");
            transform.position = new Vector3(player.position.x + num, groundPositionY, player.position.z);
            yield return new WaitForSeconds(0.5f);
            num *= -1;
        }
        animator.SetTrigger("T");//Attack
        StartCoroutine(castBulletAtSide());
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(middleOfMapX, middleOfMapY, player.position.z);
        animator.SetTrigger("C");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
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
}
