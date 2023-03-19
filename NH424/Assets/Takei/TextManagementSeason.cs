// ======================================================
//  TextManagementSeason.cs[季節のタイマー表示]
// 
// Author:武井遥都
// ======================================================
// 変更履歴
// 2023/02/27 スクリプト作成・テキスト表示実装
// ======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManagementSeason : MonoBehaviour
{
    // 変数宣言
    public TextMeshProUGUI DisplaySeason;
    public ManagementTime managementtime;
    private int Minute;
    private int second;

    // Update is called once per frame
    void Update()
    {
        Minute = (int)managementtime.GetSeasonTime() / 60;
        second = (int)managementtime.GetSeasonTime();
        DisplaySeason.text = managementtime.GetNowLap().ToString() + "週目" + managementtime.GetNowSeason() + "\n" +
           second.ToString("D3");
            //Minute.ToString("D2") + ":" + second.ToString("D2");
    }
}
