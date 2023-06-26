//=============================================================================
//
//  タグUIテキスト [TagUIText.cpp]
//
//=============================================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagUIText : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        tagData[] tag = UnitTagAbility.instance.tagDatas;

        text.text = "";
        for (int i = 0; i < tag.Length; i++)
            text.text += Enum.GetName(typeof(UnitTag), i) + ":" + tag[i].num;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetTagUI()
    {
        tagData[] tag = UnitTagAbility.instance.tagDatas;

        text.text = "";
        for (int i = 0; i < tag.Length; i++)
            text.text += Enum.GetName(typeof(UnitTag), i) + ":" + tag[i].num + "\n";
    }
}
