using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class charaInfo : MonoBehaviour
{
    public static charaInfo instance = null;

    private List<CharacterInfo> CharaInfo = new List<CharacterInfo>();
    static private string PB_CSVFile = "CSV/PictorialBook"; // CSVデータ保存場所
    private TextAsset csvFile;
    private bool bInit = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            InitCharaInfo();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ==========================
    // キャラクター情報初期化関数
    // ==========================
    private void InitCharaInfo()
    {
        // キャラクターデータなどはCSVファイルから読み込んで
        // 出会った回数は今後セーブ機能実装とともに別ファイル
        // から読み込む予定←csv上書き機能がUnityにないため

        // 一次保存用の変数
        CharacterInfo CItemp = new CharacterInfo();
        List<string[]> CItempList = new List<string[]>();
        int iLines = 0; // 行計算

        // Resources下のCSVを呼び出す
        csvFile = Resources.Load(PB_CSVFile) as TextAsset;

        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込み
            CItempList.Add(line.Split(',')); // ,で区切ってリストに追加
            iLines++; // 行数加算
        }

        for (int i = 0; i < iLines; i++)
        {
            CItemp.Num = Convert.ToInt32(CItempList[i][0]);
            CItemp.Name = CItempList[i][1];
            CItemp.Motif = CItempList[i][2];
            CItemp.Encounter = Convert.ToInt32(CItempList[i][3]);
            CItemp.Sex = CItempList[i][4];
            CItemp.MoveArea = Convert.ToInt32(CItempList[i][5]);
            CItemp.Comment = CItempList[i][6];
            CItemp.Tag1 = CItempList[i][7];
            CItemp.Tag2 = CItempList[i][8];
            CItemp.Tag3 = CItempList[i][9];


            CharaInfo.Add(CItemp);
        }
        // 最後に終了の行を入れる
        CItemp.Num = -1;
        CItemp.Name = "-1";
        CItemp.Motif = "-1";
        CItemp.Encounter = -1;
        CItemp.Sex = "-1";
        CItemp.MoveArea = -1;
        CItemp.Comment = "-1";
        CItemp.Tag1 = "-1";
        CItemp.Tag2 = "-1";
        CItemp.Tag3 = "-1";

        CharaInfo.Add(CItemp);

    }

    public List<CharacterInfo> GetInfo()
    {
        //if (!bInit)
        //{
        //    InitCharaInfo();
        //    bInit = true;
        //}
        return CharaInfo;
    }
}
