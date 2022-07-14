using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    private Vector3 direction;

    private float _angle;

    //addforce
    [SerializeField]
    private float speed = 5;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Vector3 direction = (transform.position - player.transform.position).normalized * -100;
        transform.DOMove(direction, 30f);
        //transform.DOLookAt(direction, 1f);

        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }


    private void Update()
    {

        //transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime *15);
        //transform.position += direction * speed * Time.deltaTime;
    }







    private void OnDestroy()
    {
        //Instantiate(explosion);
    }
    private void OnBecameInvisible()
    {
        //StartCoroutine(OutOfCamera());
    }
    private IEnumerator OutOfCamera()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}


/*
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Vector3 direction = (transform.position - player.transform.position).normalized * -100;
        transform.DOMove(direction, 30f);
        //transform.DOLookAt(direction, 1f);

        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
*/