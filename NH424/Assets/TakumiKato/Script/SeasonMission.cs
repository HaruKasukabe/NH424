using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonMission : MonoBehaviour
{
    public static SeasonMission instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Check(SEASON season)
    {
        switch(season)
        {
            case SEASON.SPRING:
                SpringMission();
                break;
            case SEASON.SUMMER:
                SummerMission();
                break;
            case SEASON.FALL:
                FallMission();
                break;
            case SEASON.WINTER:
                WinterMission();
                break;
            default:
                return false;
        }

        return false;
    }

    bool SpringMission()
    {
        return true;
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
}
