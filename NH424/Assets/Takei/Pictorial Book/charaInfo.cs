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
    static private string PB_CSVFile = "CSV/PictorialBook"; // CSV�f�[�^�ۑ��ꏊ
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
