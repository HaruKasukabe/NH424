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
        text.text = "モチーフ：" + sta.motifName + "\n名前：" + sta.charName;
        text.text += "\n\n持っているタグ\n";
        for (int i = 0; i < sta.unitTag.Length; i++)
            text.text += "" + sta.unitTag[i].ToString() + "\n";
        text.text += "\n木材獲得量：" + sta.wood + "\n石材獲得量：" + sta.stone + "\n鉄材獲得量：" + sta.iron + "\n食材獲得量：" + sta.food.ToString("f0");
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
