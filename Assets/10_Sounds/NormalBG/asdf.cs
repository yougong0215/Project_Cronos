using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asdf : MonoBehaviour
{
    [SerializeField] AudioSource _au;
    void Start()
    {
        
    }



// Update is called once per frame
void Update()
    {

        if(_au.isPlaying ==false)
        {
            _au.Play();
        }
    }
}
