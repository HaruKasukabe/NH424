//=============================================================================
//
// ���w�N�X �N���X [HexVillage.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexVillage : Hex
{


    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();

        // �ŏ�����J���Ă����Ԃ�
        rend.material.color = new Color32(255, 255, 255, 1);
        bReverse = true;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
