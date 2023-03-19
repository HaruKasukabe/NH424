// ======================================================
//  TextManagementSeason.cs[�G�߂̃^�C�}�[�\��]
// 
// Author:����y�s
// ======================================================
// �ύX����
// 2023/02/27 �X�N���v�g�쐬�E�e�L�X�g�\������
// ======================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManagementSeason : MonoBehaviour
{
    // �ϐ��錾
    public TextMeshProUGUI DisplaySeason;
    public ManagementTime managementtime;
    private int Minute;
    private int second;

    // Update is called once per frame
    void Update()
    {
        Minute = (int)managementtime.GetSeasonTime() / 60;
        second = (int)managementtime.GetSeasonTime();
        DisplaySeason.text = managementtime.GetNowLap().ToString() + "�T��" + managementtime.GetNowSeason() + "\n" +
           second.ToString("D3");
            //Minute.ToString("D2") + ":" + second.ToString("D2");
    }
}
