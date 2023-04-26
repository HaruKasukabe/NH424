using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonMission : MonoBehaviour
{
    public static SeasonMission instance = null;

    TextMeshProUGUI text;

    int[] level;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        level = new int[8];
        for (int i = 0; i < 8; i++)
        {
            //if (i > 6)
            //    level[i] = 3;
            //else if (i > 2)
            //    level[i] = 2;
            //else
            //    level[i] = 1;
            level[i] += i + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "ƒŒƒxƒ‹ " + level[(int)GameManager.instance.season] + " ˆÈã";
    }

    public bool Check(SEASON season)
    {
        if (TentativeMission(season))
            return true;
        //switch(season)
        //{
        //    case SEASON.SPRING:
        //        SpringMission();
        //        break;
        //    case SEASON.SUMMER:
        //        SummerMission();
        //        break;
        //    case SEASON.FALL:
        //        FallMission();
        //        break;
        //    case SEASON.WINTER:
        //        WinterMission();
        //        break;
        //    case SEASON.SPRING_2:
        //        Spring_2Mission();
        //        break;
        //    case SEASON.SUMMER_2:
        //        Summer_2Mission();
        //        break;
        //    case SEASON.FALL_2:
        //        Fall_2Mission();
        //        break;
        //    case SEASON.WINTER_2:
        //        Winter_2Mission();
        //        break;
        //    default:
        //        return false;
        //}

        return false;
    }

    bool TentativeMission(SEASON season)
    {
        if (GameManager.instance.level >= level[(int)season])
            return true;

        return false;
    }

    bool SpringMission()
    {
        if(GameManager.instance.level > level[0])
            return true;

        return false;
    }
    bool SummerMission()
    {
        return true;
    }
    bool FallMission()
    {
        return true;
    }
    bool WinterMission()
    {
        return true;
    }
    bool Spring_2Mission()
    {
        return true;
    }
    bool Summer_2Mission()
    {
        return true;
    }
    bool Fall_2Mission()
    {
        return true;
    }
    bool Winter_2Mission()
    {
        return true;
    }
}
