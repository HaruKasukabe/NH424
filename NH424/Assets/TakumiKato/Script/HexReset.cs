//=============================================================================
//
// ���Z�b�g�w�N�X �N���X [HexReset.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexReset : Hex
{
    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        // ��ɒu���ꂽ���j�b�g�����Ԃ̏ꍇ
        if(bUnit && Unit.bFriend)
        {
            DestroyObjects(Map.instance.map);   // ������}�b�v�����ׂč폜
            Map.instance.ResetMap();            // �}�b�v���Đ���
        }
    }

    private void DestroyObjects(GameObject[,] objects)
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}
