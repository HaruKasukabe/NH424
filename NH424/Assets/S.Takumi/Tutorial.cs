using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject Main;
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
    List<bool> ListFlg = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        Main.SetActive(false);
        Initirize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Stone();
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
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 黒マス
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 素材マス
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 村画面説明
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 村レベルUP
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // ケモコ交代
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 操作
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

         // 画面説明
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // 盤面上未仲間ケモコ
        temp.MainText = "石材マス";
        temp.SubText = "このマスは石材マスです。\n村をレベルアップする際に使用する材料です。";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

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

    }

    // 食料
    public void Food()
    {
        if (ListFlg[0] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[0].MainText;
            SubText.text = ListCanvas[0].SubText;
            iTorumae.sprite = ListCanvas[0].Torumae;
            iTottaato.sprite = ListCanvas[0].Tottaato;
            iTorumae.sprite = ListCanvas[0].Torumae;
            ListFlg[0] = true;
        }
    }

    // 木材
    public void Wood()
    {
        if (ListFlg[1] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[1].MainText;
            SubText.text = ListCanvas[1].SubText;
            iTorumae.sprite = ListCanvas[1].Torumae;
            iTottaato.sprite = ListCanvas[1].Tottaato;
            iTorumae.sprite = ListCanvas[1].Torumae;
            ListFlg[1] = true;
        }
    }

    // 鉄材
    public void Iron()
    {
        if (ListFlg[2] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[2].MainText;
            SubText.text = ListCanvas[2].SubText;
            iTorumae.sprite = ListCanvas[2].Torumae;
            iTottaato.sprite = ListCanvas[2].Tottaato;
            iTorumae.sprite = ListCanvas[2].Torumae;
            ListFlg[0] = true;
        }
    }

    // 石材
    public void Stone()
    {
        if (ListFlg[3] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[3].MainText;
            SubText.text = ListCanvas[3].SubText;
            iTorumae.sprite = ListCanvas[3].Torumae;
            iTottaato.sprite = ListCanvas[3].Tottaato;
            iTorumae.sprite = ListCanvas[3].Torumae;
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
            iTorumae.sprite = ListCanvas[4].Torumae;
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
            iTorumae.sprite = ListCanvas[5].Torumae;
            ListFlg[5] = true;
        }
    }

    // 素材マス
    public void SozaiMasu()
    {
        if (ListFlg[6] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[6].MainText;
            SubText.text = ListCanvas[6].SubText;
            iTorumae.sprite = ListCanvas[6].Torumae;
            iTottaato.sprite = ListCanvas[6].Tottaato;
            iTorumae.sprite = ListCanvas[6].Torumae;
            ListFlg[6] = true;
        }
    }

    // 村画面説明
    public void MuraGamenn()
    {
        if (ListFlg[7] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[7].MainText;
            SubText.text = ListCanvas[7].SubText;
            iTorumae.sprite = ListCanvas[7].Torumae;
            iTottaato.sprite = ListCanvas[7].Tottaato;
            iTorumae.sprite = ListCanvas[7].Torumae;
            ListFlg[7] = true;
        }
    }

    // 村レベルUP
    public void MuraLevel()
    {
        if (ListFlg[8] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[8].MainText;
            SubText.text = ListCanvas[8].SubText;
            iTorumae.sprite = ListCanvas[8].Torumae;
            iTottaato.sprite = ListCanvas[8].Tottaato;
            iTorumae.sprite = ListCanvas[8].Torumae;
            ListFlg[8] = true;
        }
    }

    // ケモコ交代
    public void Kemokokoutai()
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

    // 操作説明
    public void SousaSetumei()
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

    // 画面説明
    public void GamennSetumenn()
    {
        if (ListFlg[11] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[11].MainText;
            SubText.text = ListCanvas[11].SubText;
            iTorumae.sprite = ListCanvas[11].Torumae;
            iTottaato.sprite = ListCanvas[11].Tottaato;
            iTorumae.sprite = ListCanvas[11].Torumae;
            ListFlg[11] = true;
        }
    }

    // 盤上未仲間ケモコ
    public void No_Kemoko()
    {

        if (ListFlg[12] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[12].MainText;
            SubText.text = ListCanvas[12].SubText;
            iTorumae.sprite = ListCanvas[12].Torumae;
            iTottaato.sprite = ListCanvas[12].Tottaato;
            iTorumae.sprite = ListCanvas[12].Torumae;
            ListFlg[12] = true;
        }
    }

    // ゲームクリア説明
    public void Game_Clear()
    {
        if (ListFlg[13] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[13].MainText;
            SubText.text = ListCanvas[13].SubText;
            iTorumae.sprite = ListCanvas[13].Torumae;
            iTottaato.sprite = ListCanvas[13].Tottaato;
            iTorumae.sprite = ListCanvas[13].Torumae;
            ListFlg[13] = true;
        }
    }

    // ゲームオーバー説明
    public void Game_Over()
    {
        if (ListFlg[14] == false)
        {
            Main.SetActive(true);
            MainText.text = ListCanvas[14].MainText;
            SubText.text = ListCanvas[14].SubText;
            iTorumae.sprite = ListCanvas[14].Torumae;
            iTottaato.sprite = ListCanvas[14].Tottaato;
            iTorumae.sprite = ListCanvas[14].Torumae;
            ListFlg[14] = true;
        }
    }

    // ボタン処理
    public void BacktoGameButton()
    {
        Main.SetActive(false);
    }
}
