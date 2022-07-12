using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    float currentTime = 0;
    float TimeLeafCool = 0;
    int _timecode;
    Transform DamagedUI;
    [SerializeField] TextMeshProUGUI Cooldown;
    bool _bTimereaf = false;
    float _canMove = 1;
    float _MoveTimeArrange = 1;
    bool _TimeStop = false;

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
    private void Update()
    {
        if (_TimeStop == false)
        {
            if (Input.GetKeyDown(KeyCode.Q) && TimeLeafCool <= 0)
            {

                if (_bTimereaf == false)
                {
                    TimeLeafCool = 10;
                    _TimeStop = false;
                    _bTimereaf = true;
                }
            }
            currentTime += Time.deltaTime;

            if (_bTimereaf == true)
            {
                if (currentTime > 0.1f)
                {
                    currentTime = 0;
                    if (_timecode >= 29)
                    {
                        _bTimereaf = false;
                        _timecode = 0;
                        return;
                    }

                    _timecode++;
                }
            }
        }
        if (_bTimereaf == false)
        {

            if (Input.GetKeyDown(KeyCode.T) && _TimeStop == false && TimeLeafCool <= 0)
            {
                currentTime = 0;
                _TimeStop = true;
                TimeLeafCool = 10;
                _canMove = 0.1f;
                _MoveTimeArrange = 10;
                _bTimereaf = false;
            }
            if (TimeLeafCool <= 0)
            {
                TimeLeafCool = 0;
            }
            if(_TimeStop == true)
            {
                currentTime += Time.deltaTime;
                if(currentTime >= 3f)
                {
                    Debug.Log("adf");
                    _canMove = 1f;
                    _MoveTimeArrange = 1;
                    TimeLeafCool = 10;
                    currentTime = 0;
                    _TimeStop = false;
                }
            }
            
        }
        Cooldown.text = $"CoolDown : {TimeLeafCool} ";
        if (_bTimereaf == false || _TimeStop == false)
            TimeLeafCool -= Time.deltaTime;
    }

    public float CanMove()
    {
        return _canMove;
    }
    public float TimeArrange()
    {
        return _MoveTimeArrange;
    }
    public bool Timer()
    {
        return _bTimereaf;
    }

}
