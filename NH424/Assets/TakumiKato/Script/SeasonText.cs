//=============================================================================
//
// シーズンテキスト クラス [SeasonText.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonText : MonoBehaviour
{
    TextMeshProUGUI text;
    int turn = 1;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "" + turn;
    }

    // Update is called once per frame
    void Update()
    {
        if (turn != GameManager.instance.nowTurn)
        {
            turn = GameManager.instance.nowTurn;
            text.text = "" + turn;
            // 26ターンで文字を赤色に
            if (turn == 26)
                text.color = new Color(1, 0, 0);
            if (turn == 1)
                text.color = new Color(0, 0, 0);
        }
    }
}
