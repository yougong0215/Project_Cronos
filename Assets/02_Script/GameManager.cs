using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Transform DamagedUI;

    private Camera _camera = null;
    public Camera _Camera
    {
        get
        {
            if (_camera == null)
            {
                _camera = GameObject.Find("MainCamera").GetComponent<Camera>();
            }
            return _camera;
        }
    }

}
