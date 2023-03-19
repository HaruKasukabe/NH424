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

public class OptionSlider : MonoBehaviour
{
    // 変数宣言
    public OptionSC option; // オプションのスクリプト参照用

    // スライダーの値をそのままBGM・SEの音量に代入
    public void MoveSliderBGM(float newSliderValue)
    {
        option.SetBGMVolume(newSliderValue);
    }
    public void MoveSliderSE(float newSliderValue)
    {
        option.SetSEVolume(newSliderValue);
    }
    // スライダーの値をオプションクラスに渡す
    public void MoveSliderCamera(float newSliderCamera)
    {
        option.SetCameraSensitivity(newSliderCamera);
    }
}
