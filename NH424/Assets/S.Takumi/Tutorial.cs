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
        temp.MainText = "�D�F�}�X";
        temp.SubText = "�D�F�}�X�̓L�����N�^�[���ړ��ł��関�����}�X�ł��B\n�}�X�ɐG��邱�Ƃɂ���āA�ޗ��}�X��P���R�}�X�������邱�Ƃ��ł���B";
        temp.Torumae = Resources.Load<Sprite>("Image/GrayMasu");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // ���}�X
        temp.MainText = "���}�X";
        temp.SubText = "���F�}�X�́A�L�����N�^�[���ړ��ł��Ȃ��}�X�ł��B\n�D�F�}�X���J�񂷂邱�Ƃɂ���Ĉړ��ł���悤�ɂȂ�܂��B";
        temp.Torumae = Resources.Load<Sprite>("Image/BlackMasu");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);     

        // �����x��UP
        temp.MainText = "�����x��UP�ɂ���";
        temp.SubText = "���A�C�R�����^�b�v���邱�Ƃő������m�F���邱�Ƃ��ł���B\n�ޗ�������đ����x�����グ�邱�Ƃł��T���͈͂��L�����邱�Ƃ��ł���B";
        temp.Torumae = Resources.Load<Sprite>("Image/VillageUp");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �Ֆʏ㖢���ԃP���R
        temp.MainText = "�Ֆʏ㖢���ԃP���R�ɂ���";
        temp.SubText = "�Տ�̃P���R�̏�ɗ����Ƃɂ��A\n�H��������ăP���R�𒇊Ԃɂ��邱�Ƃ��ł���B";
        temp.Torumae = Resources.Load<Sprite>("Image/Alternation");
        //temp.Tottaato = Resources.Load<Sprite>("Image/Ston_catch");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        // �P���R���
        temp.MainText = "�P���R���ɂ���";
        temp.SubText = "�P���R�̏�������5�̈ȏ�ɂȂ�ƁA���ɍs���ăP���R����シ�邱�Ƃ��\�ɂȂ�B\n�O�ɂ���P���R�Ƒ��ɂ���P���R�̃A�C�R�����^�b�v���邱�ƂŌ�シ�邱�Ƃ��ł���B";
        temp.Torumae = Resources.Load<Sprite>("Image/NakamaMae");
        temp.Tottaato = Resources.Load<Sprite>("Image/NakamaTotyuu");
        //temp.Kazuhyouzi = Resources.Load<Sprite>("Image/Ston_count");
        ListCanvas.Add(temp);
        ListFlg.Add(false);

        /*
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
        */

    }

    // �H���}�X
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

    // �؍ރ}�X
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

    // �S�ރ}�X
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

    // �΍ރ}�X
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
            iTorumae.sprite = ListCanvas[4].Kazuhyouzi;
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
            iTorumae.sprite = ListCanvas[5].Kazuhyouzi;
            ListFlg[5] = true;
        }
    }

    // �����x��UP
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

    // �Տ㖢���ԃP���R
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

    // �P���R���
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
    // �Q�[���N���A����
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

    // �Q�[���I�[�o�[����
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

    // �{�^������
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
