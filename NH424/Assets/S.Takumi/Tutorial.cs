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
        // �H���}�X
        CanvasStruct temp;
        temp.MainText = "�H���}�X";
        temp.SubText = "���̃}�X�͐H���}�X�ł��B\n�����P���R�𒇊Ԃɂ���ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Food_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Food_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Food_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);
        // �؍ރ}�X
        temp.MainText = "�؍ރ}�X";
        temp.SubText = "���̃}�X�͖؍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Wood_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Wood_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Wood_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �S�ރ}�X
        temp.MainText = "�S�ރ}�X";
        temp.SubText = "���̃}�X�͓S�ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Iron_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Iron_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Iron_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �΍ރ}�X
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �D�F�}�X
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // ���}�X
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �f�ރ}�X
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // ����ʐ���
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �����x��UP
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �P���R���
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // ����
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

         // ��ʐ���
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �Ֆʏ㖢���ԃP���R
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �Q�[���N���A����
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �Q�[���I�[�o�[����
        temp.MainText = "�΍ރ}�X";
        temp.SubText = "���̃}�X�͐΍ރ}�X�ł��B\n�������x���A�b�v����ۂɎg�p����ޗ��ł��B";
        temp.Torumae = Resources.Load<Sprite>("Image/Stone_Before");
        temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

    }

    // �H��
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

    // �؍�
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

    // �S��
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

    // �΍�
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

    // �D�F�}�X
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

    // ���}�X
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

    // �f�ރ}�X
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

    // ����ʐ���
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

    // �����x��UP
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

    // �P���R���
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

    // �������
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

    // ��ʐ���
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

    // �Տ㖢���ԃP���R
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

    // �Q�[���N���A����
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

    // �Q�[���I�[�o�[����
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

    // �{�^������
    public void BacktoGameButton()
    {
        Main.SetActive(false);
    }
}
