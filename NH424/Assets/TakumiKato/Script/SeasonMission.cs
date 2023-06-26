//=============================================================================
//
// シーズンミッション クラス [SeasonMission.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonMission : MonoBehaviour
{
    public static SeasonMission instance = null;

    TextMeshProUGUI text;

    float baseSeasonMaterial = 30.0f;   // 最初に必要な素材量
    public float seasonMaterial;

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

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        seasonMaterial = baseSeasonMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "木材：" + seasonMaterial + " 以上" + " 石材：" + seasonMaterial + " 以上" + " 鉄材：" + seasonMaterial + " 以上";
    }

    // シーズンミッション達成確認
    public bool Check()
    {
        if (BaseMission())
            return true;

        return false;
    }

    bool BaseMission()
    {
        // 素材があるか確認
        if (GameManager.instance.wood >= seasonMaterial)
            if (GameManager.instance.stone >= seasonMaterial)
                if (GameManager.instance.iron >= seasonMaterial)
                {
                    seasonMaterial += baseSeasonMaterial;   // 次回に必要な素材量を増加
                    GameManager.instance.wood -= seasonMaterial;
                    GameManager.instance.stone -= seasonMaterial;
                    GameManager.instance.iron -= seasonMaterial;
                    return true;
                }
        return false;
    }
    bool SpringMission(int roundNum)
    {
        return true;
    }
    bool SummerMission(int roundNum)
    {
        return true;
    }
    bool FallMission(int roundNum)
    {
        return true;
    }
    bool WinterMission(int roundNum)
    {
        return true;
    }
}
