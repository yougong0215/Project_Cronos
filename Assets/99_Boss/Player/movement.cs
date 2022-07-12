using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 10;

    private void Update()
    {
        float X = Input.GetAxis("Horizontal");
        transform.position += new Vector3(X * m_Speed * Time.deltaTime, 0, 0);
    }
}
