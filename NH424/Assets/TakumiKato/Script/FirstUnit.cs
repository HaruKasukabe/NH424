//=============================================================================
//
// ゲーム最初のユニット クラス [FirstUnit.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUnit : Unit
{
    bool bFirstAct = false; // 最初の動きをしたかどうか

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        bFriend = true;                     // 最初のユニットなのですでに友達に設定
        bVillage = true;                    // 真ん中の村が初期位置なのでtrueを設定
        id = 0;                             // 最初のユニットにid0を設定
        GameManager.instance.AddUnit(this); // ユニットリストに追加
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (!bFirstAct)
        {
            GameObject g = Map.instance.GetCenter();    // マップの真ん中のマスの座標を取得
            OriginPos = new Vector3(g.transform.position.x, g.transform.position.y + height, g.transform.position.z);   // マスから少し高さを加えた座標
            transform.position = OriginPos;             // ユニットを真ん中の位置に設定
            OldHex = Hex = g.GetComponent<Hex>();       // 真ん中のマス情報をユニットに持たせる
            Hex.SetUnit(this);                          // 真ん中のマスにユニット情報を持たせる
            bFirstAct = true;
        }
    }
}
