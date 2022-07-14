using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Explosive());
    }
    private IEnumerator Explosive()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
