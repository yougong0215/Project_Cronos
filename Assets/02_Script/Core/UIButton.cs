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
            case SkillType.Bokja: text.text = "������ : ���Ӱ��� Ƚ�� ���� | ��������� ����"; break;
            case SkillType.fly: text.text = "Ȱ�� : Space(W)�� hold�Ͽ� Ȱ��"; break;
            case SkillType.Todam: text.text = "��Ȱ : 1���� ���� ����"; break;
            case SkillType.Lian: text.text = "���� : ���ݸ��� ƽ����� �߰�"; break;
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
    Bokja,
    fly ,
    Todam,
    Lian
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
                _tmp.text = "������ : ���Ӱ���Ƚ�� ����";
                break;
            case "Fly":
                _tmp.text = "Ȱ�� : Space(W)�� �ڴ��� Ȱ������";
                break;

        }
    }
    else
    {
        _tmp.text = "";
    }
}
*/