using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectCharaUI : MonoBehaviour
{
    TextMeshProUGUI text;
    UnitData sta;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        sta = Map.instance.firstUnit.GetComponent<Unit>().sta;
        SetText();
        transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetText()
    {
        text.text = "���`�[�t�F" + sta.motifName + "\n���O�F" + sta.charName;
        text.text += "\n\n�����Ă���^�O\n";
        for (int i = 0; i < sta.unitTag.Length; i++)
            text.text += "" + sta.unitTag[i].ToString() + "\n";
        text.text += "\n�؍ފl���ʁF" + sta.wood + "\n�΍ފl���ʁF" + sta.stone + "\n�S�ފl���ʁF" + sta.iron + "\n�H�ފl���ʁF" + sta.food.ToString("f0");
    }

    public void SetUnit(Unit unit)
    {
        if (sta.number != unit.sta.number)
        {
            sta = unit.sta;

            SetText();
        }
    }
}
