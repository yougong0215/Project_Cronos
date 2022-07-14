using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOrder : MonoBehaviour
{
    [SerializeField] Image UI;
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
        if (Input.GetKeyDown(KeyCode.Mouse1) && UI.fillAmount == 1)
        {
            UI.fillAmount = 0;
            StartCoroutine(Corutine());
        }
    }
    IEnumerator Corutine()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            order = PoolManager.Instance.Pop("Order1") as ObjectOrder;
            order.transform.position = transform.position;
            order.transform.localEulerAngles = new Vector3(0, 0, 330);
            yield return new WaitForSeconds(0.1f);
            order = PoolManager.Instance.Pop("Order1") as ObjectOrder;
            order.transform.position = transform.position;
            order.transform.localEulerAngles = new Vector3(0, 0, 30);
            yield return new WaitForSeconds(0.1f);
            order = PoolManager.Instance.Pop("Order1") as ObjectOrder;
            order.transform.position = transform.position;
            order.transform.localEulerAngles = new Vector3(0, 0, 0);
            currentTime = 0;
        }
    }
}
