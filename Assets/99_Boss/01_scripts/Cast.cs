using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private void Start()
    {
        
    }

    private IEnumerator CreateBullet()
    {
        int a = 0;
        while ( a  < 5)
        {
            a++;
            Instantiate(bullet);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
