//=============================================================================
//
// メインミッションのテキスト クラス [MainMissionText.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMissionText : MonoBehaviour
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
        float seasonMaterial = SeasonMission.instance.seasonMaterial;
        text.text = "木材：" + seasonMaterial + " 以上" + " 石材：" + seasonMaterial + " 以上" + " 鉄材：" + seasonMaterial + " 以上";
    }
}
