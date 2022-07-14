using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class MeChu : MonoBehaviour
{
    BoxCollider2D _box;
    Rigidbody2D _rigid;
    PlayerMove _playerdirect;
    const string Right = "MeChuRight";
    const string Left = "MeChuLeft";

    float TimeRush = 0;
    Vector2 MousePos;
    Vector3 OriginPos;
    Vector3 OriginRot;
    float currentTime = 0;
    //[SerializeField] CinemachineVirtualCamera _cam;
    float speed = 0;
    float Force;
    bool isDownArraAttack = false;

    bool isCanAttack = false;
    bool isAttack = false;
    bool isDownAttack = false;
    bool isDownAttackUpper = false;
    bool isDownAttakb = false;

    enum AttackNumber
    {
        First = 1,
        Second = 2,
        Third = 3
    }
    int AttackNum = 0;
    int AttackNumItem = 5;
    [SerializeField]Image ItemFill;
    int _AttackNow = 0;
    bool _attackNummm = false;
    bool _upAttack = false;
    bool _isGuard =false;
    void Start()
    {
        AttackNum = 0;
        OriginPos = transform.localPosition;
        if (gameObject.name == Left)
            OriginRot = transform.localEulerAngles;
        if (gameObject.name == Right)
            OriginRot = transform.localEulerAngles;
        
        _box = GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _box.enabled = false;
        _playerdirect = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    private Transform _player = null;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }

            return _player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        NormalAttack();
        NotAttacking();
    }
    void NormalAttack()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isAttack = true;
            _isGuard = true;
            if (gameObject.name == Left)
            {
                transform.localEulerAngles = new Vector3(0, 0, 170);
                transform.localPosition = new Vector3(0.5f, 0.5f, 0);
            }
            if (gameObject.name == Right)
            {
                transform.localEulerAngles = new Vector3(0, 0, 190);
                transform.localPosition = new Vector3(0.5f, 0.5f, 0);
            }
            return;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isGuard = false;
        }

        if(_upAttack == true)
        {
            if (gameObject.name == Left)
                transform.localPosition = new Vector3(1, 1.2f, 0);
            else if (gameObject.name == Right)
                transform.localPosition = new Vector3(-0.4f, -0.4f);
            return;
        }
        TimeRush += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && isCanAttack == false)
        {
            isAttack = true;

            if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && _upAttack == false)
            {
                
                _upAttack = true;
                _box.enabled =true;
                if (gameObject.name == Left)
                {
                    
                    if (Player.gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.1f)
                    {
                        Player.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
                    }
                    else
                    {
                        Player.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
                    }
                    Player.gameObject.GetComponent<PlayerMove>().DownPlatformT();
                    transform.DOLocalMove(new Vector3(1, 1.2f, 0), 0.1f);
                    transform.localEulerAngles = new Vector3(0, 0, 165);
                }
                if (gameObject.name == Right)
                {
                    transform.DOLocalMove(new Vector3(-0.4f, -0.4f), 0.1f);
                    transform.localEulerAngles = new Vector3(0, 0, 340);
                }
                currentTime = 0;
                AttackNum = 35;
                _attackNummm = false;
            }

            if (isDownAttack == true && AttackNum < AttackNumItem)
            {
                isDownAttackUpper = true;
                if (AttackNum < AttackNumItem)
                {
                    if (_attackNummm == true)
                    {
                        if (gameObject.name == Left)
                            transform.DOLocalMove(new Vector3(Random.Range(-0.4f, 0f), Random.Range(0.2f, 0.7f), 0), 0.1f);
                        if (gameObject.name == Right)
                        {
                            _box.enabled = true;
                            transform.DOLocalMove(new Vector3(Random.Range(1.6f, 2f), Random.Range(0.2f, 0.7f), 0), 0.1f);
                            transform.localEulerAngles = new Vector3(0, 0, 90);
                        }

                        _attackNummm = false;
                    }
                    else if (_attackNummm == false)
                    {
                        if (gameObject.name == Right)
                            transform.DOLocalMove(new Vector3(Random.Range(-0.4f, 0f), Random.Range(0.2f, 0.7f), 0), 0.1f);
                        if (gameObject.name == Left)
                        {
                            _box.enabled = true;
                            transform.DOLocalMove(new Vector3(Random.Range(1.8f, 2.2f), Random.Range(0.2f, 0.7f), 0), 0.1f);
                            transform.localEulerAngles = new Vector3(0, 0, 90);
                        }
                        _attackNummm = true;
                    }
                }
                AttackNum++;
                if (AttackNum >= AttackNumItem)
                {
                    if (gameObject.name == Right)
                        transform.DOLocalMove(new Vector3(Random.Range(-0.4f, 0f), Random.Range(0.2f, 0.7f), 0), 0.1f).OnComplete(() =>
                        {
                            transform.DOLocalMove(new Vector3(5, Random.Range(-0.4f, 0f)), 0.1f);
                        });
                    if (gameObject.name == Left)
                        transform.DOLocalMove(new Vector3(Random.Range(-0.4f, 0f), Random.Range(0.2f, 0.7f), 0), 0.1f).OnComplete(() =>
                        {
                            transform.DOLocalMove(new Vector3(5, Random.Range(-0.4f, 0f)), 0.1f);
                        });
                }
                currentTime = 0;
                return;
            }

            if (Input.GetKey(KeyCode.S) && isDownAttack == false && isDownAttakb ==false)
            {
                isDownAttack = true;
                isDownAttakb = true;
                if (gameObject.name == Left)
                {
                    _box.enabled = true;
                    transform.localPosition = new Vector3(0.5f, 2.2f, 0);
                    transform.DOLocalMove(new Vector3(0, -0.6f, 0), 0.1f).SetEase(Ease.InElastic);
                    
                    
                    transform.localEulerAngles = new Vector3(0, 0, 290);
                }
                if (gameObject.name == Right)
                {
                    _box.enabled = true;
                    transform.localPosition = new Vector3(-0.5f, 2.2f, 0);
                    transform.DOLocalMove(new Vector3(0, -0.6f, 0), 0.1f).SetEase(Ease.InElastic);
                    transform.localEulerAngles = new Vector3(0, 0, 70);
                }
                _box.size = new Vector2(0.32f* 2, 0.64f*2);
                currentTime = 0;
                TimeRush = 0;
            }

            _AttackNow++;
            if ((int)AttackNumber.First == _AttackNow && isDownAttack == false && isDownAttackUpper == false)
            {
                if (gameObject.name == Left)
                {
                    transform.localPosition = OriginPos;
                    transform.localEulerAngles = OriginRot;
                }
                if (gameObject.name == Right)
                {
                    _box.enabled = true;
                    Debug.Log("펀취");
                    //new Vector3(2f, -0.2f, 0)
                    transform.DOLocalMove(new Vector3(2f, -0.2f, 0), 0.1f);
                    transform.localEulerAngles = new Vector3(0, 0, 95);
                    _box.size = new Vector2(0.32f * 2, 0.64f * 2);
                }
                currentTime = 0;
            }

            if ((int)AttackNumber.Second == _AttackNow && isDownAttack == false && isDownAttackUpper == false)
            {

                if (gameObject.name == Left)
                {
                    _box.enabled = true;
                    transform.DOLocalMove(new Vector3(2.5f, 0f, 0), 0.1f);
                    transform.localEulerAngles = new Vector3(0, 0, 85);
                    _box.size = new Vector2(0.32f * 2, 0.64f * 2);
                }
                if (gameObject.name == Right)
                {
                    transform.localPosition = OriginPos;
                    transform.localEulerAngles = OriginRot;
                }
                currentTime = 0;
            }

        }
    }
    IEnumerator TurnCam()
    {
        CameraManager.Instance.TurnCamRig();
        yield return new WaitForSeconds(0.3f);
        CameraManager.Instance.TurnCamNormal();
    }

    void NotAttacking()
    {
        if(currentTime>=0.1f)
        {
            _box.enabled = false;
        }
        if (isAttack == true)
        {
            currentTime += Time.deltaTime;
        }
        if ((currentTime >= 0.8f || TimeRush >= 5f) && _isGuard== false)
        {
            _attackNummm = false;
            TimeRush = 0;
            AttackNum = 0;
            isCanAttack = true;
            StartCoroutine(CoolDown());
            isAttack = false;
            isDownAttack = false;
            isDownAttackUpper = false;
            isDownAttakb = false;
            _AttackNow = 0;
            _upAttack = false;
            _box.size = new Vector2(0.32f * 2, 0.64f * 2);
        }
        if (isAttack == false)
        {
            transform.DOLocalMove(OriginPos, 0.1f);
            transform.localEulerAngles = OriginRot;
            currentTime = 0;
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isCanAttack = false;
    }

    public bool GetBool()
    {
        return isAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("몬가있음");
        if (collision.GetComponent<EnemyHPMaster>() )
        {
            if ((_AttackNow == 1 || _AttackNow == 2))
            {
                if(isDownAttack==false && isDownAttackUpper == false)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 2f * _playerdirect.GetDirect(), ForceMode2D.Impulse);
                    collision.GetComponent<EnemyHPMaster>().GetDamage(10);
                    StartCoroutine(TurnCam());
                }
                

                
            }

            if(_upAttack == true && isDownAttackUpper == false)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10) * 2f * GameManager.Instance.CanMove(), ForceMode2D.Impulse);
                collision.GetComponent<EnemyHPMaster>().GetDamage(10);
                StartCoroutine(EnemyMove(collision.GetComponent<EnemyHPMaster>()));
                StartCoroutine(TurnCam());
            }
            if(isDownAttack == true && isDownAttackUpper == false)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10) * 0.2f * GameManager.Instance.CanMove(), ForceMode2D.Impulse);
                collision.GetComponent<EnemyHPMaster>().GetDamage(10);
                StartCoroutine(EnemyMove(collision.GetComponent<EnemyHPMaster>()));
                StartCoroutine(TurnCam());
            }

            if (isDownAttackUpper == true)
            {
                if (GameManager.Instance.TimeArrange() != 10 && AttackNum == AttackNumItem)
                {
                   collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 6f * _playerdirect.GetDirect(), ForceMode2D.Impulse);
                    StartCoroutine(TurnCam());
                }
                if(AttackNum == AttackNumItem)
                {
                    if (AttackNumItem == 40 && AttackNum == 40)
                    {
                        collision.GetComponent<EnemyHPMaster>().GetDamage(50000);
                    }
                    else
                    {
                        collision.GetComponent<EnemyHPMaster>().GetDamage(10 * AttackNum);
                    }
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 6f * _playerdirect.GetDirect(), ForceMode2D.Impulse);
                    AttackNumItem++;
                    ItemFill.fillAmount +=  0.025f;
                    ItemFill.rectTransform.anchoredPosition = new Vector3(ItemFill.rectTransform.position.x, 480, 0);
                    StartCoroutine(TurnCam());
                }
                StartCoroutine(TurnCam());
                StopCoroutine(EnemyMove(collision.GetComponent<EnemyHPMaster>()));
                StartCoroutine(EnemyMove(collision.GetComponent<EnemyHPMaster>()));
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * 0.4f * GameManager.Instance.CanMove(), ForceMode2D.Impulse);
                collision.transform.position = new Vector3(Player.transform.position.x + 2.5f * _player.localScale.x, Player.transform.position.y, 0);
                collision.GetComponent<EnemyHPMaster>().GetDamage(AttackNum * 1);
            }
        }
    }
    IEnumerator EnemyMove(EnemyHPMaster enemy)
    {
        enemy.SetMove(0);
        yield return new WaitForSeconds(0.6f);
        enemy.SetMove(1);
    }
}
