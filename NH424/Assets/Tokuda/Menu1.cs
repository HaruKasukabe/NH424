// ================================================
// MenuSC.cs[メニュー画面の管理]
// 
// Author:徳田亮
//=================================================
// 作成履歴
// 2023/05/02 
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu1 : MonoBehaviour
{

    //変数宣言
    private bool OpenMMenu; //メインのメニューの画面が開かれているか
    private bool OpenOMenu; //メニューのオプション画面が開かれているか
    private bool OpenSMenu; //メニューの操作画面が開かれているか
    private bool OpenZMenu; //メニューの図鑑画面が開かれているか
    private bool OpenMenu;  //メニュー画面が開かれているか
    public GameObject ImageMMenu;
    public GameObject ImageOMenu;
    public GameObject ImageSMenu;
    public GameObject ImageZMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        OpenMMenu = false;
        OpenOMenu = false;
        OpenSMenu = false;
        OpenMenu = false;
        ImageMMenu.SetActive(OpenMMenu);  //最初は非表示のためfalse
    }

    // Update is called once per frame
    void Update()
    {
        //キーボードのLでメニューを開く
        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Menu"))
        {
            //メニュー画面が開かれていない
            if(!OpenMenu)
            {
                OpenMenu = true;
                //メニュー画面を開く
                OpenMMenu = true;
                ImageMMenu.SetActive(OpenMMenu);

                GameManager.instance.SetUICursol(true);
            }
        }
    }

    // ==========================
    // ボタン処理関数
    // ==========================
    // ゲームに戻るを押したとき
    public void SetMenu()
    {
        //メニュー画面が開かれていない
        if(!OpenMenu)
        {
            OpenMenu = true;
            //メニュー画面を開く
            OpenMMenu = true;
            ImageMMenu.SetActive(OpenMMenu);
        }
        else if (OpenMenu)
        {
            //全てのメニュー画面をオフにする
            OpenMenu = false;
            OpenMMenu = false;
            OpenOMenu = false;
            OpenSMenu = false;
            OpenZMenu = false;
            ImageMMenu.SetActive(OpenMMenu);
            ImageOMenu.SetActive(OpenOMenu);
            ImageSMenu.SetActive(OpenSMenu);
            ImageZMenu.SetActive(OpenZMenu);
        }
    }
    public void SetActiveFlg()
    {
        OpenMMenu = !OpenMMenu;
        ImageMMenu.SetActive(OpenMMenu);
    }

    //オプションを押したとき
    public void SetOptionSetting()
    {
        //まずはメニュー画面を閉じる
        OpenMMenu = !OpenMMenu;
        ImageMMenu.SetActive(OpenMMenu);
        OpenOMenu = !OpenOMenu;
        ImageOMenu.SetActive(OpenOMenu);
    }
   
    //操作方法を押したとき
    public void SetSousaSetting()
    {
        //まずはメニュー画面を閉じる
        OpenMMenu = !OpenMMenu;
        ImageMMenu.SetActive(OpenMMenu);
        OpenSMenu = !OpenSMenu;
        ImageSMenu.SetActive(OpenSMenu);
    }
}
