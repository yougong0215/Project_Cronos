using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : PoolAble
{
    [SerializeField]
    private GameObject explosion;
    private Vector3 direction;

    private Vector3 diff;

    private float rot_z;
        
    private Transform player;

    private void Start()
    {
        StartCoroutine(Destroid());
        Instantiate(explosion, transform.position, Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        direction = (transform.position - player.transform.position).normalized * -100;
        transform.DOMove(direction, 30f);

        diff = player.transform.position - transform.position;
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
    private void OnDestroy()
    {
        Debug.Log("Dead");
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
    private IEnumerator Destroid()
    {
        Debug.Log("sex");
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}