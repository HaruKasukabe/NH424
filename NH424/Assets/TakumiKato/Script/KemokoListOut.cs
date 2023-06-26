//=============================================================================
//
// �O�ɂ���P���R���X�g �N���X [KemokoListOut.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KemokoListOut : MonoBehaviour
{
    public static KemokoListOut instance = null;
    public List<Unit> outUnitList = new List<Unit>();               // �O�̃��j�b�g���X�g
    public List<UnitImage> outImageList = new List<UnitImage>();    // �O�̃��j�b�g�̃C���[�W���X�g
    int selectId;               // �������郆�j�b�g��Id
    public bool bSelect;        // �I�����Ă��邩
    public int maxOutNum = 5;   // �����ɊO�ɏo��钇�Ԃ̃P���R�̐�

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
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ���X�g�ɒǉ�
    public bool Add(Unit unit)
    {
        if (outUnitList.Count < maxOutNum)
        {
            GameObject obj = Instantiate(unit.sta.sprite, new Vector3(-1000, 0, 0), Quaternion.identity);
            obj.transform.SetParent(transform);
            UnitImage image = obj.GetComponent<UnitImage>();
            image.bOut = true;
            image.id = unit.id;

            outUnitList.Add(unit);
            UnitTagAbility.instance.addTag(unit);
            outImageList.Add(image);
            return true;
        }
        else
            return false;
    }
    // �V���b�v�Ŕ��������j�b�g��ǉ�
    public bool SelectAdd(Unit unit)
    {
        if (outUnitList.Count < maxOutNum)
        {
            GameObject obj = Instantiate(unit.sta.sprite, new Vector3(-1000, 0, 0), Quaternion.identity);
            obj.transform.SetParent(transform);
            UnitImage image = obj.GetComponent<UnitImage>();
            image.bOut = true;
            image.id = unit.id;

            unit.gameObject.SetActive(true);
            unit.SetHex(Map.instance.GetVillageHex());

            outUnitList.Add(unit);
            UnitTagAbility.instance.addTag(unit);
            outImageList.Add(image);
            return true;
        }
        else
            return false;
    }

    // �����������j�b�g�����X�g����폜
    public void RemoveOutList()
    {
        for(int i = 0; i < outUnitList.Count; i++)
        {
            if (outUnitList[i].id == selectId)
            {
                Unit unit = outUnitList[i];
                outUnitList.RemoveAt(i);
                UnitTagAbility.instance.deleteTag(unit);
            }
        }
        for (int i = 0; i < outImageList.Count; i++)
        {
            if (outImageList[i].id == selectId)
            {
                Destroy(outImageList[i].gameObject);
                outImageList.RemoveAt(i);
            }
        }
    }

    // �������郆�j�b�g��I��
    public void SelectOutUnit(int id)
    {
        selectId = id;
        bSelect = true;

        if (KemokoListVillage.instance.bSelect)
        {
            KemokoListVillage.instance.Add(GetSelectOutUnit());

            RemoveOutList();
            SelectAdd(KemokoListVillage.instance.GetSelectVillageUnit());

            KemokoListVillage.instance.RemoveVillageList();

            bSelect = false;
            KemokoListVillage.instance.bSelect = false;

            GameManager.instance.nowTurn++;
        }
    }

    // �ړ��\���̍��v���擾
    public int GetMoveNumTotal()
    {
        int num = 0;
        for(int i = 0; i < outUnitList.Count; i++)
        {
            num += outUnitList[i].sta.moveNum;
        }
        return num;
    }

    // �����ɑI���������j�b�g���擾
    public Unit GetSelectOutUnit()
    {
        for (int i = 0; i < outUnitList.Count; i++)
        {
            if (outUnitList[i].id == selectId)
            {
                return outUnitList[i];
            }
        }

        return null;
    }

    // �O�ɂ���P���R��^�񒆂̑��}�X�ɏW�߂�
    public void SetVillageHex()
    {
        for (int i = 0; i < outUnitList.Count; i++)
        {
            outUnitList[i].SetHex(Map.instance.GetVillageHex());
        }
    }

    // ���H�Ƃ̃^�O�\�̓t���O�Z�b�g
    public void SetGourmet()
    {
        for (int i = 0; i < outUnitList.Count; i++)
        {
            if (outUnitList[i].bGourmetTag)
                outUnitList[i].bGourmetSeason = true;
        }
    }

    // �Y�_���̃^�O�\�̓t���O�Z�b�g
    public void SetCarbonated()
    {
        for (int i = 0; i < outUnitList.Count; i++)
        {
            if (outUnitList[i].bCarbonatedTag)
                outUnitList[i].bCarbonatedSeason = true;
        }
    }

    // �Q��q�͈�̃^�O�\�̓t���O�Z�b�g
    public void SetSleep()
    {
        if (UnitTagAbility.instance.GetSleep() > 0)
        {
            bool b;
            int num = Random.Range(0, 2);
            if (num == 0)
                b = true;
            else
                b = false;
            for (int i = 0; i < outUnitList.Count; i++)
            {
                if (outUnitList[i].bSleepTag)
                {
                    outUnitList[i].bSleepSeason = b;
                    b = !b;
                }
            }
        }
    }
}
