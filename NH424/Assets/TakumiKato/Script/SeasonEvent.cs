//=============================================================================
//
// シーズンイベント クラス [SeasonEvent.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonEvent : MonoBehaviour
{
    public static SeasonEvent instance = null;

    List<Hex> hexList = new List<Hex>();    // イベントヘクスリスト
    bool bSetEvent = true;                  // イベントを設定するか

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 最初のリセット後に
        if (!GameManager.instance.bFirstReset)
        {
            if (bSetEvent)
                SetEvent(GameManager.instance.season);
            else
                CheckEvent(GameManager.instance.season);
        }
    }

    // イベントを達成できたか確認
    void CheckEvent(SEASON season)
    {
        switch (season)
        {
            case SEASON.SPRING:
                CheckSpringEvent();
                break;
            case SEASON.SUMMER:
                break;
            case SEASON.FALL:
                break;
            case SEASON.WINTER:
                CheckWinterEvent();
                break;
        }
    }
    // イベントを発生させる
    void SetEvent(SEASON season)
    {
        hexList.Clear();
        switch (season)
        {
            case SEASON.SPRING:
                hexList = Map.instance.GetRandomHex(2);
                for(int i = 0; i < hexList.Count; i++)
                    hexList[i].SetEventHex(true);
                break;
            case SEASON.SUMMER:
                break;
            case SEASON.FALL:
                break;
            case SEASON.WINTER:
                hexList = Map.instance.GetRandomHex(3);
                for (int i = 0; i < hexList.Count; i++)
                    hexList[i].SetEventHex(true);
                break;
        }
        bSetEvent = false;
    }
    // イベントマスを消す
    void ChangeEvent()
    {
        if (!bSetEvent)
        {
            for (int i = 0; i < hexList.Count; i++)
                hexList[i].SetEventHex(false);
        }
    }
    // 春イベント確認
    void CheckSpringEvent()
    {
        bool b = true;
        for (int i = 0; i < hexList.Count; i++)
        {
            // ユニットがいなかったらfalseに
            if (!hexList[i].bUnit)
                b = false;
        }
        // ユニットが全イベントマスにいるなら
        if (b)
        {
            for (int j = 0; j < hexList.Count; j++)
                hexList[j].SetEventHex(false);

            List<Hex> list = Map.instance.GetRandomHex(2);
            for (int i = 0; i < list.Count; i++)
                list[i].ChangeEventHex(0);
            bSetEvent = true;
        }
    }
    // 冬イベント確認
    void CheckWinterEvent()
    {
        bool b = true;
        for (int i = 0; i < hexList.Count; i++)
        {
            // ユニットがいなかったらfalseに
            if (!hexList[i].bUnit)
                b = false;
        }
        // ユニットが全イベントマスにいるなら
        if (b)
        {
            for (int j = 0; j < hexList.Count; j++)
                hexList[j].SetEventHex(false);

            List<Hex> list = Map.instance.GetRandomHex(2);
            for (int i = 0; i < list.Count; i++)
                list[i].ChangeEventHex(1);
            bSetEvent = true;
        }
    }
    // イベントをリセットする
    public void ResetEvent()
    {
        ChangeEvent();
        SetEvent(GameManager.instance.season);
    }
    // マップリセット時に遅らせてからイベントをリセット
    public void ResetMap()
    {
        Invoke("ResetEvent", 2);
    }
}
