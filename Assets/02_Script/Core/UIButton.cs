using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] SkillType skillType;

    [SerializeField] TextMeshProUGUI text;

    public void OnPointer()
    {
        Debug.Log("OnPointer");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (skillType)
        {
            case SkillType.정복자: text.text = "정복자 : 연속공격 횟수 증가 | 최종대미지 증가"; break;
            case SkillType.활공: text.text = "활공 : Space(W)를 hold하여 활공"; break;
            case SkillType.불사의토뎀: text.text = "부활 : 1번의 죽음 무시"; break;
        }

        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "";

        Debug.Log("The cursor exited the selectable UI element.");
    }

}

public enum SkillType
{
    정복자,
    활공,
    불사의토뎀
}

/*
 *     }
private static bool IsPointerOverUI()
=> UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
[SerializeField] TextMeshProUGUI _tmp;
// Update is called once per frame
void Update()
{

    if(IsPointerOverUI() == true)
    {
        Debug.Log(gameObject.name);

        switch(gameObject.name)
        {
            case "BokJa":
                _tmp.text = "정복자 : 연속공격횟수 증가";
                break;
            case "Fly":
                _tmp.text = "활공 : Space(W)를 꾹눌러 활공가능";
                break;

        }
    }
    else
    {
        _tmp.text = "";
    }
}
*/