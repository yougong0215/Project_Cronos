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
    
    private int num = 0;

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
        transform.localPosition = new Vector3(3.23f, 1.28f, player.position.z);
        animator = GetComponent<Animator>();
        distance = Vector3.Distance(player.position, transform.position);

    }

    private void Start()
    {
        StartCoroutine(Pattern());
    }
    private IEnumerator Pattern()
    {
        while (true) // 보스HP 0일때 까지
        {
            StartCoroutine(RestTime());
            yield return new WaitForSeconds(2.5f);

            for (int i = 0; i < 2; i++)
            {
                num = Random.Range(1, 8);
                switch (num)
                {
                    case 1:
                        StartCoroutine(CastBulletSemiCircleLeft());
                        break;
                    case 2:
                        StartCoroutine(CastBulletSemiCircleRight());
                        break;
                    case 3:
                        StartCoroutine(CastBulletSemiCircleSameTime());
                        break;
                    case 4:
                        StartCoroutine(CastBulletStraightLeft());
                        break;
                    case 5:
                        StartCoroutine(CastBulletStraightRight());
                        break;
                    case 6:
                        StartCoroutine(CastBulletStraightSameTime());
                        break;
                    case 7:
                        StartCoroutine(CastBulletEdge());
                        break;
                    case 8:
                        StartCoroutine(CloseAttack());
                        num = 0;
                        break;
                }
                yield return new WaitForSeconds(6.25f);
            }
        }
    }

    //반원(좌 > 우)
    private IEnumerator CastBulletSemiCircleLeft()
    {
        int count = 20;

        transform.localPosition = new Vector3(Random.Range(-3, 3), middleOfMapY, player.position.z);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < count + 1; i++)
        {
            float value = (i / (float)count) * Mathf.PI;

            Vector2 pos = new Vector2(Mathf.Cos(value), Mathf.Sin(value)) * 8;
            pos += Vector2.down * 3;

            Instantiate(bulletPrefabs, new Vector2(-pos.x, pos.y), Quaternion.identity);
            yield return new WaitForSeconds(0.0625f);
        }
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("N");
        yield return new WaitForSeconds(3f);
    }
    
    //반원(우 > 좌)
    private IEnumerator CastBulletSemiCircleRight()
    {
        int count = 20;

        transform.localPosition = new Vector3(Random.Range(-3, 3), middleOfMapY, player.position.z);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < count + 1; i++)
        {
            float value = (i / (float)count) * Mathf.PI;

            Vector2 pos = new Vector2(Mathf.Cos(value), Mathf.Sin(value)) * 8;
            pos += Vector2.down * 3;

            Instantiate(bulletPrefabs, new Vector2(pos.x, pos.y), Quaternion.identity);
            yield return new WaitForSeconds(0.0625f);
        }
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("N");
        yield return new WaitForSeconds(3f);
    }

    //반원(동시)
    private IEnumerator CastBulletSemiCircleSameTime()
    {
        transform.localPosition = new Vector3(Random.Range(-3, 3), middleOfMapY, player.position.z);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
        yield return new WaitForSeconds(1f);
        int count = 20;
        for (int i = 0; i < count + 1; i++)
        {
            float value = (i / (float)count) * Mathf.PI;

            Vector2 pos = new Vector2(Mathf.Cos(value), Mathf.Sin(value)) * 8;
            pos += Vector2.down * 3;

            Instantiate(bulletPrefabs, new Vector2(-pos.x, pos.y), Quaternion.identity);
        }
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("N");
        yield return new WaitForSeconds(4.25f);
    }

    //직선(좌 > 우)
    private IEnumerator CastBulletStraightLeft()
    {
        transform.localPosition = new Vector3(Random.Range(-3, 3), middleOfMapY, player.position.z);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
        yield return new WaitForSeconds(1f);

        for (int i = -8; i < 9; i++)
        {
            Instantiate(bulletPrefabs, new Vector3(i, 0.8f, player.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0.0625f);
        }
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("N");
        yield return new WaitForSeconds(3.1875f);

    }

    //직선(우 > 좌)
    private IEnumerator CastBulletStraightRight()
    {
        transform.localPosition = new Vector3(Random.Range(-3, 3), middleOfMapY, player.position.z);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
        yield return new WaitForSeconds(1f);
        for (int i = 9; i > -8; i--)
        {
            Instantiate(bulletPrefabs, new Vector3(i, 0.8f, player.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0.0625f);
        }
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("N");
        yield return new WaitForSeconds(3.1875f);
    }

    //직선(동시)
    private IEnumerator CastBulletStraightSameTime()
    {
        transform.localPosition = new Vector3(Random.Range(-3, 3), middleOfMapY, player.position.z);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
        yield return new WaitForSeconds(1f);
        for (int i = -9; i < 8; i++)
        {
            Instantiate(bulletPrefabs, new Vector3(i, 0.8f, player.position.z), Quaternion.identity);
        }
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("N");
        yield return new WaitForSeconds(4.25f);
    }

    //양끝
    private IEnumerator CastBulletEdge()
    {
        transform.localPosition = new Vector3(Random.Range(-3, 3), middleOfMapY, player.position.z);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("L");
        yield return new WaitForSeconds(1f);
        for(int i = -3; i < 4; i++)
        {
            Instantiate(bulletPrefabs, new Vector3(-7, i, player.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
            Instantiate(bulletPrefabs, new Vector3(7, i, player.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("V");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("N");
    }

    //근접 패턴
    private IEnumerator CloseAttack()
    {
        int num = 3;
        for (int i = 0; i < 3; i++)
        {
            animator.SetTrigger("T");
            StartCoroutine(CastBulletAtSide());
            yield return new WaitForSeconds(0.5f);
            animator.SetTrigger("A");
            transform.localPosition = new Vector3(player.position.x + num, groundPositionY, player.position.z);
            if (transform.localPosition.x >= 3.39 || transform.localPosition.x <= -3.39)
            {
                transform.localPosition = new Vector3(Random.Range(-3, 3), groundPositionY, player.position.z);
            }
            yield return new WaitForSeconds(0.5f);
            num *= -1;
        }
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("N");
        yield return new WaitForSeconds(2.75f);
    }




    //쉬는타임
    private IEnumerator RestTime()
    {
        transform.localPosition = new Vector3(0, groundPositionY, 0);
        animator.SetTrigger("P");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("I");
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("V");
        yield return new WaitForSeconds(0.5f);
    }




    private IEnumerator CastBulletAtSide()
    {
        Instantiate(bulletPrefabs, new Vector3(transform.localPosition.x - 2, transform.localPosition.y, player.position.z), Quaternion.identity);
        Instantiate(bulletPrefabs, new Vector3(transform.localPosition.x - 6, transform.localPosition.y, player.position.z), Quaternion.identity);
        yield return null;
    }
}
