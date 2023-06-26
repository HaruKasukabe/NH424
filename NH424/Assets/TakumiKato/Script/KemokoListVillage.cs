//=============================================================================
//
// ���ɂ���P���R���X�g �N���X [KemokoListVillage.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KemokoListVillage : MonoBehaviour
{
    public static KemokoListVillage instance = null;
    public List<Unit> villageUnitList = new List<Unit>();               // ���̃��j�b�g���X�g
    public List<UnitImage> villageImageList = new List<UnitImage>();    // ���̃��j�b�g�̃C���[�W���X�g
    int selectId;           // �������郆�j�b�g��Id
    public bool bSelect;    // �I�����Ă��邩

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
    public void Add(Unit unit)
    {
        GameObject obj = Instantiate(unit.sta.sprite, new Vector3(-1000, 0, 0), Quaternion.identity);
        obj.transform.SetParent(transform);
        UnitImage image = obj.GetComponent<UnitImage>();
        image.bOut = false;
        image.id = unit.id;

        villageImageList.Add(image);

        if (unit.Hex)
        {
            unit.Hex.DisUnit();
            unit.OldHex.DisUnit();
        }
        unit.gameObject.SetActive(false);

        villageUnitList.Add(unit);
    }

    // �����������j�b�g�����X�g����폜
    public void RemoveVillageList()
    {
        for (int i = 0; i < villageUnitList.Count; i++)
        {
            if (villageUnitList[i].id == selectId)
            {
                villageUnitList.RemoveAt(i);
            }
        }
        for (int i = 0; i < villageImageList.Count; i++)
        {
            if (villageImageList[i].id == selectId)
            {
                Destroy(villageImageList[i].gameObject);
                villageImageList.RemoveAt(i);
            }
        }
    }

    // �������郆�j�b�g��I��
    public void SelectVillageUnit(int id)
    {
        selectId = id;
        bSelect = true;

        if (KemokoListOut.instance.bSelect)
        {
            Add(KemokoListOut.instance.GetSelectOutUnit());
            KemokoListOut.instance.RemoveOutList();

            KemokoListOut.instance.SelectAdd(GetSelectVillageUnit());
            RemoveVillageList();

            bSelect = false;
            KemokoListOut.instance.bSelect = false;

            GameManager.instance.nowTurn++;
        }
    }

    // �����ɑI���������j�b�g���擾
    public Unit GetSelectVillageUnit()
    {
        for (int i = 0; i < villageUnitList.Count; i++)
        {
            if (villageUnitList[i].id == selectId)
            {
                return villageUnitList[i];
            }
        }

        return null;
    }
    //public void DestroyAll()
    //{
    //    for (int i = 0; i < villageUnitList.Count; i++)
    //    {
    //        Destroy(villageUnitList[i].gameObject);
    //    }
    //    villageUnitList.Clear();
    //}
    //public void SetDontDestroy()
    //{
    //    for (int i = 0; i < villageUnitList.Count; i++)
    //    {
    //        DontDestroyOnLoad(villageUnitList[i].gameObject);
    //    }
    //}
}


