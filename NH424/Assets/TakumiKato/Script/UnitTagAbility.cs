//=============================================================================
//
// ユニットのタグ効果 クラス [UnitTagAbility.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タグ情報
public struct tagData
{
    public int num;
    public List<UnitData> unitData;
    public bool bAbility;
}

public class UnitTagAbility : MonoBehaviour
{
    public static UnitTagAbility instance = null;

    public tagData[] tagDatas = new tagData[(int)UnitTag.MAX];  // タグの種類分のタグ情報配列
    public int[] numConditions; // 能力発動条件数

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
        // タグ情報を初期化
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

    // 仲間になったユニットのタグ情報を追加
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
    // いなくなったユニットのタグを削除
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

    // 能力　true : 発動、false : 元に戻す
    void SetAbility(List<UnitData> unitDatas, UnitTag tag, bool b)
    {
        switch(tag)
        {
            case UnitTag.主人公:
                if (b)
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveNum += 1;  // 移動可能数１回増加
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
            case UnitTag.インターネット海を泳ぐ:
                if (b)
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveNum += 1;  // 移動可能数１回増加
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
            case UnitTag.最年長組:
                if (b && mostOld)
                {
                    GameManager.instance.nowTurn -= 3;
                    mostOld = false;
                }
                break;
            case UnitTag.ちっちゃいもんクラブ:
                if (b)
                {
                    for (int i = 0; i < unitDatas.Count; i++)
                    {
                        unitDatas[i].moveLong += 1;  // 移動可能数１回増加
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
            case UnitTag.バンド:
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
            case UnitTag.木陰で一休み:
                if (b && woodShadow)
                {
                    GameManager.instance.nowTurn -= 3;
                    woodShadow = false;
                }
                break;
        }
    }

    // 能力発動条件チェック
    bool GetCondition(UnitTag tag)
    {
        switch (tag)
        {
            case UnitTag.主人公:
                if (KemokoListOut.instance.outUnitList.Count <= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.インターネット海を泳ぐ:
                if (KemokoListOut.instance.outUnitList.Count <= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.最年長組:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.ちっちゃいもんクラブ:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.バンド:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            case UnitTag.木陰で一休み:
                if (tagDatas[(int)tag].num >= numConditions[(int)tag])
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    // 村のアイドル数取得
    public int GetVillageIdol()
    {
        int num = tagDatas[(int)UnitTag.村のアイドル].num;
        if (num > 1)
            return num;
        else
            return 0;
    }

    // 寝る子は育つ数取得
    public int GetSleep()
    {
        int num = tagDatas[(int)UnitTag.寝る子は育つ].num;
        if (GameManager.instance.IsEven(num))
            return num;
        else
            return 0;
    }
}
