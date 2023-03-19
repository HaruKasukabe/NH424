// ================================================
//  OptionSlider.cs[�I�v�V�����̃X���C�_�[����]
// 
// Author:����y�s
//=================================================
// �ύX����
// 2023/02/27 �X�N���v�g�쐬�EBGM��SE�̉��ʒ�����
// ����
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSlider : MonoBehaviour
{
    // �ϐ��錾
    public OptionSC option; // �I�v�V�����̃X�N���v�g�Q�Ɨp

    // �X���C�_�[�̒l�����̂܂�BGM�ESE�̉��ʂɑ��
    public void MoveSliderBGM(float newSliderValue)
    {
        option.SetBGMVolume(newSliderValue);
    }
    public void MoveSliderSE(float newSliderValue)
    {
        option.SetSEVolume(newSliderValue);
    }
    // �X���C�_�[�̒l���I�v�V�����N���X�ɓn��
    public void MoveSliderCamera(float newSliderCamera)
    {
        option.SetCameraSensitivity(newSliderCamera);
    }
}
