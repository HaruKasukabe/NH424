// ================================================
//  PictorialBook.cs[�}�ӊǗ�]
// 
// Author:����y�s
//=================================================
// �ύX����
// 2023/03/04 �}�ӊǗ��\�[�X�쐬
// 2023/04/09 �}�Ӑ���(��������)
// 2023/04/10 
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public class PictorialBook : MonoBehaviour
{
    // �\���̐錾
    private struct CharacterInfo
    {
        public int Num;        // �L�����N�^�[�ԍ�
        public string Name;    // �L�����N�^�[�l�[��
        public string Motif;   // �L�����N�^�[���`�[�t
        public int Encounter;  // �o�������
        public string Sex;     // ����
        public int MoveArea;   // �ړ��͈�
        public string Comment; // �ꌾ�R�����g
        public string Tag1;    // �^�O1
        public string Tag2;    // �^�O2
        public string Tag3;    // �^�O3
    };
    
    // �ϐ��錾
    public GameObject PictorialBookobj; // �L�����N�^�[�}�ӃO���[�v�I�u�W�F�N�g
    private bool OpenFlg; // �L�����N�^�[�}�ӂ��J���Ă��邩
    private List<CharacterInfo> CharaInfo = new List<CharacterInfo>(); // �L�����N�^�[���Ǘ��\����
    static private string PB_CSVFile = "CSV/PictorialBook"; // CSV�f�[�^�ۑ��ꏊ
    private TextAsset csvFile;
    private int pbNum; // �����Ă�}�ӂ̈ʒu

    public TextMeshProUGUI TB_CharacterName; // �L�����N�^�[�l�[��
    public TextMeshProUGUI TB_Motif; // ���`�[�t
    public TextMeshProUGUI TB_Sex; // ����
    public TextMeshProUGUI TB_Encounter; // ������
    public TextMeshProUGUI TB_MoveArea; // �ړ��͈�
    public TextMeshProUGUI TB_Comment; // �ꌾ�R�����g
    public TextMeshProUGUI TB_Tags; // �^�O

    public Image LeftPB;

    private Sprite s_Crow;         // �J���X�̉摜
    private Sprite s_Rabbit;       // �E�T�M�̉摜
    private Sprite s_Wolf;         // �I�I�J�~�̉摜
    private Sprite s_Giraffe;      // �L�����̉摜
    private Sprite s_Cat;          // �l�R�̉摜
    private Sprite s_Mouse;        // �l�Y�~�̉摜
    private Sprite s_Squirrel;     // ���X�̉摜
    private Sprite s_Bat;          // �R�E�����̉摜
    private Sprite s_Crocodile;    // ���j�̉摜
    private Sprite s_Elephant;     // �]�E�̉摜
    private Sprite s_Frog;         // �J�G���̉摜
    private Sprite s_Headgehog;    // �n���l�Y�~�̉摜
    private Sprite s_Horse;        // �E�}�̉摜
    private Sprite s_Lion;         // ���C�I���̉摜
    private Sprite s_Mole;         // ���O���̉摜
    private Sprite s_Panda;        // �p���_�̉摜
    private Sprite s_Swan;         // �n�N�`���E�̉摜
    private Sprite s_Turtle;       // �J���̉摜
    private Sprite s_Tiger;        // �g���̉摜
    private Sprite s_Sheep;        // �q�c�W�̉摜
    private Sprite s_Undiscavered; // �������̉摜

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

    // Initilize
    void Start()
    {
        OpenFlg = false; // �����l�Ő}�ӂ͕��Ă���
        PictorialBookobj.SetActive(false); // �}�ӃI�u�W�F�N�g�͔�\��
        InitCharaInfo(); // �L������񏉊�������
        pbNum = 0; // �����l�͍���
        InitCharaTexture(); // �e�N�X�`������������
    }

    // �X�V�֐�
    void Update()
    {
        // Z�L�[�������Ɛ}�ӂ��J��
        if(Input.GetKeyDown(KeyCode.Z)&& !OpenFlg)
        {
            OpenFlg = !OpenFlg; // �t���O�𔽓]
            PictorialBookobj.SetActive(OpenFlg); // �}�ӃI�u�W�F�N�g�\��
            pbNum = 0;
            CheckDiscoverChara();
            CheckLeftPB();
            DisplayTextBox();

        }
    }


    // ==========================
    // �L�����������Ăяo���֐�
    // ==========================
    public void DiscoverCharacter(int Num)
    {
        CharacterInfo ciTemp = new CharacterInfo();
        ciTemp = CharaInfo[Num];
        ciTemp.Encounter++;
        CharaInfo[Num] = ciTemp;
    }

    // ==========================
    // �e�L�X�g�{�b�N�X�X�V�֐�
    // ==========================
    private void DisplayTextBox()
    {
        // �o����Ă�������\��
        if (CharaInfo[pbNum].Encounter != 0)
        {
            // �L�����N�^�[�l�[���X�V
            TB_CharacterName.text = CharaInfo[pbNum].Name;
            // ���`�[�t�X�V
            TB_Motif.text = CharaInfo[pbNum].Motif;
            // �o�������
            TB_Encounter.text = CharaInfo[pbNum].Encounter.ToString() + "��";
            // ���ʍX�V
            TB_Sex.text = CharaInfo[pbNum].Sex;
            // �ړ��͈͍X�V
            TB_MoveArea.text = CharaInfo[pbNum].MoveArea.ToString() + "�}�X";
            // �ꌾ�R�����g�X�V
            TB_Comment.text = CharaInfo[pbNum].Comment;
            // �^�O�X�V
            TB_Tags.text = CharaInfo[pbNum].Tag1;
            if (CharaInfo[pbNum].Tag2 != "Null")
                TB_Tags.text = TB_Tags + " " + CharaInfo[pbNum].Tag2;
            if (CharaInfo[pbNum].Tag3 != "Null")
                TB_Tags.text = TB_Tags + " " + CharaInfo[pbNum].Tag3;
        }
        else
        {
            // �L�����N�^�[�l�[���X�V
            TB_CharacterName.text = "???";
            // ���`�[�t�X�V
            TB_Motif.text = "???";
            // �o�������
            TB_Encounter.text = "������";
            // ���ʍX�V
            TB_Sex.text = "???";
            // �ړ��͈͍X�V
            TB_MoveArea.text = "???";
            // �ꌾ�R�����g�X�V
            TB_Comment.text = "???";
            // �^�O�X�V
            TB_Tags.text = "???"; 
        }
    }

    // ==========================
    // �L�����N�^�[��񏉊����֐�
    // ==========================
    private void InitCharaInfo()
    {
        // �L�����N�^�[�f�[�^�Ȃǂ�CSV�t�@�C������ǂݍ����
        // �o������񐔂͍���Z�[�u�@�\�����ƂƂ��ɕʃt�@�C��
        // ����ǂݍ��ޗ\�聩csv�㏑���@�\��Unity�ɂȂ�����

        // �ꎟ�ۑ��p�̕ϐ�
        CharacterInfo CItemp = new CharacterInfo();
        List<string[]> CItempList = new List<string[]>();
        int iLines = 0; // �s�v�Z

        // Resources����CSV���Ăяo��
        csvFile = Resources.Load(PB_CSVFile) as TextAsset;

        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1�s���ǂݍ���
            CItempList.Add(line.Split(',')); // ,�ŋ�؂��ă��X�g�ɒǉ�
            iLines++; // �s�����Z
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
        // �Ō�ɏI���̍s������
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

    // ==========================
    // �L�����e�N�X�`���������֐�
    // ==========================
    private void InitCharaTexture()
    {
        // �e�N�X�`�������\�[�X����ǂݍ���
        s_Crow = Resources.Load<Sprite>("Image/Crow");
        s_Rabbit = Resources.Load<Sprite>("Image/Rabbit");
        s_Wolf = Resources.Load<Sprite>("Image/Wolf");
        s_Giraffe = Resources.Load<Sprite>("Image/Giraffe");
        s_Cat = Resources.Load<Sprite>("Image/Cat");
        s_Mouse = Resources.Load<Sprite>("Image/Mouse");
        s_Squirrel = Resources.Load<Sprite>("Image/Squirrel");
        s_Bat = Resources.Load<Sprite>("Image/Nodata");
        s_Crocodile = Resources.Load<Sprite>("Image/Nodata");
        s_Elephant = Resources.Load<Sprite>("Image/Nodata");
        s_Frog = Resources.Load<Sprite>("Image/Nodata");
        s_Headgehog = Resources.Load<Sprite>("Image/Nodata");
        s_Horse = Resources.Load<Sprite>("Image/Nodata");
        s_Lion = Resources.Load<Sprite>("Image/Nodata");
        s_Mole = Resources.Load<Sprite>("Image/Nodata");
        s_Panda = Resources.Load<Sprite>("Image/Nodata");
        s_Swan = Resources.Load<Sprite>("Image/Nodata");
        s_Turtle = Resources.Load<Sprite>("Image/Nodata");
        s_Tiger = Resources.Load<Sprite>("Image/Nodata");
        s_Sheep = Resources.Load<Sprite>("Image/Nodata");
        s_Undiscavered = Resources.Load<Sprite>("Image/Undiscovered");

        // �ǂݍ��񂾃��\�[�X��Image�ɓn��
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
    // �L���������`�F�b�N�֐�
    // ==========================
    private void CheckDiscoverChara()
    {
        if (CharaInfo[0].Encounter != 0)
            i_Crow.sprite = s_Crow;
        if (CharaInfo[1].Encounter != 0)
            i_Rabbit.sprite = s_Rabbit;
        if (CharaInfo[2].Encounter != 0)
            i_Wolf.sprite = s_Wolf;
        if (CharaInfo[3].Encounter != 0)
            i_Giraffe.sprite = s_Giraffe;
        if (CharaInfo[4].Encounter != 0)
            i_Cat.sprite = s_Cat;
        if (CharaInfo[5].Encounter != 0)
            i_Mouse.sprite = s_Mouse;
        if (CharaInfo[6].Encounter != 0)
            i_Squirrel.sprite = s_Squirrel;
        if (CharaInfo[7].Encounter != 0)
            i_Bat.sprite = s_Bat;
        if (CharaInfo[8].Encounter != 0)
            i_Crocodile.sprite = s_Crocodile;
        if (CharaInfo[9].Encounter != 0)
            i_Elephant.sprite = s_Elephant;
        if (CharaInfo[10].Encounter != 0)
            i_Frog.sprite = s_Frog;
        if (CharaInfo[11].Encounter != 0)
            i_Headgehog.sprite = s_Headgehog;
        if (CharaInfo[12].Encounter != 0)
            i_Horse.sprite = s_Horse;
        if (CharaInfo[13].Encounter != 0)
            i_Lion.sprite = s_Lion;
        if (CharaInfo[14].Encounter != 0)
            i_Mole.sprite = s_Mole;
        if (CharaInfo[15].Encounter != 0)
            i_Panda.sprite = s_Panda;
        if (CharaInfo[16].Encounter != 0)
            i_Swan.sprite = s_Swan;
        if (CharaInfo[17].Encounter != 0)
            i_Turtle.sprite = s_Turtle;
        if (CharaInfo[18].Encounter != 0)
            i_Tiger.sprite = s_Tiger;
        if (CharaInfo[19].Encounter != 0)
            i_Sheep.sprite = s_Sheep;
    }

    // ==========================
    // �}�Ӎ����`�F�b�N�֐�
    // ==========================
    private void CheckLeftPB()
    {
        if(CharaInfo[pbNum].Encounter !=0)
        {
            if (CharaInfo[0].Encounter != 0)
                LeftPB.sprite = s_Crow;
            if (CharaInfo[1].Encounter != 0)
                LeftPB.sprite = s_Rabbit;
            if (CharaInfo[2].Encounter != 0)
                LeftPB.sprite = s_Wolf;
            if (CharaInfo[3].Encounter != 0)
                LeftPB.sprite = s_Giraffe;
            if (CharaInfo[4].Encounter != 0)
                LeftPB.sprite = s_Cat;
            if (CharaInfo[5].Encounter != 0)
                LeftPB.sprite = s_Mouse;
            if (CharaInfo[6].Encounter != 0)
                LeftPB.sprite = s_Squirrel;
            if (CharaInfo[7].Encounter != 0)
                LeftPB.sprite = s_Bat;
            if (CharaInfo[8].Encounter != 0)
                LeftPB.sprite = s_Crocodile;
            if (CharaInfo[9].Encounter != 0)
                LeftPB.sprite = s_Elephant;
            if (CharaInfo[10].Encounter != 0)
                LeftPB.sprite = s_Frog;
            if (CharaInfo[11].Encounter != 0)
                LeftPB.sprite = s_Headgehog;
            if (CharaInfo[12].Encounter != 0)
                LeftPB.sprite = s_Horse;
            if (CharaInfo[13].Encounter != 0)
                LeftPB.sprite = s_Lion;
            if (CharaInfo[14].Encounter != 0)
                LeftPB.sprite = s_Mole;
            if (CharaInfo[15].Encounter != 0)
                LeftPB.sprite = s_Panda;
            if (CharaInfo[16].Encounter != 0)
                LeftPB.sprite = s_Swan;
            if (CharaInfo[17].Encounter != 0)
                LeftPB.sprite = s_Turtle;
            if (CharaInfo[18].Encounter != 0)
                LeftPB.sprite = s_Tiger;
            if (CharaInfo[19].Encounter != 0)
                LeftPB.sprite = s_Sheep;
        }
        else
        {
            LeftPB.sprite = s_Undiscavered;
        }
    }

    // ==========================
    // �{�^�������֐�
    // ==========================
    public void SelectLeftCButton()
    {
        if (pbNum > 0)
        {
            pbNum--;
            Debug.Log("�ʉ�");
            CheckLeftPB();
            DisplayTextBox();
        }
    }
    public void SelectRightCButton()
    {
        if (pbNum < 19)
        {
            pbNum++;
            Debug.Log("�ʉ�");
            CheckLeftPB();
            DisplayTextBox();
        }
    }

    // �}�ӂ����{�^�����������Ƃ�
    public void BacktoMapButton()
    {
        OpenFlg = false;
        PictorialBookobj.SetActive(OpenFlg);
    }
}