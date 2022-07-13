using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineVirtualCamera _normalCam;
    [SerializeField] CinemachineVirtualCamera _RigCam;
    void Start()
    {
        
    }
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
    // Update is called once per frame
    public void TurnCamNormal()
    {
        _normalCam.Priority = 10;
        _RigCam.Priority = 5;
    }
    public void TurnCamRig()
    {
        _normalCam.Priority = 5;
        _RigCam.Priority = 10;
    }

}
