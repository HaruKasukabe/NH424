//=============================================================================
//
// 外にいるケモコリスト クラス [KemokoListOut.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KemokoListOut : MonoBehaviour
{
    public static KemokoListOut instance = null;
    public List<Unit> outUnitList = new List<Unit>();               // 外のユニットリスト
    public List<UnitImage> outImageList = new List<UnitImage>();    // 外のユニットのイメージリスト
    int selectId;               // 交換するユニットのId
    public bool bSelect;        // 選択してあるか
    public int maxOutNum = 5;   // 同時に外に出れる仲間のケモコの数

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
    // ショップで買ったユニットを追加
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

    // 交換したユニットをリストから削除
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

    // 交換するユニットを選択
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

    // 移動可能数の合計を取得
    public int GetMoveNumTotal()
    {
        int num = 0;
        for(int i = 0; i < outUnitList.Count; i++)
        {
            num += outUnitList[i].sta.moveNum;
        }
        return num;
    }

    // 交換に選択したユニットを取得
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

    // 外にいるケモコを真ん中の村マスに集める
    public void SetVillageHex()
    {
        for (int i = 0; i < outUnitList.Count; i++)
        {
            outUnitList[i].SetHex(Map.instance.GetVillageHex());
        }
    }

    // 美食家のタグ能力フラグセット
    public void SetGourmet()
    {
        for (int i = 0; i < outUnitList.Count; i++)
        {
            if (outUnitList[i].bGourmetTag)
                outUnitList[i].bGourmetSeason = true;
        }
    }

    // 炭酸水のタグ能力フラグセット
    public void SetCarbonated()
    {
        for (int i = 0; i < outUnitList.Count; i++)
        {
            if (outUnitList[i].bCarbonatedTag)
                outUnitList[i].bCarbonatedSeason = true;
        }
    }

    // 寝る子は育つのタグ能力フラグセット
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
