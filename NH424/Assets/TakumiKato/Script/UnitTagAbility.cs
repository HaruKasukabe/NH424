using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct tagData
{
    public int num;
    public List<UnitData> unitData;
    public bool bAbility;
}

public class UnitTagAbility : MonoBehaviour
{
    public static UnitTagAbility instance = null;

    public tagData[] tagDatas = new tagData[(int)UnitTag.MAX];
    public int[] numConditions;

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

    void SetAbility(List<UnitData> unitDatas, UnitTag tag, bool b)
    {
        switch(tag)
        {
            case UnitTag.éÂêlåˆ:
                if (b)
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveNum += 1;
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
        }
    }
    bool GetCondition(UnitTag tag)
    {
        switch (tag)
        {
            case UnitTag.éÂêlåˆ:
                if (KemokoListOut.instance.outUnitList.Count <= numConditions[(int)tag])
                    return true;
                else
                    return false;

            default:
                return false;
        }
    }
}
