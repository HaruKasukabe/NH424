using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonEvent : MonoBehaviour
{
    public static SeasonEvent instance = null;

    List<Hex> hexList = new List<Hex>();
    bool bSetEvent = true;

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
        if (!GameManager.instance.bFirstReset)
        {
            if (bSetEvent)
                SetEvent(GameManager.instance.season);
            else
                CheckEvent(GameManager.instance.season);
        }
    }

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
    public void SetEvent(SEASON season)
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
    public void ChangeEvent()
    {
        if (!bSetEvent)
        {
            for (int i = 0; i < hexList.Count; i++)
                hexList[i].SetEventHex(false);
        }
    }
    void CheckSpringEvent()
    {
        bool b = true;
        for (int i = 0; i < hexList.Count; i++)
        {
            if (!hexList[i].bUnit)
                b = false;
        }
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
    void CheckWinterEvent()
    {
        bool b = true;
        for (int i = 0; i < hexList.Count; i++)
        {
            if (!hexList[i].bUnit)
                b = false;
        }
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
    public void ResetEvent()
    {
        ChangeEvent();
        SetEvent(GameManager.instance.season);
    }
    public void ResetMap()
    {
        Invoke("ResetEvent", 3);
    }
}
