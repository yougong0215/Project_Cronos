using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLayer()
    {
        StopAllCoroutines();
        gameObject.layer = 22;
        StartCoroutine(ReturnLayer());
    }
    IEnumerator ReturnLayer()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.layer = 21;
    }
}
