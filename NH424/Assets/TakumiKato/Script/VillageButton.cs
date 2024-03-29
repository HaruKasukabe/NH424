//=============================================================================
//
//  村ボタン　クラス [VillageButton.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageButton : MonoBehaviour
{
    public static VillageButton instance = null;

    bool bCanMenu = true;   // メニューを表示させられるか
    RectTransform trs;
    RectTransform listTrs;
    public GameObject villageMenu;
    public GameObject villageImageList;

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
        trs = villageMenu.GetComponent<RectTransform>();
        listTrs = villageImageList.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // LBを押しながら
        if (Input.GetButton("Trigger_L"))
        {
            // Yを押す
            if (Input.GetButtonDown("Jump"))
            {
                if (bCanMenu)   // 村メニューを表示する
                {
                    trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
                    listTrs.position = new Vector2(Screen.width / 2, Screen.height / 6);
                    GameManager.instance.SetUICursol(true);
                    Tutorial.instance.Kemokokoutai();

                    bCanMenu = false;
                }
                else　          // 村メニューを非表示にする
                {
                    trs.position += new Vector3(-9999, 0, 0);
                    listTrs.position += new Vector3(-9999, 0, 0);
                    GameManager.instance.SetUICursol(false);

                    bCanMenu = true;
                }
            }
        }
        // Bを押してメニューを消す
        else if (Input.GetButtonDown("Fire2") && !bCanMenu)
        {
            //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
            trs.position += new Vector3(-9999, 0, 0);
            listTrs.position += new Vector3(-9999, 0, 0);
            GameManager.instance.SetUICursol(false);
            bCanMenu = true;
        }
    }

    public void OnClick()
    {
        if (bCanMenu)
        {
            trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
            listTrs.position = new Vector2(Screen.width / 2, Screen.height / 6);
            Tutorial.instance.Kemokokoutai();

            bCanMenu = false;
        }
        else
        {
            trs.position += new Vector3(-9999, 0, 0);
            listTrs.position += new Vector3(-9999, 0, 0);

            bCanMenu = true;
        }
    }
    public bool GetbMenu()
    {
        return bCanMenu;
    }
}
