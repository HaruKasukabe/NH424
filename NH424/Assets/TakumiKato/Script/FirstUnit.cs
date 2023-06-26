//=============================================================================
//
// �Q�[���ŏ��̃��j�b�g �N���X [FirstUnit.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUnit : Unit
{
    bool bFirstAct = false; // �ŏ��̓������������ǂ���

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        bFriend = true;                     // �ŏ��̃��j�b�g�Ȃ̂ł��łɗF�B�ɐݒ�
        bVillage = true;                    // �^�񒆂̑��������ʒu�Ȃ̂�true��ݒ�
        id = 0;                             // �ŏ��̃��j�b�g��id0��ݒ�
        GameManager.instance.AddUnit(this); // ���j�b�g���X�g�ɒǉ�
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (!bFirstAct)
        {
            GameObject g = Map.instance.GetCenter();    // �}�b�v�̐^�񒆂̃}�X�̍��W���擾
            OriginPos = new Vector3(g.transform.position.x, g.transform.position.y + height, g.transform.position.z);   // �}�X���班�����������������W
            transform.position = OriginPos;             // ���j�b�g��^�񒆂̈ʒu�ɐݒ�
            OldHex = Hex = g.GetComponent<Hex>();       // �^�񒆂̃}�X�������j�b�g�Ɏ�������
            Hex.SetUnit(this);                          // �^�񒆂̃}�X�Ƀ��j�b�g������������
            bFirstAct = true;
        }
    }
}
