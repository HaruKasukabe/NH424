// ================================================
//  Choice.cs[メインメニューの選択管理]
// 
// Author:徳田亮
//=================================================
// 変更履歴
// 2023/05/08 スクリプト作成
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Canvas/MainMenu/Button").GetComponent<Button>();
        //ボタンが選択された状態になる
        button.Select();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
