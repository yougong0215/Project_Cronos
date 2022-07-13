using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    [SerializeField] PlayerMove _player;

    Animator _ani;
    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetMove() == true)
        {
            _ani.SetBool("Run", true);
        }
        else if (_player.GetMove() == false)
        {
            _ani.SetBool("Run", false);
        }
    }
}
