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
            case SkillType.������: text.text = "������ : ���Ӱ��� Ƚ�� ���� | ��������� ����"; break;
            case SkillType.Ȱ��: text.text = "Ȱ�� : Space(W)�� hold�Ͽ� Ȱ��"; break;
            case SkillType.�һ����䵩: text.text = "��Ȱ : 1���� ���� ����"; break;
            case SkillType.���ȵ帮: text.text = "���� : ���ݸ��� ƽ����� �߰�"; break;
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
    ������,
    Ȱ��,
    �һ����䵩,
    ���ȵ帮
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