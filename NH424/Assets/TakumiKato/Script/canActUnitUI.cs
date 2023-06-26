//=============================================================================
//
// 1�^�[�����̃��j�b�g�ړ���UI �N���X [CanActUnitUI.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanActUnitUI : MonoBehaviour
{
    [SerializeField] Image[] colors;    // UI5���̔z��
    int actUnitNum;                     // �����郆�j�b�g�̐�

    // Start is called before the first frame update
    void Start()
    {
        actUnitNum = GameManager.instance.canActUnitNum - 1;    // �Y�����̊֌W��-1����
    }

    // Update is called once per frame
    void Update()
    {
        // �i�[���������郆�j�b�g�̐��ƍ������郆�j�b�g�̐����Ⴄ�ꍇ
        if (actUnitNum != GameManager.instance.canActUnitNum - 1)
        {
            actUnitNum = GameManager.instance.canActUnitNum - 1;
            for (int i = 0; i < 5; i++)
            {
                // ���j�b�g�̐��ȉ��̓Y�������ǂ���
                if (actUnitNum >= i)
                    colors[i].color = new Color(1,1,1,1);
                else
                    colors[i].color = new Color(1,1,1, 0.25f);
            }
        }
    }
}
