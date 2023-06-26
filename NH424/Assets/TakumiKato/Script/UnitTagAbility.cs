//=============================================================================
//
// ���j�b�g�̃^�O���� �N���X [UnitTagAbility.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �^�O���
public struct tagData
{
    public int num;
    public List<UnitData> unitData;
    public bool bAbility;
}

public class UnitTagAbility : MonoBehaviour
{
    public static UnitTagAbility instance = null;

    public tagData[] tagDatas = new tagData[(int)UnitTag.MAX];  // �^�O�̎�ޕ��̃^�O���z��
    public int[] numConditions; // �\�͔���������

    int bandTagNum = 0;
    bool woodShadow = true;
    bool mostOld = true;

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
        // �^�O����������
        for (int i = 0; i < tagDatas.Length; i++)
        {
            tagDatas[i].num = 0;
            tagDatas[i].unitData = new List<UnitData>();
            tagDatas[i].bAbility = false;
        }

        GameObject[] unit = Map.instance.unit;
        for (int i = 0; i < unit.Length; i++)
        {
            UnitData sta = unit[i].GetComponent<Unit>().sta;
            for(int j = 0; j < sta.unitTag.Length; j++)
            {
                tagDatas[(int)sta.unitTag[j]].unitData.Add(sta);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���ԂɂȂ������j�b�g�̃^�O����ǉ�
    public void addTag(Unit unit)
    {
        UnitTag[] tag = unit.sta.unitTag;
        for (int i = 0; i < tag.Length; i++)
        {
            tagDatas[(int)tag[i]].num += 1;
        }

        for(int i = 0; i < tagDatas.Length; i++)
        {
            if (GetCondition((UnitTag)i))
            {
                if (!tagDatas[i].bAbility)
                {
                    SetAbility(tagDatas[i].unitData, (UnitTag)i, true);
                    tagDatas[i].bAbility = true;
                }
            }
            else if (tagDatas[i].bAbility)
            {
                SetAbility(tagDatas[i].unitData, (UnitTag)i, false);
                tagDatas[i].bAbility = false;
            }
        }
    }
    // ���Ȃ��Ȃ������j�b�g�̃^�O���폜
    public void deleteTag(Unit unit)
    {
        UnitTag[] tag = unit.sta.unitTag;
        for (int i = 0; i < tag.Length; i++)
        {
            tagDatas[(int)tag[i]].num -= 1;
        }

        for (int i = 0; i < tagDatas.Length; i++)
        {
            if (GetCondition((UnitTag)i))
            {
                if (!tagDatas[i].bAbility)
                {
                    SetAbility(tagDatas[i].unitData, (UnitTag)i, true);
                    tagDatas[i].bAbility = true;
                }
            }
            else if (tagDatas[i].bAbility)
            {
                SetAbility(tagDatas[i].unitData, (UnitTag)i, false);
                tagDatas[i].bAbility = false;
            }
        }
    }

    // �\�́@true : �����Afalse : ���ɖ߂�
    void SetAbility(List<UnitData> unitDatas, UnitTag tag, bool b)
    {
        switch(tag)
        {
            case UnitTag.��l��:
                if (b)
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveNum += 1;  // �ړ��\���P�񑝉�
                    }
                }
                else
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveNum -= 1;
                    }
                }
                break;
            case UnitTag.�C���^�[�l�b�g�C���j��:
                if (b)
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveNum += 1;  // �ړ��\���P�񑝉�
                    }
                }
                else
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveNum -= 1;
                    }
                }
                break;
            case UnitTag.�ŔN���g:
                if (b && mostOld)
                {
                    GameManager.instance.nowTurn -= 3;
                    mostOld = false;
                }
                break;
            case UnitTag.�������Ⴂ����N���u:
                if (b)
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveLong += 1;  // �ړ��\���P�񑝉�
                    }
                }
                else
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveLong -= 1;
                    }
                }
                break;
            case UnitTag.�o���h:
                if (b)
                {
                    int dif = tagDatas[(int)tag].num - bandTagNum;
                    if (dif > 0)
                    {
                        GameManager.instance.nowTurn -= dif;
                        bandTagNum = tagDatas[(int)tag].num;
                    }
                }
                break;
            case UnitTag.�؉A�ň�x��:
                if (b && woodShadow)
                {
                    GameManager.instance.nowTurn -= 3;
                    woodShadow = false;
                }
                break;
        }
    }

    // �\�͔��������`�F�b�N
    bool GetCondition(UnitTag tag)
    {
        switch (tag)
        {
            case UnitTag.��l��:
                if (KemokoListOut.instance.outUnitList.Count <= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.�C���^�[�l�b�g�C���j��:
                if (KemokoListOut.instance.outUnitList.Count <= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.�ŔN���g:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.�������Ⴂ����N���u:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.�o���h:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.�؉A�ň�x��:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    // ���̃A�C�h�����擾
    public int GetVillageIdol()
    {
        int num = tagDatas[(int)UnitTag.���̃A�C�h��].num;
        if (num > 1)
            return num;
        else
            return 0;
    }

    // �Q��q�͈���擾
    public int GetSleep()
    {
        int num = tagDatas[(int)UnitTag.�Q��q�͈��].num;
        if (GameManager.instance.IsEven(num))
            return num;
        else
            return 0;
    }
}
