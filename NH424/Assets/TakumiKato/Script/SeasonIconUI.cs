//=============================================================================
//
// シーズンアイコンUI クラス [SeasonIconUI.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonIconUI : MonoBehaviour
{
    Image image;
    public Sprite[] sprite;

    public static bool bChangeSeasonIcon = false;

    public static SeasonIconUI instance = null;

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


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bChangeSeasonIcon)
        {
            image.sprite = sprite[(int)GameManager.instance.season];
        }
    }

    // 季節アイコン切り替えフラグ設定
    public void SetSeasonIcon()
    {
        bChangeSeasonIcon = true;
    }
}
