using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    AudioSource _audio;
    [SerializeField] Animator image;
    [SerializeField] Image _MPUI;
    float currentTime = 0;
    float TimeLeafCool = 0;
    int _timecode;
    Transform DamagedUI;
    bool _bTimereaf = false;
    float _canMove = 1;
    float _MoveTimeArrange = 1;
    bool _TimeStop = false;
    bool _guardSuc = false;
    float TimeCool = 0;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void GetGurid()
    {
        _guardSuc = true;
    }

    private void Update()
    {
        if (_TimeStop == false)
        {
            if (Input.GetKeyDown(KeyCode.Q) && _MPUI.fillAmount == 1)
            {

                if (_bTimereaf == false)
                {
                    _MPUI.fillAmount = 0;
                    _TimeStop = false;
                    _bTimereaf = true;
                    image.SetBool("Leaf", true);
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
                        image.SetBool("Leaf", false);
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

            if (_guardSuc == true)
            {
                _guardSuc = false;
                currentTime = 0;
                _TimeStop = true;
                TimeCool = 1;
                _canMove = 0.1f;
                _MoveTimeArrange = 10;
                _bTimereaf = false;
            }
            if (TimeCool <= 0)
            {
                TimeCool = 0;
            }
            if(_TimeStop == true)
            {
                currentTime += Time.deltaTime;
                if(currentTime >= 1f)
                {
                    Debug.Log("adf");
                    _canMove = 1f;
                    _MoveTimeArrange = 1;
                    TimeCool = 3;
                    currentTime = 0;
                    _TimeStop = false;
                }
            }
            
        }
        if (_bTimereaf == false || _TimeStop == false)
            _MPUI.fillAmount += (Time.deltaTime/10);
    }
    public void SoundPlay(AudioClip ad)
    {
        _audio.PlayOneShot(ad);
    }
    public void BGSoundPlay(AudioClip ad)
    {
        _audio.Stop();
        _audio.PlayOneShot(ad);
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
