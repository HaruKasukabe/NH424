//=============================================================================
//
// �H�ރw�N�X �N���X [HexGarden.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGarden : Hex
{
    GameObject child;   // ��ɒu���Ă���I�u�W�F�N�g

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        bMaterialHex = true;        // �f�ރ}�X�Ȃ̂�true
        bGarden = true;             // �H�ރ}�X�Ȃ̂�true
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);     // ���̃}�X���������Ă��Ȃ��Ԃ͏�ɂ����Ă���I�u�W�F�N�g������
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        GetMaterial(UNIT_ACT.GARDEN);   // �H�ނ��l��

        // ���̃}�X���J���Ă���A����̃I�u�W�F�N�g���܂��o�������ĂȂ��ꍇ
        if (bReverse && !child.activeSelf)
        {
            Tutorial.instance.Food();   // �H�ނɊւ���`���[�g���A����\��
            child.SetActive(true);      // ��̃I�u�W�F�N�g��\��
        }
    }
}
