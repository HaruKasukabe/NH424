//=============================================================================
//
// �Q�[���}�l�[�W���[ �N���X [GameManager.cpp]
//
//=============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �G��
public enum SEASON
{
    SPRING,
    SUMMER,
    FALL,
    WINTER,

    MAX
}
// float x, z
public struct FLOAT2
{
    public float x;
    public float z;

    public FLOAT2(float a, float b)
    {
        x = a;
        z = b;
    }
};
// int x, z
public struct INT2
{
    public int x;
    public int z;

    public INT2(int a, int b)
    {
        x = a;
        z = b;
    }
};

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] ManagementAudio m_audiosc;         // �I�[�f�B�I�Ǘ��X�N���v�g
    [SerializeField] GameObject UICursol;               // UI�J�[�\���I�u�W�F�N�g
    [SerializeField] GameObject CharacterUI;            // �L�����N�^�[�̃X�e�[�^�X���UI�I�u�W�F�N�g
    [SerializeField] Fade fade;                         // �t�F�C�h
    [SerializeField] GameObject stage;                  // ���j�b�g�̈ړ��͈̓I�u�W�F�N�g
    [SerializeField] Vector3[] stageScale;              // �ړ��͈͂̑傫���z��
    [SerializeField] Material[] skyMat;                 // ��̃}�e���A��4�G�ߕ�

    public SEASON season = SEASON.SPRING;               // �G��
    int seasonRoundNum = 0;                             // �G�߂�1�T������
    int seasonTurnNum = 31;                             // �G�߂̃^�[����
    public int nowTurn = 1;                             // ���̃^�[����
    public int canActUnitNum = 0;                       // ���j�b�g�𓮂����鐔
    int originActUnitNum;                               // �^�[�����߂̃��j�b�g�𓮂����鐔
    int moveNumTotal = 0;                               // �����邷�ׂẴ��j�b�g�̈ړ��\�񐔂����v������
    public List<int> friendCatList = new List<int>();   // ���Ԃɂ�����ނ̃��X�g
    public int friendNum = 1;                           // ���Ԃ̐�
    bool bClear = false;                                // �N���A�������̃v���O���������s������

    public int level = 0;                               // ���݂̑����x��
    public int maxVillageLevel = 3;                     // �����g�傷��ő僌�x��
    public float food = 0.0f;                           // �H��
    public float wood = 0.0f;                           // �؍�
    public float stone = 0.0f;                          // �΍�
    public float iron = 0.0f;                           // �S��
    public float levelUpNeed = 100.0f;                  // ���x���A�b�v�ɕK�v�ȑf�ނ̐�

    public bool bFirstReset = true;                     // �ŏ��̃��Z�b�g��������

    [SerializeField] OptionSC option;                   // �I�v�V����
    public PictorialBook book;                          // �}��

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

    void Start()
    {
        fade.FadeOut(2.0f);         // �t�F�C�h�A�E�g
        book.DiscoverCharacter(0);  // �ŏ��̃��j�b�g�̐}�Ӊ��
    }

    void Update()
    {
        // �ŏ��̃��Z�b�g
        if(bFirstReset)
        {
            FirstReset();
            if(canActUnitNum > 0)
                bFirstReset = false;
        }

        // ���Ԃɂ�����ސ���20�ȏ�̎��N���A
        if (friendCatList.Count >= 20 && !bClear)
        {
            WindowEffect.instance.PlayClearEffect();    // �Q�[���N���A�G�t�F�N�g���Đ�
            StartCoroutine(DelayCoroutine(3, () =>      // �R�҂��Ă���
            {
                ScoreManager.instance.ScoreAdd(KemokoListOut.instance.outUnitList, KemokoListVillage.instance.villageUnitList); // ���j�b�g�̃X�R�A���c��
                fade.FadeIn(2.0f, () => { SceneManager.LoadScene("ResultScene"); });    // ���U���g�V�[���Ƀt�F�C�h�C��
            }));
            bClear = true;
        }

        // �^�[�����I��
        if(!bFirstReset)    // �ŏ��̃��Z�b�g�������Ƃ�����
            if (canActUnitNum <= 0 || (Input.GetButtonDown("TurnEnd") && bMenuDisplay()))
                EndTurn();

        // ���̃^�[�����G�߂̍ő�^�[�����ɂȂ����Ƃ�
        if (nowTurn == seasonTurnNum)
        {
            // �G�߃~�b�V�������N���A�ł��Ȃ�������
            if (!SeasonMission.instance.Check())
            {
                WindowEffect.instance.PlayOverEffect(); // �Q�[���I�[�o�[�G�t�F�N�g���Đ�
                StartCoroutine(DelayCoroutine(3, () =>  // �R�҂��Ă���
                {
                    ScoreManager.instance.ScoreAdd(KemokoListOut.instance.outUnitList, KemokoListVillage.instance.villageUnitList); // ���j�b�g�̃X�R�A���c��
                    fade.FadeIn(2.0f, () => { SceneManager.LoadScene("ResultScene"); });    // ���U���g�V�[���Ƀt�F�C�h�C��
                }));
            }

            m_audiosc.NextSeason(); // BGM�����̋G�߂̂�
            nowTurn = 1;            // ���̃^�[����1�ɐݒ�
            season++;               // ���̋G�߂�

            // �G�߂��~�ł����
            if (season == SEASON.MAX)
            {
                season = SEASON.SPRING;
                seasonRoundNum++;   // �G�ߎ��񐔂�+1
            }

            WindowEffect.instance.ChangeSeasonEffect();     // �G�߃G�t�F�N�g��؂�ւ�
            RenderSettings.skybox = skyMat[(int)season];    // ��̐F��؂�ւ�
            SeasonIconUI.instance.SetSeasonIcon();          // �G�߃A�C�R��UI��؂�ւ�
            SeasonEvent.instance.ResetEvent();              // �G�߃C�x���g�����Z�b�g
            KemokoListOut.instance.SetGourmet();            // ���H�Ƃ̃^�O�\�̓��Z�b�g
            KemokoListOut.instance.SetCarbonated();         // �Y�_���̃^�O�\�̓��Z�b�g
        }

        // B�{�^�����������Ƃ���UI�J�[�\��������
        if (Input.GetButtonDown("Fire2"))
            SetUICursol(false);
    }

    // �����x���A�b�v
    public void LevelUp()
    {
        if(wood >= levelUpNeed)
            if (stone >= levelUpNeed)
                if (iron >= levelUpNeed)
                {
                    wood -= levelUpNeed;
                    stone -= levelUpNeed;
                    iron -= levelUpNeed;
                    level++;
                    levelUpNeed = 100.0f * (level + 1);     // ���x���A�b�v�ɕK�v�ȑf�ނ̐���ݒ�

                    // ���x����4�ȉ��̎��Ƃ̃��f����؂�ւ�
                    if(level < 5)
                        Map.instance.LevelUpHouse();
                }
    }

    // ���j�b�g��ǉ�
    public void AddUnit(Unit unit)
    {
        if (!KemokoListOut.instance.Add(unit))
            KemokoListVillage.instance.Add(unit);
    }
    // �V���b�v�Ŕ��������j�b�g��ǉ�
    public void AddSelectUnit(Unit unit)
    {
        if (!KemokoListOut.instance.SelectAdd(unit))
            KemokoListVillage.instance.Add(unit);
    }

    // �^�[���I��
    public void EndTurn()
    {
        // �������j���[��\�����Ă��Ȃ���
        if (bMenuDisplay())
        {
            nowTurn++;  // �^�[������+1
            List<Unit> unitList = KemokoListOut.instance.outUnitList;

            ShopList.instance.ChengeList(); // �V���b�v�̕��т�ύX
            KemokoListOut.instance.SetSleep(); // �Q��q�͈�̃^�O�\�̓t���O�Z�b�g

            moveNumTotal = KemokoListOut.instance.GetMoveNumTotal();
            if (moveNumTotal < KemokoListOut.instance.maxOutNum)    // �ő�ړ��\����5��菬�����ꍇ
                originActUnitNum = canActUnitNum = moveNumTotal;
            else
                originActUnitNum = canActUnitNum = KemokoListOut.instance.maxOutNum;

            for (int i = 0; i < unitList.Count; i++)
                unitList[i].SetAct();   // ���j�b�g�̈ړ��\�񐔂����Z�b�g
        }
    }

    // �ŏ��̃��Z�b�g
    void FirstReset()
    {
        List<Unit> unitList = KemokoListOut.instance.outUnitList;

        ShopList.instance.ChengeList();
        KemokoListOut.instance.SetSleep();

        moveNumTotal = KemokoListOut.instance.GetMoveNumTotal();
        if (moveNumTotal < KemokoListOut.instance.maxOutNum)
           originActUnitNum = canActUnitNum = moveNumTotal;
        else
           originActUnitNum = canActUnitNum = KemokoListOut.instance.maxOutNum;

        for (int i = 0; i < unitList.Count; i++)
            unitList[i].SetAct();
    }

    // ���������
    public bool IsEven(int num)
    {
        return (num % 2 == 0);
    }

    // �������j���[��\�����Ă��邩
    public bool bMenuDisplay()
    {
        if (!Tutorial.instance.Main.activeSelf && !option.bOpenOption())
        {
            if (SelectButtons.instance.GetbFriendSelect() && book.GetOpenFlg())
            {
                if (MissionButton.instance.GetbMenu())
                {
                    if (ShopButton.instance.GetbMenu() && VillageButton.instance.GetbMenu())
                    {
                        SetUICursol(false);
                        return true;
                    }
                    else
                    {
                        SetUICursol(true);
                        return false;
                    }
                }
                else
                {
                    SetUICursol(true);
                    return false;
                }
            }
            else
            {
                SetUICursol(true);
                return false;
            }
        }
        else
            return false;
    }

    // UI�J�[�\����ݒ�
    public void SetUICursol(bool act)
    {
        UICursol.gameObject.SetActive(act);
    }

    // �I���������j�b�g�̏��UI��ݒ�
    public void SetCharacterUI(bool act, Unit unit)
    {
        CharacterUI.gameObject.SetActive(act);
        stage.SetActive(act);
        Vector3 pos = new Vector3(unit.transform.position.x, unit.transform.position.y + 0.2f, unit.transform.position.z);
        stage.transform.position = pos;
        stage.transform.localScale = stageScale[unit.sta.moveLong];

        if (act)
            CharacterUI.GetComponentInChildren<SelectCharaUI>().SetUnit(unit);
    }

    // ���Ԃ̎�ސ��ǉ�
    public void AddFriendCatNum(Unit unit)
    {
        bool bAdd = true;
        for(int i = 0; i < friendCatList.Count; i++)
        {
            if (friendCatList[i] == unit.sta.number)
                bAdd = false;
        }
        // ���܂Œ��Ԃɂ��Ă��Ȃ��������j�b�g�Ȃ�
        if(bAdd)
        {
            friendCatList.Add(unit.sta.number);
        }
    }

    // ���Ԃɂ������Ƃ������ނ̃��j�b�g������
    public bool bFriendCat(int number)
    {
        bool bFriend = false;
        for (int i = 0; i < friendCatList.Count; i++)
        {
            if (friendCatList[i] == number)
                bFriend = true;
        }
        return bFriend;
    }

    // �P��ł����j�b�g�������Ă��邩
    public bool bMoveUnitThisTurn()
    {
        if (canActUnitNum != originActUnitNum)
            return false;
        else
            return true;
    }

    // ��莞�Ԍ�ɏ������Ăяo���R���[�`��
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}