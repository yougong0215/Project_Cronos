using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Movement movement;
    private GameObject player;

    [SerializeField]
    private GameObject attack;
    private void Start()
    {
        movement = GetComponent<Movement>();
    }
    private void SpearSting()
    {
        Instantiate(attack, new Vector3(transform.position.x, 0, 0), Quaternion.identity);
    }
}
