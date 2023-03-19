// ================================================
//  ManagementTime.cs[時間管理スクリプト]
// 
// Author:武井遥都
//=================================================
// 変更履歴
// 2023/02/25 スクリプト作成
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementTime : MonoBehaviour
{
    // 列挙体宣言
    enum Season
    {
        Spring = 0,
        Summer,
        Fall,
        Winter
    }
    // 変数宣言
    float seasontime; // 季節のタイマー管理
    int lap; // 春夏秋冬が何回過ぎたか
    Season season; // 季節の列挙体
    bool StartFlg;


    // Start is called before the first frame update
    void Start()
    {
        StartFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartFlg)
        {
            // タイマー計算(FPS60想定)
            seasontime -= Time.deltaTime;
            Debug.Log("現在時間:" + seasontime);
            if (seasontime <= 0.0f)
            {
                if ((int)season == 3)
                {
                    lap += 1;
                    season = 0;
                    seasontime = 300.0f;
                }
                else
                {
                    season += 1;
                    seasontime = 300.0f;
                }

            }
        }
    }

    // ゲームを開始
    public void GameStart()
    {
        // タイマーをリセット
        seasontime = 300.0f; // デフォルトは5分
        season = 0;
        lap = 1;
        StartFlg = true;
    }

    // 現在の季節時間を返す
    public float GetSeasonTime()
    {
        return seasontime;
    }
    // 現在の季節を返す
    public string GetNowSeason()
    {
        switch(season)
        {
            case Season.Spring:
                return "春";
            case Season.Summer:
                return "夏";
            case Season.Fall:
                return "秋";
            case Season.Winter:
                return "冬";
        }
        return "エラー";
    }
    // 現在の春夏秋冬が何周目か返す
    public int GetNowLap()
    {
        return lap;
    }
}
