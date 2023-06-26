//=============================================================================
//
// 村にいるケモコリスト クラス [KemokoListVillage.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KemokoListVillage : MonoBehaviour
{
    public static KemokoListVillage instance = null;
    public List<Unit> villageUnitList = new List<Unit>();               // 村のユニットリスト
    public List<UnitImage> villageImageList = new List<UnitImage>();    // 村のユニットのイメージリスト
    int selectId;           // 交換するユニットのId
    public bool bSelect;    // 選択してあるか

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

    // リストに追加
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

    // 交換したユニットをリストから削除
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

    // 交換するユニットを選択
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

    // 交換に選択したユニットを取得
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


