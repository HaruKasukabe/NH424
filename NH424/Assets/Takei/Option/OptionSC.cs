// ================================================
//  OptionSC.cs[メインのオプション管理]
// 
// Author:武井遥都
//=================================================
// 変更履歴
// 2023/02/27 スクリプト作成・サウンド関連・
// カメラ感度・オプション画面作成
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionSC : MonoBehaviour
{
    // スクリプト参照
    public ManagementAudio m_audiosc; // オーディオ管理スクリプト

    // 変数宣言
    private float Camerasensitivity; // カメラ感度保存関数
    private bool OpenMOption; // メインのオプションの画面が開かれているか
    private bool OpenCOption; // カメラのオプション画面が開かれているか
    private bool OpenSOption; // サウンドのオプション画面が開かれているか
    private bool OpenPictorialBook;
    private bool OpenOption; // オプション画面が開かれているか
    public GameObject ImageMOption; // メインのOptionのUI画像
    public GameObject ImageCOption; // カメラのOptionのUI画像
    public GameObject ImageSOption; // サウンドのOptionのUI画像
    public PictorialBook PictorialBook;

    [SerializeField] GameCamera cam;
    [SerializeField] PadSlider padSlider;
    [SerializeField] Button MOptionButton;

    // Initialize
    void Start()
    {
        OpenMOption = false;
        OpenCOption = false;
        OpenSOption = false;
        OpenPictorialBook = false;
        OpenOption = false;
        ImageMOption.SetActive(OpenMOption); // 最初は非表示のためfals
    }

    // Update is called once per frame
    void Update()
    {
        // キーボードのMでオプションを開く
        if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("Option"))
        {
            SetOption();
        }
        if (Input.GetButtonDown("Fire2") && OpenOption)
        {
            //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
            // 全てのオプション画面をオフにする
            OpenOption = false;
            OpenMOption = false;
            OpenCOption = false;
            OpenSOption = false;
            OpenPictorialBook = true;
            ImageMOption.SetActive(OpenMOption);
            ImageCOption.SetActive(OpenCOption);
            ImageSOption.SetActive(OpenSOption);
            NoPictorialBook();
            padSlider.nullSlider();
        }
    }

    // ==========================
    // ボタン処理関数
    // ==========================
    // ゲームに戻るを押したとき
    public void SetOption()
    {
        // オプション画面が開かれていいない
        if (!OpenOption)
        {
            OpenOption = true;
            // メインのオプション画面開く
            OpenMOption = true;
            ImageMOption.SetActive(OpenMOption);
            MOptionButton.Select();
        }
        else if (OpenOption)
        {
            // 全てのオプション画面をオフにする
            OpenOption = false;
            OpenMOption = false;
            OpenCOption = false;
            OpenSOption = false;
            OpenPictorialBook = true;
            ImageMOption.SetActive(OpenMOption);
            ImageCOption.SetActive(OpenCOption);
            ImageSOption.SetActive(OpenSOption);
            NoPictorialBook();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public void SetActiveFlg()
    {
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
    }
    // カメラ設定を押したとき
    public void SetCameraSetting()
    {
        // まずはメインのオプション画面を閉じる
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
        OpenCOption = !OpenCOption;
        ImageCOption.SetActive(OpenCOption);
        if (OpenCOption)
            padSlider.SetCameraSlider();
        else
        {
            padSlider.nullSlider();
            MOptionButton.Select();
        }
    }
    // サウンド設定関連を押したとき
    public void SetSoundSetting()
    {
        // まずはメインのオプション画面を反転
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
        // サウンド設定の画面を反転
        OpenSOption = !OpenSOption;
        ImageSOption.SetActive(OpenSOption);
        if (OpenSOption)
            padSlider.SetSoundSlider();
        else
        {
            padSlider.nullSlider();
            MOptionButton.Select();
        }
    }
    
    public void SetPictorialBook()
    {
        // まずはメインのオプション画面を反転
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
        OpenPictorialBook = !OpenPictorialBook;
        if (OpenPictorialBook)
        {
            PictorialBook.SetDisplay();
        }
        else
        {
            PictorialBook.BacktoMapButton();
            MOptionButton.Select();
        }
    }
    void NoPictorialBook()
    {
        OpenPictorialBook = !OpenPictorialBook;
        if (OpenPictorialBook)
        {
            PictorialBook.SetDisplay();
        }
        else
            PictorialBook.BacktoMapButton();
    }

    // =============================
    // カメラのスライダー処理関数
    // =============================
    // スライダーの値をメイン管理クラスで保存する
    public void SetCameraSensitivity(float sensitivity)
    {
        // カメラ感度を保存する
        cam.sensitiveMovePad =  sensitivity;
    }
    // カメラ操作関数で呼び出す用
    public float ReturnCameraSensitivity()
    {
        return Camerasensitivity;
    }
    
    // =============================
    // サウンドのスライダー処理関数
    // =============================
    public void SetBGMVolume(float volume)
    {
        m_audiosc.SetBGMVolume(volume);
    }
    public void SetSEVolume(float volume)
    {
        m_audiosc.SetSEVolume(volume);
    }

    public bool bOpenOption()
    {
        return OpenOption;
    }
}
