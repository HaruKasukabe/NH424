//=============================================================================
//
// �����Ԃ̐����o�� �N���X [Fukidasi.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fukidasi : MonoBehaviour
{
    Unit unit;
    GameObject obj;         // �����o���̃Q�[���I�u�W�F�N�g
    bool bDelete = false;   // �����o������������

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponentInParent<Unit>();
        obj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // ���j�b�g�����ԂɂȂ�A���܂������o���������Ă��Ȃ��ꍇ
        if (unit.bFriend && !bDelete)
        {
            obj.SetActive(false);
            bDelete = true;
        }
    }
}
