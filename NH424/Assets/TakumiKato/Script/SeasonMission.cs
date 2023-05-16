using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonMission : MonoBehaviour
{
    public static SeasonMission instance = null;

    TextMeshProUGUI text;

    float baseSeasonMaterial = 30.0f;
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
        text.text = "–ØÞF" + seasonMaterial + " ˆÈã" + " ÎÞF" + seasonMaterial + " ˆÈã" + " “SÞF" + seasonMaterial + " ˆÈã";
    }

    public bool Check(SEASON season, int roundNum)
    {
        if (SpringMission())
            return true;

        //switch (season)
        //{
        //    case SEASON.SPRING:
        //        SpringMission();
        //        break;
        //    case SEASON.SUMMER:
        //        SummerMission(roundNum);
        //        break;
        //    case SEASON.FALL:
        //        FallMission(roundNum);
        //        break;
        //    case SEASON.WINTER:
        //        WinterMission(roundNum);
        //        break;
        //    default:
        //        return false;
        //}

        return false;
    }

    bool SpringMission()
    {
        if (GameManager.instance.wood >= seasonMaterial)
            if (GameManager.instance.stone >= seasonMaterial)
                if (GameManager.instance.iron >= seasonMaterial)
                {
                    seasonMaterial += baseSeasonMaterial;
                    return true;
                }
        return false;
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
