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

public class OptionSC : MonoBehaviour
{
    // 変数宣言
    private float Camerasensitivity; // カメラ感度保存関数
    private bool OpenMOption; // メインのオプションの画面が開かれているか
    private bool OpenCOption; // カメラのオプション画面が開かれているか
    private bool OpenSOption; // サウンドのオプション画面が開かれているか
    private bool OpenOption; // オプション画面が開かれているか
    public GameObject ImageMOption; // メインのOptionのUI画像
    public GameObject ImageCOption; // カメラのOptionのUI画像
    public GameObject ImageSOption; // サウンドのOptionのUI画像
    public AudioSource ASbgm; // BGMのオーディオソース
    public AudioSource ASSE; // SEのオーディオソース
    [SerializeField]AudioClip BGM1; // BGMのクリップ(ソース)増えるなら別スクリプトで管理予定


    // Initialize
    void Start()
    {
        OpenMOption = false;
        OpenCOption = false;
        OpenSOption = false;
        OpenOption = false;
        ImageMOption.SetActive(OpenMOption); // 最初は非表示のためfalse
        ASbgm = GetComponent<AudioSource>();
        ASbgm.loop = true;
        ASSE = GetComponent<AudioSource>();
        ASSE.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // キーボードのMでオプションを開く
        if (Input.GetKeyDown(KeyCode.M))
        {
            // オプション画面が開かれていいない
            if(!OpenOption)
            {
                OpenOption = true;
                // メインのオプション画面開く
                OpenMOption = true;
                ImageMOption.SetActive(OpenMOption);
            }
            else if(OpenOption)
            {
                // 全てのオプション画面をオフにする
                OpenOption = false;
                OpenMOption = false;
                OpenCOption = false;
                OpenSOption = false;
                ImageMOption.SetActive(OpenMOption);
                ImageCOption.SetActive(OpenCOption);
                ImageSOption.SetActive(OpenSOption);
            }
            
        }
        // デバッグ用インプット
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayBGM(BGM1);
        }
    }

    // ==========================
    // ボタン処理関数
    // ==========================
    // ゲームに戻るを押したとき
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
    }

    // =============================
    // カメラのスライダー処理関数
    // =============================
    // スライダーの値をメイン管理クラスで保存する
    public void SetCameraSensitivity(float sensitivity)
    {
        // カメラ感度を保存する
         Camerasensitivity =  sensitivity;
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
        ASbgm.volume = volume;
    }
    public void SetSEVolume(float volume)
    {
        ASSE.volume = volume;
    }

    // =============================
    // サウンドの再生処理関数
    // =============================
    // BGMの再生管理
    public void PlayBGM(AudioClip BGMclip)
    {
        // Playだと引数指定できないのでPlayOneShotを使用
        ASbgm.PlayOneShot(BGMclip);
    }
    // SEの再生管理
    public void PlaySE(AudioClip SEclip)
    {
        ASSE.PlayOneShot(SEclip);
    }

}
