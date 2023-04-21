using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KemokoListOut : MonoBehaviour
{
    public static KemokoListOut instance = null;
    public List<Unit> outUnitList = new List<Unit>();
    public List<UnitImage> outImageList = new List<UnitImage>();
    public int selectId;
    public bool bSelect;
    public int maxOutNum = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
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
    public int GetMoveNumTotal()
    {
        int num = 0;
        for(int i = 0; i < outUnitList.Count; i++)
        {
            num += outUnitList[i].sta.moveNum;
        }
        return num;
    }
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
}
