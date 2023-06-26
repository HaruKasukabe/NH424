//=============================================================================
//
// ケモコの移動可能数テキスト クラス [MoveText.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveText : MonoBehaviour
{
    TextMeshPro text;
    Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = "";
        unit = GetComponentInParent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unit.bFriend && unit.bMoveNumDisplay)   // ユニットが仲間で移動可能数を表示する時
        {
            text.color = new Color32(255, 255, 255, 255);
            text.text = "" + unit.actNum;
            unit.bMoveNumDisplay = false;
        }
        else if(unit.bFriend && !unit.bMoveNumDisplay)
        {
            text.text = "" + unit.actNum;
            text.color = text.color - new Color32(0, 0, 0, 1);  // フェイドアウト
        }
    }
}
