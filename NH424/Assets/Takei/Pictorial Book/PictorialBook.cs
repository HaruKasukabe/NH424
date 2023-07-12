// ================================================
//  PictorialBook.cs[図鑑管理]
// 
// Author:武井遥都
//=================================================
// 変更履歴
// 2023/03/04 図鑑管理ソース作成
// 2023/04/09 図鑑説明(左側完成)
// 2023/04/10 
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

// 構造体宣言
public struct CharacterInfo
{
    public int Num;        // キャラクター番号
    public string Name;    // キャラクターネーム
    public string Motif;   // キャラクターモチーフ
    public int Encounter;  // 出会った回数
    public string Sex;     // 性別
    public int MoveArea;   // 移動範囲
    public string Comment; // 一言コメント
    public string Tag1;    // タグ1
    public string Tag2;    // タグ2
    public string Tag3;    // タグ3
};

public class PictorialBook : MonoBehaviour
{
    // 変数宣言
    public GameObject PictorialBookobj; // キャラクター図鑑グループオブジェクト
    private bool OpenFlg; // キャラクター図鑑を開いているか
    static private string PB_CSVFile = "CSV/PictorialBook"; // CSVデータ保存場所
    private TextAsset csvFile;
    private int pbNum; // 今見てる図鑑の位置

    public TextMeshProUGUI TB_CharacterName; // キャラクターネーム
    public TextMeshProUGUI TB_Motif; // モチーフ
    public TextMeshProUGUI TB_Sex; // 性別
    public TextMeshProUGUI TB_Encounter; // 遭遇回数
    public TextMeshProUGUI TB_MoveArea; // 移動範囲
    public TextMeshProUGUI TB_Comment; // 一言コメント
    public TextMeshProUGUI TB_Tags; // タグ

    public Image LeftPB;

    private Sprite s_Crow;         // カラスの画像
    private Sprite s_Rabbit;       // ウサギの画像
    private Sprite s_Wolf;         // オオカミの画像
    private Sprite s_Giraffe;      // キリンの画像
    private Sprite s_Cat;          // ネコの画像
    private Sprite s_Mouse;        // ネズミの画像
    private Sprite s_Squirrel;     // リスの画像
    private Sprite s_Bat;          // コウモリの画像
    private Sprite s_Crocodile;    // ワニの画像
    private Sprite s_Elephant;     // ゾウの画像
    private Sprite s_Frog;         // カエルの画像
    private Sprite s_Headgehog;    // ハリネズミの画像
    private Sprite s_Horse;        // ウマの画像
    private Sprite s_Lion;         // ライオンの画像
    private Sprite s_Mole;         // モグラの画像
    private Sprite s_Panda;        // パンダの画像
    private Sprite s_Swan;         // ハクチョウの画像
    private Sprite s_Turtle;       // カメの画像
    private Sprite s_Tiger;        // トラの画像
    private Sprite s_Sheep;        // ヒツジの画像
    private Sprite s_Undiscavered; // 未発見の画像

    public Image i_Crow;
    public Image i_Rabbit;
    public Image i_Wolf;
    public Image i_Giraffe;
    public Image i_Cat;
    public Image i_Mouse;
    public Image i_Squirrel;
    public Image i_Bat;
    public Image i_Crocodile;
    public Image i_Elephant;
    public Image i_Frog;
    public Image i_Headgehog;
    public Image i_Horse;
    public Image i_Lion;
    public Image i_Mole;
    public Image i_Panda;
    public Image i_Swan;
    public Image i_Turtle;
    public Image i_Tiger;
    public Image i_Sheep;

    bool bStartScene = false;
    public Choice choice;

    // Initilize
    void Start()
    {
        OpenFlg = false; // 初期値で図鑑は閉じている
        PictorialBookobj.SetActive(false); // 図鑑オブジェクトは非表示
        //InitcharaInfo.instance.GetInfo()(); // キャラ情報初期化処理
        pbNum = 0; // 初期値は左上
        InitCharaTexture(); // テクスチャ初期化処理
        if (SceneManager.GetActiveScene().name == "StartScene")
            bStartScene = true;
    }

    // 更新関数
    void Update()
    {
        // Zキーを押すと図鑑が開く
        //if(Input.GetKeyDown(KeyCode.Z)&& !OpenFlg)
        //{
        //    OpenFlg = !OpenFlg; // フラグを反転
        //    PictorialBookobj.SetActive(OpenFlg); // 図鑑オブジェクト表示
        //    pbNum = 0;
        //    CheckDiscoverChara();
        //    CheckLeftPB();
        //    DisplayTextBox();
        //}
        if (Input.GetButtonDown("Fire2") && OpenFlg)
        {
            BacktoMapButton();
        }
    }


    // ==========================
    // キャラ発見時呼び出し関数
    // ==========================
    public void DiscoverCharacter(int Num)
    {
        CharacterInfo ciTemp = new CharacterInfo();
        ciTemp = charaInfo.instance.GetInfo()[Num];
        ciTemp.Encounter++;
        charaInfo.instance.GetInfo()[Num] = ciTemp;
    }

    // ==========================
    // テキストボックス更新関数
    // ==========================
    private void DisplayTextBox()
    {
        // 出会っていたら情報表示
        if (charaInfo.instance.GetInfo()[pbNum].Encounter != 0)
        {
            // キャラクターネーム更新
            TB_CharacterName.text = charaInfo.instance.GetInfo()[pbNum].Name;
            // モチーフ更新
            TB_Motif.text = charaInfo.instance.GetInfo()[pbNum].Motif;
            // 出会った回数
            TB_Encounter.text = charaInfo.instance.GetInfo()[pbNum].Encounter.ToString() + "回";
            // 性別更新
            TB_Sex.text = charaInfo.instance.GetInfo()[pbNum].Sex;
            // 移動範囲更新
            TB_MoveArea.text = charaInfo.instance.GetInfo()[pbNum].MoveArea.ToString() + "マス";
            // 一言コメント更新
            TB_Comment.text = charaInfo.instance.GetInfo()[pbNum].Comment;
            // タグ更新
            TB_Tags.text = charaInfo.instance.GetInfo()[pbNum].Tag1;
            if (charaInfo.instance.GetInfo()[pbNum].Tag2 != "Null")
                TB_Tags.text = TB_Tags + " " + charaInfo.instance.GetInfo()[pbNum].Tag2;
            if (charaInfo.instance.GetInfo()[pbNum].Tag3 != "Null")
                TB_Tags.text = TB_Tags + " " + charaInfo.instance.GetInfo()[pbNum].Tag3;
        }
        else
        {
            // キャラクターネーム更新
            TB_CharacterName.text = "???";
            // モチーフ更新
            TB_Motif.text = "???";
            // 出会った回数
            TB_Encounter.text = "未発見";
            // 性別更新
            TB_Sex.text = "???";
            // 移動範囲更新
            TB_MoveArea.text = "???";
            // 一言コメント更新
            TB_Comment.text = "???";
            // タグ更新
            TB_Tags.text = "???"; 
        }
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


            charaInfo.instance.GetInfo().Add(CItemp);
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

        charaInfo.instance.GetInfo().Add(CItemp);

    }

    // ==========================
    // キャラテクスチャ初期化関数
    // ==========================
    private void InitCharaTexture()
    {
        // テクスチャをリソースから読み込み
        s_Crow = Resources.Load<Sprite>("Image/Crow");
        s_Rabbit = Resources.Load<Sprite>("Image/Rabbit");
        s_Wolf = Resources.Load<Sprite>("Image/Wolf");
        s_Giraffe = Resources.Load<Sprite>("Image/giraffe");
        s_Cat = Resources.Load<Sprite>("Image/Cat");
        s_Mouse = Resources.Load<Sprite>("Image/rat");
        s_Squirrel = Resources.Load<Sprite>("Image/squirrel");
        s_Bat = Resources.Load<Sprite>("Image/bat");
        s_Crocodile = Resources.Load<Sprite>("Image/crocodile");
        s_Elephant = Resources.Load<Sprite>("Image/elephant");
        s_Frog = Resources.Load<Sprite>("Image/frog");
        s_Headgehog = Resources.Load<Sprite>("Image/hedgehog");
        s_Horse = Resources.Load<Sprite>("Image/horse");
        s_Lion = Resources.Load<Sprite>("Image/lion");
        s_Mole = Resources.Load<Sprite>("Image/mole");
        s_Panda = Resources.Load<Sprite>("Image/panda");
        s_Swan = Resources.Load<Sprite>("Image/swan");
        s_Turtle = Resources.Load<Sprite>("Image/turtle");
        s_Tiger = Resources.Load<Sprite>("Image/tiger");
        s_Sheep = Resources.Load<Sprite>("Image/sheep");
        s_Undiscavered = Resources.Load<Sprite>("Image/Undiscovered");

        // 読み込んだリソースをImageに渡す
        i_Crow.sprite = s_Undiscavered;
        i_Rabbit.sprite = s_Undiscavered;
        i_Wolf.sprite = s_Undiscavered;
        i_Giraffe.sprite = s_Undiscavered;
        i_Cat.sprite = s_Undiscavered;
        i_Mouse.sprite = s_Undiscavered;
        i_Squirrel.sprite = s_Undiscavered;
        i_Bat.sprite = s_Undiscavered;
        i_Crocodile.sprite = s_Undiscavered;
        i_Elephant.sprite = s_Undiscavered;
        i_Frog.sprite = s_Undiscavered;
        i_Headgehog.sprite = s_Undiscavered;
        i_Horse.sprite = s_Undiscavered;
        i_Lion.sprite = s_Undiscavered;
        i_Mole.sprite = s_Undiscavered;
        i_Panda.sprite = s_Undiscavered;
        i_Swan.sprite = s_Undiscavered;
        i_Turtle.sprite = s_Undiscavered;
        i_Tiger.sprite = s_Undiscavered;
        i_Sheep.sprite = s_Undiscavered;
    }

    // ==========================
    // キャラ発見チェック関数
    // ==========================
    private void CheckDiscoverChara()
    {
        if (charaInfo.instance.GetInfo()[0].Encounter != 0)
            i_Cat.sprite = s_Cat;
        if (charaInfo.instance.GetInfo()[1].Encounter != 0)
            i_Crow.sprite = s_Crow;
        if (charaInfo.instance.GetInfo()[2].Encounter != 0)
            i_Giraffe.sprite = s_Giraffe;
        if (charaInfo.instance.GetInfo()[3].Encounter != 0)
            i_Rabbit.sprite = s_Rabbit;
        if (charaInfo.instance.GetInfo()[4].Encounter != 0)
            i_Mouse.sprite = s_Mouse;
        if (charaInfo.instance.GetInfo()[5].Encounter != 0)
            i_Squirrel.sprite = s_Squirrel;
        if (charaInfo.instance.GetInfo()[6].Encounter != 0)
            i_Wolf.sprite = s_Wolf;
        if (charaInfo.instance.GetInfo()[7].Encounter != 0)
            i_Bat.sprite = s_Bat;
        if (charaInfo.instance.GetInfo()[8].Encounter != 0)
            i_Crocodile.sprite = s_Crocodile;
        if (charaInfo.instance.GetInfo()[9].Encounter != 0)
            i_Elephant.sprite = s_Elephant;
        if (charaInfo.instance.GetInfo()[10].Encounter != 0)
            i_Frog.sprite = s_Frog;
        if (charaInfo.instance.GetInfo()[11].Encounter != 0)
            i_Headgehog.sprite = s_Headgehog;
        if (charaInfo.instance.GetInfo()[12].Encounter != 0)
            i_Horse.sprite = s_Horse;
        if (charaInfo.instance.GetInfo()[13].Encounter != 0)
            i_Lion.sprite = s_Lion;
        if (charaInfo.instance.GetInfo()[14].Encounter != 0)
            i_Mole.sprite = s_Mole;
        if (charaInfo.instance.GetInfo()[15].Encounter != 0)
            i_Sheep.sprite = s_Sheep;
        if (charaInfo.instance.GetInfo()[16].Encounter != 0)
            i_Swan.sprite = s_Swan;
        if (charaInfo.instance.GetInfo()[17].Encounter != 0)
            i_Tiger.sprite = s_Tiger;
        if (charaInfo.instance.GetInfo()[18].Encounter != 0)
            i_Turtle.sprite = s_Turtle;
        if (charaInfo.instance.GetInfo()[19].Encounter != 0)
            i_Panda.sprite = s_Panda;
    }

    // ==========================
    // 図鑑左側チェック関数
    // ==========================
    private void CheckLeftPB()
    {
        if(charaInfo.instance.GetInfo()[pbNum].Encounter !=0)
        {
            int num = 0;
            if (charaInfo.instance.GetInfo()[0].Encounter != 0)
                LeftPB.sprite = s_Cat;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[1].Encounter != 0)
                LeftPB.sprite = s_Crow;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[2].Encounter != 0)
                LeftPB.sprite = s_Giraffe;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[3].Encounter != 0)
                LeftPB.sprite = s_Rabbit;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[4].Encounter != 0)
                LeftPB.sprite = s_Mouse;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[5].Encounter != 0)
                LeftPB.sprite = s_Squirrel;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[6].Encounter != 0)
                LeftPB.sprite = s_Wolf;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[7].Encounter != 0)
                LeftPB.sprite = s_Bat;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[8].Encounter != 0)
                LeftPB.sprite = s_Crocodile;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[9].Encounter != 0)
                LeftPB.sprite = s_Elephant;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[10].Encounter != 0)
                LeftPB.sprite = s_Frog;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[11].Encounter != 0)
                LeftPB.sprite = s_Headgehog;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[12].Encounter != 0)
                LeftPB.sprite = s_Horse;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[13].Encounter != 0)
                LeftPB.sprite = s_Lion;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[14].Encounter != 0)
                LeftPB.sprite = s_Mole;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[15].Encounter != 0)
                LeftPB.sprite = s_Sheep;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[16].Encounter != 0)
                LeftPB.sprite = s_Swan;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[17].Encounter != 0)
                LeftPB.sprite = s_Tiger;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[18].Encounter != 0)
                LeftPB.sprite = s_Turtle;
            if (pbNum == num) return;
            num++;
            if (charaInfo.instance.GetInfo()[19].Encounter != 0)
                LeftPB.sprite = s_Panda;
            if (pbNum == num) return;
            num++;
        }
        else
        {
            LeftPB.sprite = s_Undiscavered;
        }
    }

    // ==========================
    // ボタン処理関数
    // ==========================
    public void SelectLeftCButton()
    {
        if (pbNum > 0)
        {
            pbNum--;
            ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Select);
            Debug.Log("通過");
            CheckLeftPB();
            DisplayTextBox();
        }
    }
    public void SelectRightCButton()
    {
        if (pbNum < 19)
        {
            pbNum++;
            ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Select);
            Debug.Log("通過");
            CheckLeftPB();
            DisplayTextBox();
        }
    }

    // 図鑑を閉じるボタンを押したとき
    public void BacktoMapButton()
    {
        OpenFlg = false;
        PictorialBookobj.SetActive(false);
        if (!bStartScene)
            GameManager.instance.SetUICursol(false);
        else
            choice.select();
    }
    public void SetDisplay()
    {
        OpenFlg = true;
        PictorialBookobj.SetActive(true); // 図鑑オブジェクト表示
        pbNum = 0;
        CheckDiscoverChara();
        CheckLeftPB();
        DisplayTextBox();
        if (!bStartScene)
            GameManager.instance.SetUICursol(true);
        else
            choice.selectPicto();
    }
    public bool GetOpenFlg()
    {
        return OpenFlg;
    }
}
