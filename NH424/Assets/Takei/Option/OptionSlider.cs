// ================================================
//  OptionSlider.cs[オプションのスライダー調整]
// 
// Author:武井遥都
//=================================================
// 変更履歴
// 2023/02/27 スクリプト作成・BGMとSEの音量調整を
// 実装
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    // 変数宣言
    public OptionSC option; // オプションのスクリプト参照用
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    // スライダーの値をそのままBGM・SEの音量に代入
    public void MoveSliderBGM()
    {
        option.SetBGMVolume(slider.value);
    }
    public void MoveSliderSE()
    {
        option.SetSEVolume(slider.value);
    }
    // スライダーの値をオプションクラスに渡す
    public void MoveSliderCamera()
    {
        option.SetCameraSensitivity(slider.value);
    }
}
