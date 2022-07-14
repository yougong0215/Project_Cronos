using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class StartPlaying : MonoBehaviour
{

    Transform _move;
    [SerializeField] CinemachineConfiner _cam;
    [SerializeField] CinemachineConfiner _cam2;
    void Start()
    {
        _move = GameObject.Find("Player").GetComponent<Transform>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Stage11");
            _move.position = new Vector3(0, 0, 0);
        }
        try
        {
            _cam.m_BoundingShape2D = GameObject.Find("Poligon").GetComponent<PolygonCollider2D>();
            _cam2.m_BoundingShape2D = GameObject.Find("Poligon").GetComponent<PolygonCollider2D>();
        }
        catch
        {

        }
    }
}
