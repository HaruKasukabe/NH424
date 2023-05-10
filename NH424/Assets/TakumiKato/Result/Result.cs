using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    TextMeshProUGUI text;
    SpriteRenderer rend;

    string empty = "  ";

    // Start is called before the first frame update
    void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        rend = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetText(UNIT_SCORE score)
    {
        rend.sprite = score.sprite.GetComponent<Image>().sprite;
        text.text = "" + score.motifName + empty + score.charName +
                        empty + "仲間にした数：" + score.friendNum + empty + "開けたマスの数：" + score.reverseHexNum +
                        empty + "食材：" + score.food + " 木材：" + score.wood + " 石材：" + score.stone + " 鉄材：" + score.iron;
    }
}
