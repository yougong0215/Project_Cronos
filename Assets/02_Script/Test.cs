using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class Test : MonoBehaviour
{
    Stack<Vector3> positions = new Stack<Vector3>();

    private void Update()
    {
        if(gameObject.activeSelf)
            positions.Push(transform.position);
    }


}


