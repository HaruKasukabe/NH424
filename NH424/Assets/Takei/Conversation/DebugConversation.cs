using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DebugConversation : MonoBehaviour
{
    // 構造体
    // 変数宣言
    public ConverSation conversation; // 会話管理スクリプト呼び出し
    bool flg;
    string FileName = "CSV/DebugText";

    // Start is called before the first frame update
    void Start()
    {
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ファイル読み込み関数呼び出し
        if (Input.GetKey(KeyCode.K) && !flg)
        {
            flg = conversation.ConverSationInit(FileName);
        }
        if(flg)
            flg = conversation.ActiveGameIvent();
        Debug.Log(flg);
    }

    
}
