using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternType : MonoBehaviour
{
    private Renderer color;
    private enum _PatternType
    {
        Gallop,
        Attack,
        SpawnEnemy
    }
    private void Start()
    {
             color = GetComponent<Renderer>();
    }
    private void Update()
    {
         
    }
}
