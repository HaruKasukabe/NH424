//=============================================================================
//
// 未仲間の吹き出し クラス [Fukidasi.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fukidasi : MonoBehaviour
{
    Unit unit;
    GameObject obj;         // 吹き出しのゲームオブジェクト
    bool bDelete = false;   // 吹き出しを消したか

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponentInParent<Unit>();
        obj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // ユニットが仲間になり、かつまだ吹き出しを消していない場合
        if (unit.bFriend && !bDelete)
        {
            obj.SetActive(false);
            bDelete = true;
        }
    }
}
