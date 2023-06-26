//=============================================================================
//
// 村メニューの左側の家のテキスト クラス [LeftHomeText.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftHomeText : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "現在のLv" + GameManager.instance.level + "\n木石鉄必要素材量：" + GameManager.instance.levelUpNeed;
    }
}
