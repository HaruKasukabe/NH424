//=============================================================================
//
// リセットヘクス クラス [HexReset.cpp]
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

        // 上に置かれたユニットが仲間の場合
        if(bUnit && Unit.bFriend)
        {
            DestroyObjects(Map.instance.map);   // 今あるマップをすべて削除
            Map.instance.ResetMap();            // マップを再生成
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
