//=============================================================================
//
//  リザルト　クラス [Result.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    TextMeshProUGUI text;
    Image rend;

    string empty = "  ";

    // Start is called before the first frame update
    void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        rend = transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // リザルトの画像とテキストを設定
    public void SetText(UNIT_SCORE score)
    {
        rend.sprite = score.sprite.GetComponent<Image>().sprite;
        text.text = "" + score.motifName + empty + score.charName +
                        empty + "\n仲間にした数：" + score.friendNum + empty + "開けたマスの数：" + score.reverseHexNum +
                        empty + "\n食材：" + score.food + " 木材：" + score.wood + " 石材：" + score.stone + " 鉄材：" + score.iron;
    }
}
