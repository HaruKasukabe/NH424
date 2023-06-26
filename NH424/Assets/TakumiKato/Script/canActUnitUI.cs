//=============================================================================
//
// 1ターン内のユニット移動回数UI クラス [CanActUnitUI.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanActUnitUI : MonoBehaviour
{
    [SerializeField] Image[] colors;    // UI5枚の配列
    int actUnitNum;                     // 動けるユニットの数

    // Start is called before the first frame update
    void Start()
    {
        actUnitNum = GameManager.instance.canActUnitNum - 1;    // 添え字の関係で-1する
    }

    // Update is called once per frame
    void Update()
    {
        // 格納した動けるユニットの数と今動けるユニットの数が違う場合
        if (actUnitNum != GameManager.instance.canActUnitNum - 1)
        {
            actUnitNum = GameManager.instance.canActUnitNum - 1;
            for (int i = 0; i < 5; i++)
            {
                // ユニットの数以下の添え字かどうか
                if (actUnitNum >= i)
                    colors[i].color = new Color(1,1,1,1);
                else
                    colors[i].color = new Color(1,1,1, 0.25f);
            }
        }
    }
}
