using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLaye : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 21)
        {
            collision.gameObject.layer = 22;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 21)
        {
            StartCoroutine(LayerChange(collision));
        }
    }
    IEnumerator LayerChange(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        collision.gameObject.layer = 21;
    }
}
