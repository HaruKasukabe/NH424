using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public enum TUTORIAL
{
    FOOD,
    WOOD,
    IRON,
    STONE,
    HAIIRO,
    KURO,
    SOZAI,
    MURAGAMEN,
    MURALEVEL,
    KOUTAI,
    SOUSA,
    GAMEN,
    NO_KEMOKO,
    GAME_CLEAR,
    GAME_OVER,

    MAX
}

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance = null;

    public GameObject Main;
    [SerializeField] TextMeshProUGUI MainText;
    [SerializeField] TextMeshProUGUI SubText;
    [SerializeField] Image iTorumae;
    [SerializeField] Image iTottaato;
    [SerializeField] Image iKazuhyouzi;

    private struct CanvasStruct
    {
        public string MainText;
        public string SubText;
        public Sprite Torumae;
        public Sprite Tottaato;
        public Sprite Kazuhyouzi;
    }
    List<CanvasStruct> ListCanvas = new List<CanvasStruct>();
    public List<bool> ListFlg = new List<bool>();

    int num = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Main.SetActive(false);
        Initirize();
        HaiiroMasu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && Main.activeSelf)
        {
            //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
            Main.SetActive(false);
            GameManager.instance.SetUICursol(false);

            if (num < 2)
                StartTutorial();
        }
    }

    private void Initirize()
    {
        // 食料マス
        CanvasStruct temp;
        temp.MainText = "食料マス";
        temp.SubText = "このマスは食料マスです。\n村をケモコを仲間にする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Food_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Food_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Food_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 木材マス
        temp.MainText = "木材マス";
        temp.SubText = "このマスは木材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Wood_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Wood_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Wood_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 鉄材マス
        temp.MainText = "鉄材マス";
        temp.SubText = "このマスは鉄材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Iron_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Iron_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Iron_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 石材マス
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 灰色マス
        temp.MainText = "灰色マス";
        temp.SubText = "灰色マスはキャラクターが移動できる未発見マスです。\nマスに触れることによって、材料マスやケモコマスを見つけることができる。";
        temp.Torumae = Resources.Load<Sprite>("Image/GrayMasu");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 黒マス
        temp.MainText = "黒マス";
        temp.SubText = "黒色マスは、キャラクターが移動できないマスです。\n灰色マスを開拓することによって移動できるようになります。";
        temp.Torumae = Resources.Load<Sprite>("Image/BlackMasu");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);     

        // 村レベルUP
        temp.MainText = "村レベルUPについて";
        temp.SubText = "村アイコンをタップすることで村情報を確認することができる。\n材料を消費して村レベルを上げることでより探索範囲を広くすることができる。";
        temp.Torumae = Resources.Load<Sprite>("Image/VillageUp");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 盤面上未仲間ケモコ
        temp.MainText = "盤面上未仲間ケモコについて";
        temp.SubText = "盤上のケモコの上に立つことにより、\n食料を消費してケモコを仲間にすることができる。";
        temp.Torumae = Resources.Load<Sprite>("Image/Alternation");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // ケモコ交代
        temp.MainText = "ケモコ交代について";
        temp.SubText = "ケモコの所持数が5体以上になると、村に行ってケモコを交代することが可能になる。\n外にいるケモコと村にいるケモコのアイコンをタップすることで交代することができる。";
        temp.Torumae = Resources.Load<Sprite>("Image/NakamaMae");
        temp.Tottaato = Resources.Load<Sprite>("Image/NakamaTotyuu");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        /*
        // ゲームクリア説明
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // ゲームオーバー説明
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);
        */

    }

    // 食料マス
    public void Food()
    {
        if (ListFlg[0] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[0].MainText;
            SubText.text = ListCanvas[0].SubText;
            iTorumae.sprite = ListCanvas[0].Torumae;
            iTottaato.sprite = ListCanvas[0].Tottaato;
            iTorumae.sprite = ListCanvas[0].Kazuhyouzi;
            ListFlg[0] = true;
        }
    }

    // 木材マス
    public void Wood()
    {
        if (ListFlg[1] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[1].MainText;
            SubText.text = ListCanvas[1].SubText;
            iTorumae.sprite = ListCanvas[1].Torumae;
            iTottaato.sprite = ListCanvas[1].Tottaato;
            iTorumae.sprite = ListCanvas[1].Kazuhyouzi;
            ListFlg[1] = true;
        }
    }

    // 鉄材マス
    public void Iron()
    {
        if (ListFlg[2] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[2].MainText;
            SubText.text = ListCanvas[2].SubText;
            iTorumae.sprite = ListCanvas[2].Torumae;
            iTottaato.sprite = ListCanvas[2].Tottaato;
            iTorumae.sprite = ListCanvas[2].Kazuhyouzi;
            ListFlg[2] = true;
        }
    }

    // 石材マス
    public void Stone()
    {
        if (ListFlg[3] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[3].MainText;
            SubText.text = ListCanvas[3].SubText;
            iTorumae.sprite = ListCanvas[3].Torumae;
            iTottaato.sprite = ListCanvas[3].Tottaato;
            iTorumae.sprite = ListCanvas[3].Kazuhyouzi;
            ListFlg[3] = true;
        }
    }

    // 灰色マス
    public void HaiiroMasu()
    {
        if (ListFlg[4] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[4].MainText;
            SubText.text = ListCanvas[4].SubText;
            iTorumae.sprite = ListCanvas[4].Torumae;
            iTottaato.sprite = ListCanvas[4].Tottaato;
            iTorumae.sprite = ListCanvas[4].Kazuhyouzi;
            ListFlg[4] = true;
        }
    }

    // 黒マス
    public void KuroMasu()
    {
        if (ListFlg[5] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[5].MainText;
            SubText.text = ListCanvas[5].SubText;
            iTorumae.sprite = ListCanvas[5].Torumae;
            iTottaato.sprite = ListCanvas[5].Tottaato;
            iTorumae.sprite = ListCanvas[5].Kazuhyouzi;
            ListFlg[5] = true;
        }
    }

    // 村レベルUP
    public void MuraLevel()
    {
        if (ListFlg[6] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[6].MainText;
            SubText.text = ListCanvas[6].SubText;
            iTorumae.sprite = ListCanvas[6].Torumae;
            iTottaato.sprite = ListCanvas[6].Tottaato;
            iTorumae.sprite = ListCanvas[6].Kazuhyouzi;
            ListFlg[6] = true;
        }
    }

    // 盤上未仲間ケモコ
    public void No_Kemoko()
    {

        if (ListFlg[7] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[7].MainText;
            SubText.text = ListCanvas[7].SubText;
            iTorumae.sprite = ListCanvas[7].Torumae;
            iTottaato.sprite = ListCanvas[7].Tottaato;
            iTorumae.sprite = ListCanvas[7].Kazuhyouzi;
            ListFlg[7] = true;
        }
    }

    // ケモコ交代
    public void Kemokokoutai()
    {
        if (ListFlg[8] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[8].MainText;
            SubText.text = ListCanvas[8].SubText;
            iTorumae.sprite = ListCanvas[8].Torumae;
            iTottaato.sprite = ListCanvas[8].Tottaato;
            iTorumae.sprite = ListCanvas[8].Kazuhyouzi;
            ListFlg[8] = true;
        }
    }

    /*
    // ゲームクリア説明
    public void Game_Clear()
    {
        if (ListFlg[9] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[9].MainText;
            SubText.text = ListCanvas[9].SubText;
            iTorumae.sprite = ListCanvas[9].Torumae;
            iTottaato.sprite = ListCanvas[9].Tottaato;
            iTorumae.sprite = ListCanvas[9].Torumae;
            ListFlg[9] = true;
        }
    }

    // ゲームオーバー説明
    public void Game_Over()
    {
        if (ListFlg[10] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[10].MainText;
            SubText.text = ListCanvas[10].SubText;
            iTorumae.sprite = ListCanvas[10].Torumae;
            iTottaato.sprite = ListCanvas[10].Tottaato;
            iTorumae.sprite = ListCanvas[10].Torumae;
            ListFlg[10] = true;
        }
    }
    */

    // ボタン処理
    public void BacktoGameButton()
    {
        //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
        Main.SetActive(false);
        GameManager.instance.SetUICursol(false);
    }
    void StartTutorial()
    {
        switch(num)
        {
            case 0:
                KuroMasu();
                break;
            case 1:
                MuraLevel();
                break;
            default:
                break;
        }
        num++;
    }
}
