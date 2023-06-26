//=============================================================================
//
// パッドスライダー クラス [PadSlider.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PadSlider : MonoBehaviour
{
    int nowOption = -1;     // ０：カメラ　１：サウンド
    float add = 0.001f;     // スライダーの増減量
    Slider activeSlider;    // 今動かしているスライダー

    [SerializeField] Slider cameraSlider;   
    [SerializeField] Slider bgmSlider;      
    [SerializeField] Slider seSlider;       

    [SerializeField] Button cameraButton;   
    [SerializeField] Button soundButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nowOption != -1)
        {
            // 左スティック取得
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            // 横でスライダー増減
            if (moveX > 0)
                activeSlider.value += add;
            else if (moveX < 0)
                activeSlider.value -= add;

            // サウンドオプションの時
            if (nowOption == 1)
            {
                // 縦でスライダー変更
                if (moveY > 0)
                    activeSlider = bgmSlider;
                else if (moveY < 0)
                    activeSlider = seSlider;
            }
        }
    }

    // カメラスライダーをセット
    public void SetCameraSlider()
    {
        nowOption = 0;
        activeSlider = cameraSlider;
        cameraButton.Select();
    }
    // BGMスライダーをセット
    public void SetSoundSlider()
    {
        nowOption = 1;
        activeSlider = bgmSlider;
        soundButton.Select();
    }
    // スライダーを外す
    public void nullSlider()
    {
        nowOption = -1;
        activeSlider = null;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
