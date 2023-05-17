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
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    // �ϐ��錾
    public OptionSC option; // �I�v�V�����̃X�N���v�g�Q�Ɨp
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    // �X���C�_�[�̒l�����̂܂�BGM�ESE�̉��ʂɑ��
    public void MoveSliderBGM()
    {
        option.SetBGMVolume(slider.value);
    }
    public void MoveSliderSE()
    {
        option.SetSEVolume(slider.value);
    }
    // �X���C�_�[�̒l���I�v�V�����N���X�ɓn��
    public void MoveSliderCamera()
    {
        option.SetCameraSensitivity(slider.value);
    }
}
