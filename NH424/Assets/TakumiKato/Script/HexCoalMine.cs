//=============================================================================
//
// 鉄材ヘクス クラス [HexCoalMine.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoalMine : Hex
{
    GameObject child;   // 上に置いてあるオブジェクト

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        bMaterialHex = true;        // 素材マスなのでtrue
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);     // このマスが見つかっていない間は上においてあるオブジェクトを消す
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        GetMaterial(UNIT_ACT.COAL_MINE);    // 鉄材獲得

        // このマスが開いており、かつ上のオブジェクトをまだ出現させてない場合
        if (bReverse && !child.activeSelf)
        {
            Tutorial.instance.Iron();   // 鉄材に関するチュートリアルを表示
            child.SetActive(true);      // 上のオブジェクトを表示
        }
    }
}
