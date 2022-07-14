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
    float currentTime = 0;
    //private bool isOnAttack = false;

    [SerializeField] protected List<Vector4> _timeLeaf1;
    protected Sprite[] _timeLeaf2 = new Sprite[30];
    int _timecode = 0;

    [SerializeField]
    private float groundPositionY; //지면과 Boss의 Collider가 맞닿는 지점

    Word _damageUI;
    //보스 궁극기 사용시 이동할 지점
    [SerializeField]
    private float middleOfMapX; //보스맵의 중앙 X
    [SerializeField]
    private float middleOfMapY; //맵의 위쪽
    SpriteRenderer _spi;
    Bullet bi;

    private void Awake()
    {
        
        _spi = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 30; i++)
        {
            _timeLeaf1.Add(new Vector4(transform.position.x, transform.position.y, 0, transform.localEulerAngles.z));
            _timeLeaf2[i] = _spi.sprite;
        }
        movement = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.localPosition = new Vector3(3.23f, 1.28f, player.position.z);
        animator = GetComponent<Animator>();
        distance = Vector3.Distance(player.position, transform.position);
        
    }

    private void TimeSave()
    {
        if (currentTime >= 0.1f * GameManager.Instance.TimeArrange())
        {
            currentTime = 0;
            for (int i = 29; i > 0; i--)
            {
                if (i <= 0)
                    break;


                _timeLeaf1[i] = _timeLeaf1[i - 1];
                _timeLeaf2[i] = _timeLeaf2[i - 1];
            }
            _timeLeaf1[0] = new Vector4(transform.position.x, transform.position.y, 0, transform.rotation.z);
            _timeLeaf2[0] = _spi.sprite;
        }
    }
    private void TimeLeaf()
    {

        animator.speed = GameManager.Instance.CanMove();
        if (GameManager.Instance.Timer() == false && _timecode >= 29)
        {
            animator.enabled = true;
            animator.Rebind();
            _timecode = 0;
            StartCoroutine(Pattern());
        }
        if (GameManager.Instance.Timer() == false && GameManager.Instance.TimeArrange() != 10)
        {
            TimeSave();
            _timecode = 0;
            
        }

        if (GameManager.Instance.Timer() == true && _timecode <= 29)
        {
            StopAllCoroutines();
            animator.StopPlayback();
            animator.enabled = false;
            if (currentTime > 0.1f * GameManager.Instance.TimeArrange())
            {
                currentTime = 0;
                if (_timecode >= 29)
                {

                    animator.enabled = true;
                    animator.Rebind();
                    animator.SetBool("Died", false);
                    return;
                }

                _timecode++;
                transform.position = _timeLeaf1[_timecode - 1];
                transform.localEulerAngles = new Vector3(0, 0, _timeLeaf1[_timecode - 1].w);
                _spi.sprite = _timeLeaf2[_timecode - 1];
            }

        }
    }






    private void Update()
    {
        currentTime += Time.deltaTime;
        TimeLeaf();
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
            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(-pos.x, pos.y);
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
            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(pos.x, pos.y);
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

            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(-pos.x, pos.y);
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
            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(i, 0.8f);
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
            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(i, 0.8f);
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
            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(i, 0.8f);
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
            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(-7, i);
            yield return new WaitForSeconds(0.25f);
            PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(7, i);
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
        PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(transform.localPosition.x - 2, transform.localPosition.y);
        PoolManager.Instance.Pop("BossBullet1").transform.position = new Vector2(transform.localPosition.x - 6, transform.localPosition.y);
        yield return null;
    }
}
