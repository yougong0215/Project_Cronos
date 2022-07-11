using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private Camera _camera = null;
        public Camera _Camera
    {
        get
        {
            if(_camera == null)
            {
                _camera = GameObject.Find("PlayerCam").GetComponent<Camera>();
            }
            return _camera;
        }
    }
    [SerializeField] private List<PoolAble> _PoolList;

    private void Awake()
    {
        PoolManager.Instance = new PoolManager(transform);
        foreach (PoolAble p in _PoolList)
        {
            PoolManager.Instance.CreatePool(p, 3);
        }


    }
}
