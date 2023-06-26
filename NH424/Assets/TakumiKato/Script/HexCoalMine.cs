//=============================================================================
//
// �S�ރw�N�X �N���X [HexCoalMine.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoalMine : Hex
{
    GameObject child;   // ��ɒu���Ă���I�u�W�F�N�g

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        bMaterialHex = true;        // �f�ރ}�X�Ȃ̂�true
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);     // ���̃}�X���������Ă��Ȃ��Ԃ͏�ɂ����Ă���I�u�W�F�N�g������
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        GetMaterial(UNIT_ACT.COAL_MINE);    // �S�ފl��

        // ���̃}�X���J���Ă���A����̃I�u�W�F�N�g���܂��o�������ĂȂ��ꍇ
        if (bReverse && !child.activeSelf)
        {
            Tutorial.instance.Iron();   // �S�ނɊւ���`���[�g���A����\��
            child.SetActive(true);      // ��̃I�u�W�F�N�g��\��
        }
    }
}
