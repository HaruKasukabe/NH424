//=============================================================================
//
// ショップボタン クラス [ShopButton.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public static ShopButton instance = null;

    bool bCanMenu = true; // メニューを表示させられるか
    RectTransform trs;
    RectTransform listTrs;
    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject villageImageList;

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
        trs = ShopMenu.GetComponent<RectTransform>();
        listTrs = villageImageList.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // LBを押しながら
        if (Input.GetButton("Trigger_L"))
        {
            // Xを押す
            if (Input.GetButtonDown("Fire3"))
            {
                if (bCanMenu)       // ショップメニューを表示する
                {
                    trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
                    listTrs.position = new Vector2(Screen.width / 2, Screen.height / 6);
                    GameManager.instance.SetUICursol(true);

                    bCanMenu = false;
                }
                else                // ショップメニューを非表示にする
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

            bCanMenu = false;
        }
        else if(!bCanMenu)
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
