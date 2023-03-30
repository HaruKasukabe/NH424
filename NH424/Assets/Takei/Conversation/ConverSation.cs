// ================================================
//  OptionSC.cs[メインのオプション管理]
// 
// Author:武井遥都
//=================================================
// 変更履歴
// 2023/02/27 スクリプト作成
// 2023/03/06 イベント作成に合わせて改修中
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class ConverSation : MonoBehaviour
{
    // 構造体宣言
    private struct SConverSation // 会話CSV用
    {
        public string TConverSation; // メインの会話内容
        public string TCharaName;    // キャラクターネーム
        public int LRNum;            // 左か右か
        public int Select;          // 選択肢
    }

    // ---変数宣言---
    // 会話テキストボックスオブジェクト
    public GameObject CSMainObj;   // メイン
    public GameObject CSLeftObj;   // 左
    public GameObject CSRightObj;  // 右
    // 会話選択ボタンオブジェクト
    public GameObject CSYButtonObj; // Yes
    public GameObject CSNButtonObj; // No
    // 会話テキストボックス
    public TextMeshProUGUI CSMain;       // メイン
    public TextMeshProUGUI CSCharaLeft;  // 左
    public TextMeshProUGUI CSCharaRight; // 右
    // 選択肢ボックス
    public GameObject SelectBox;
    public GameObject SelectBoxY;
    public GameObject SelectBoxN;
    private int SelectNum;
    private bool EventFlg;

    // CSVファイル関係
    TextAsset csvFile; // CSVファイル指定
    private int CSNum; // CSVファイルの行計算
    List<SConverSation> csvDatas = new List<SConverSation>(); // CSVの中身を入れるリスト

    // *****************
    // Initialize
    // *****************
    private void Start()
    {
        // 全会話オブジェクトを無効化
        CSMainObj.SetActive(false);
        CSLeftObj.SetActive(false);
        CSRightObj.SetActive(false);
        CSYButtonObj.SetActive(false);
        CSNButtonObj.SetActive(false);
        SelectBoxY.SetActive(false);
        SelectBoxN.SetActive(false);
        SelectBox.SetActive(false);
        SelectNum = -1;
        EventFlg = false;
        CSNum = 0;
    }

    //  **************************
    // 外部から呼び出す会話進行用
    //  **************************
    public bool ActiveGameIvent()
    {
        // Enterキーを押したら次のテキストへ
        if (Input.GetKeyDown(KeyCode.Return) && !EventFlg)
        {
            // ---リストを次の行へ---
            CSNum++;
            Debug.Log("会話内容通過");
        }
        // ---イベントが発生したらているか判定---
        if (csvDatas[CSNum].Select == 1)
        {
            SelectBox.SetActive(true);
            SelectBoxY.SetActive(true);
            SelectBoxN.SetActive(true);
            EventFlg = true;
        }
        if (SelectNum == 1)
        {
            CSNum++;
            SelectNum = -1;
            EventFlg = false;
        }
        else if (SelectNum == 0)
        {
            CSNum += 2;
            SelectNum = -1;
            EventFlg = false;
        }

        Debug.Log("現在会話内容" + csvDatas[CSNum].TConverSation+csvDatas[CSNum].Select);
        // ---テキスト描画---
        if (csvDatas[CSNum].TConverSation != "-1")
        {
            DisplayText(csvDatas[CSNum].TConverSation, csvDatas[CSNum].TCharaName, csvDatas[CSNum].LRNum);
        }
        // 左のキャラか右のキャラか
        if (csvDatas[CSNum].LRNum == 0)// 左
        {
            CSLeftObj.SetActive(true);
            CSRightObj.SetActive(false);
        }
        else if (csvDatas[CSNum].LRNum == 1) // 右
        {
            CSRightObj.SetActive(true);
            CSLeftObj.SetActive(false);

        }
        // ---最後の行になったら終わったと返す---
        if (csvDatas[CSNum].TConverSation == "-1")
        {
            // ---全オブジェクトを無効化---
            CSMainObj.SetActive(false);
            CSLeftObj.SetActive(false);
            CSRightObj.SetActive(false);
            CSYButtonObj.SetActive(false);
            CSNButtonObj.SetActive(false);
            SelectBoxY.SetActive(false);
            SelectBoxN.SetActive(false);
            SelectBox.SetActive(false);
            SelectNum = -1;
            EventFlg = false;
            // CSVファイルを読み込んだリストは初期化
            List<SConverSation> csvDatas = new List<SConverSation>();
            // 終了したとfalseを返す
            return false;
        }
        return true;
    }
        //  ***************
        // テキスト描画関数
        //  ***************
        // イベント発生時に呼び出す用 引数(会話内容,発言者の名前,発言者が左か右か0:左1:右)
        public void DisplayText(string Text, string charaName, int Num)
    {
        // メインのテキストボックスにそのまま代入
        CSMain.text = Text;
        if (Num == 0) // 0なら左にキャラネームを
            CSCharaLeft.text = charaName;
        else if (Num == 1) // 1なら右にキャラネームを
            CSCharaRight.text = charaName;
        else
            Debug.Log("キャラネーム描画でエラーが発生しました");
    }

    // ********************
    // CSVファイル読み込み
    // ********************
    private void LoadSCVFile(string CSVFileName)
    {
        // 一次保存用の変数
        SConverSation SCStemp = new SConverSation();
        List<string[]> SCStempList = new List<string[]>();
        int iLines = 0; // 行計算

        // Resources下のCSVを呼び出す
        csvFile = Resources.Load(CSVFileName) as TextAsset;

        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込み
            SCStempList.Add(line.Split(',')); // ,で区切ってリストに追加
            iLines++; // 行数加算
        }

        for(int i=0;i<iLines;i++)
        {
            SCStemp.TConverSation = SCStempList[i][0];
            SCStemp.TCharaName = SCStempList[i][1];
            SCStemp.LRNum = Convert.ToInt32(SCStempList[i][2]);
            SCStemp.Select = Convert.ToInt32(SCStempList[i][3]);

            csvDatas.Add(SCStemp);
        }
        // 最後に終了の行を入れる
        SCStemp.TConverSation = "-1";
        SCStemp.TCharaName = "-1";
        SCStemp.LRNum = -1;
        SCStemp.Select = -1;

        csvDatas.Add(SCStemp);
    }

    // 会話発生Initilize
    public bool ConverSationInit(string CSVFileName)
    {
        // CSVファイル読み込み
        LoadSCVFile(CSVFileName);
        // ---必要全オブジェクトを有効化---
        CSMainObj.SetActive(true); // 会話メインオブジェクト有効化
        CSNum = 0; // CSVファイルの会話計算用の整数は1にする(1行目は説明用)

        // ---最初の会話を表示---
        DisplayText(csvDatas[CSNum].TConverSation, csvDatas[CSNum].TCharaName, csvDatas[CSNum].LRNum);
        
        // --- キャラクターは右か左か---
        if (csvDatas[CSNum].LRNum == 0) // 左
        {
            // 左を有効化し右を無効化
            CSLeftObj.SetActive(true);
            CSRightObj.SetActive(false);
        }
        else if (csvDatas[CSNum].LRNum == 1) // 右
        {
            // 左を無効化し右を有効化
            CSLeftObj.SetActive(false);
            CSRightObj.SetActive(true);
        }

        // フラグ管理の為trueを返す
        return true;
    }

    // イベント発生ボタン管理
    public void EventYesButton()
    {
        SelectNum = 1;
        EventFlg = false;
    }
    public void EventNoButton()
    {
        SelectNum = 0;
        EventFlg = false;
    }
}
