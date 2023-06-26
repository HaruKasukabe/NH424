//=============================================================================
//
// ミッションボタン クラス [MissionButton.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionButton : MonoBehaviour
{
    public static MissionButton instance = null;

    bool bCanMenu = true;           // メニューを表示できるか
    RectTransform trs;              // ミッションメニューのtransform
    public GameObject MissionMenu;

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
        trs = MissionMenu.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // LBを押しながら
        if (Input.GetButton("Trigger_L"))
        {
            // Aを押す
            if (Input.GetButtonDown("Fire1"))
            {
                if (bCanMenu)   // メニューを表示する
                {
                    trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
                    GameManager.instance.SetUICursol(true);
                    bCanMenu = false;
                }
                else    // メニューを非表示にする
                {
                    //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
                    trs.position += new Vector3(-9999, 0, 0);
                    GameManager.instance.SetUICursol(false);
                    bCanMenu = true;
                }
            }
        }
        else if(Input.GetButtonDown("Fire2") && !bCanMenu)  // Bを押すとメニューを非表示にする
        {
            //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
            trs.position += new Vector3(-9999, 0, 0);
            GameManager.instance.SetUICursol(false);
            bCanMenu = true;
        }
    }

    public void OnClick()
    {
        if (bCanMenu)
        {
            trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
            bCanMenu = false;
        }
        else
        {
            trs.position += new Vector3(-9999, 0, 0);
            bCanMenu = true;
        }
    }

    // true : 今非表示, false : 今表示
    public bool GetbMenu()
    {
        return bCanMenu;
    }
}
