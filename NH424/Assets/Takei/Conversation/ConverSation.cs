// ================================================
//  OptionSC.cs[���C���̃I�v�V�����Ǘ�]
// 
// Author:����y�s
//=================================================
// �ύX����
// 2023/02/27 �X�N���v�g�쐬
// 2023/03/06 �C�x���g�쐬�ɍ��킹�ĉ��C��
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class ConverSation : MonoBehaviour
{
    // �\���̐錾
    private struct SConverSation // ��bCSV�p
    {
        public string TConverSation; // ���C���̉�b���e
        public string TCharaName;    // �L�����N�^�[�l�[��
        public int LRNum;            // �����E��
        public int Select;          // �I����
    }

    // ---�ϐ��錾---
    // ��b�e�L�X�g�{�b�N�X�I�u�W�F�N�g
    public GameObject CSMainObj;   // ���C��
    public GameObject CSLeftObj;   // ��
    public GameObject CSRightObj;  // �E
    // ��b�I���{�^���I�u�W�F�N�g
    public GameObject CSYButtonObj; // Yes
    public GameObject CSNButtonObj; // No
    // ��b�e�L�X�g�{�b�N�X
    public TextMeshProUGUI CSMain;       // ���C��
    public TextMeshProUGUI CSCharaLeft;  // ��
    public TextMeshProUGUI CSCharaRight; // �E
    // �I�����{�b�N�X
    public GameObject SelectBox;
    public GameObject SelectBoxY;
    public GameObject SelectBoxN;
    private int SelectNum;
    private bool EventFlg;

    // CSV�t�@�C���֌W
    TextAsset csvFile; // CSV�t�@�C���w��
    private int CSNum; // CSV�t�@�C���̍s�v�Z
    List<SConverSation> csvDatas = new List<SConverSation>(); // CSV�̒��g�����郊�X�g

    // *****************
    // Initialize
    // *****************
    private void Start()
    {
        // �S��b�I�u�W�F�N�g�𖳌���
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
    // �O������Ăяo����b�i�s�p
    //  **************************
    public bool ActiveGameIvent()
    {
        // Enter�L�[���������玟�̃e�L�X�g��
        if (Input.GetKeyDown(KeyCode.Return) && !EventFlg)
        {
            // ---���X�g�����̍s��---
            CSNum++;
            Debug.Log("��b���e�ʉ�");
        }
        // ---�C�x���g������������Ă��邩����---
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

        Debug.Log("���݉�b���e" + csvDatas[CSNum].TConverSation+csvDatas[CSNum].Select);
        // ---�e�L�X�g�`��---
        if (csvDatas[CSNum].TConverSation != "-1")
        {
            DisplayText(csvDatas[CSNum].TConverSation, csvDatas[CSNum].TCharaName, csvDatas[CSNum].LRNum);
        }
        // ���̃L�������E�̃L������
        if (csvDatas[CSNum].LRNum == 0)// ��
        {
            CSLeftObj.SetActive(true);
            CSRightObj.SetActive(false);
        }
        else if (csvDatas[CSNum].LRNum == 1) // �E
        {
            CSRightObj.SetActive(true);
            CSLeftObj.SetActive(false);

        }
        // ---�Ō�̍s�ɂȂ�����I������ƕԂ�---
        if (csvDatas[CSNum].TConverSation == "-1")
        {
            // ---�S�I�u�W�F�N�g�𖳌���---
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
            // CSV�t�@�C����ǂݍ��񂾃��X�g�͏�����
            List<SConverSation> csvDatas = new List<SConverSation>();
            // �I��������false��Ԃ�
            return false;
        }
        return true;
    }
        //  ***************
        // �e�L�X�g�`��֐�
        //  ***************
        // �C�x���g�������ɌĂяo���p ����(��b���e,�����҂̖��O,�����҂������E��0:��1:�E)
        public void DisplayText(string Text, string charaName, int Num)
    {
        // ���C���̃e�L�X�g�{�b�N�X�ɂ��̂܂ܑ��
        CSMain.text = Text;
        if (Num == 0) // 0�Ȃ獶�ɃL�����l�[����
            CSCharaLeft.text = charaName;
        else if (Num == 1) // 1�Ȃ�E�ɃL�����l�[����
            CSCharaRight.text = charaName;
        else
            Debug.Log("�L�����l�[���`��ŃG���[���������܂���");
    }

    // ********************
    // CSV�t�@�C���ǂݍ���
    // ********************
    private void LoadSCVFile(string CSVFileName)
    {
        // �ꎟ�ۑ��p�̕ϐ�
        SConverSation SCStemp = new SConverSation();
        List<string[]> SCStempList = new List<string[]>();
        int iLines = 0; // �s�v�Z

        // Resources����CSV���Ăяo��
        csvFile = Resources.Load(CSVFileName) as TextAsset;

        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1�s���ǂݍ���
            SCStempList.Add(line.Split(',')); // ,�ŋ�؂��ă��X�g�ɒǉ�
            iLines++; // �s�����Z
        }

        for(int i=0;i<iLines;i++)
        {
            SCStemp.TConverSation = SCStempList[i][0];
            SCStemp.TCharaName = SCStempList[i][1];
            SCStemp.LRNum = Convert.ToInt32(SCStempList[i][2]);
            SCStemp.Select = Convert.ToInt32(SCStempList[i][3]);

            csvDatas.Add(SCStemp);
        }
        // �Ō�ɏI���̍s������
        SCStemp.TConverSation = "-1";
        SCStemp.TCharaName = "-1";
        SCStemp.LRNum = -1;
        SCStemp.Select = -1;

        csvDatas.Add(SCStemp);
    }

    // ��b����Initilize
    public bool ConverSationInit(string CSVFileName)
    {
        // CSV�t�@�C���ǂݍ���
        LoadSCVFile(CSVFileName);
        // ---�K�v�S�I�u�W�F�N�g��L����---
        CSMainObj.SetActive(true); // ��b���C���I�u�W�F�N�g�L����
        CSNum = 0; // CSV�t�@�C���̉�b�v�Z�p�̐�����1�ɂ���(1�s�ڂ͐����p)

        // ---�ŏ��̉�b��\��---
        DisplayText(csvDatas[CSNum].TConverSation, csvDatas[CSNum].TCharaName, csvDatas[CSNum].LRNum);
        
        // --- �L�����N�^�[�͉E������---
        if (csvDatas[CSNum].LRNum == 0) // ��
        {
            // ����L�������E�𖳌���
            CSLeftObj.SetActive(true);
            CSRightObj.SetActive(false);
        }
        else if (csvDatas[CSNum].LRNum == 1) // �E
        {
            // ���𖳌������E��L����
            CSLeftObj.SetActive(false);
            CSRightObj.SetActive(true);
        }

        // �t���O�Ǘ��̈�true��Ԃ�
        return true;
    }

    // �C�x���g�����{�^���Ǘ�
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
