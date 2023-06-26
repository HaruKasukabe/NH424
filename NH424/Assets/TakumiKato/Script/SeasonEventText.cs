//=============================================================================
//
// シーズンイベントテキスト クラス [SeasonEventText.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonEventText : MonoBehaviour
{
    public TextMeshProUGUI[] seasonEventText;
    SEASON season = SEASON.SPRING;

    // Start is called before the first frame update
    void Start()
    {
        seasonEventText[(int)season].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // 季節が変わった時
        if(season != GameManager.instance.season)
        {
            seasonEventText[(int)season].gameObject.SetActive(false);
            season = GameManager.instance.season;
            seasonEventText[(int)season].gameObject.SetActive(true);
        }
    }
}
