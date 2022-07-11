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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            order = PoolManager.Instance.Pop("Order1") as ObjectOrder;
            order.transform.position = transform.position;
            
        }
    }
}
