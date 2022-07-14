using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    ObjectOrder order;
    float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1) && currentTime>= 3f)
        {
            StartCoroutine(Corutine());
        }
    }
    IEnumerator Corutine()
    {
        yield return new WaitForSeconds(0.01f);
        order = PoolManager.Instance.Pop("Order1") as ObjectOrder;
        order.transform.position = transform.position;
        order.transform.localEulerAngles = new Vector3(0, 0, 330);
        yield return new WaitForSeconds(0.01f);
        order = PoolManager.Instance.Pop("Order1") as ObjectOrder;
        order.transform.position = transform.position;
        order.transform.localEulerAngles = new Vector3(0, 0, 30);
        yield return new WaitForSeconds(0.01f);
        order = PoolManager.Instance.Pop("Order1") as ObjectOrder;
        order.transform.position = transform.position;
        order.transform.localEulerAngles = new Vector3(0, 0, 0);
        currentTime = 0;
    }
}
