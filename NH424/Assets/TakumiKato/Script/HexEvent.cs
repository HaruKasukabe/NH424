//=============================================================================
//
// イベントヘクス クラス [HexEvent.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEvent : Hex
{
    GameObject child;   // 上に置いてあるオブジェクト

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        bMaterialHex = true;        // 素材マスなのでtrue
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);     // このマスが見つかっていない間は上においてあるオブジェクトを消す
        pickTime = 1;               // 素材獲得可能数１回
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        GetMaterial(UNIT_ACT.ALL);      // 全ての素材を獲得

        // このマスが開いているなら
        if (bReverse)
            child.SetActive(true);      // 上のオブジェクトを表示
    }
}
