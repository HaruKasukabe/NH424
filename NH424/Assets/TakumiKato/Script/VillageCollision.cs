//=============================================================================
//
//  村マス判定　クラス [VillageCollision.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageCollision : MonoBehaviour
{
    Hex hex;
    int level;
    bool b = false;
    public int villageNum = 0;

    // Start is called before the first frame update
    void Awake()
    {
        level = GameManager.instance.level;     // 生成した時点のレベルを代入
        hex = GetComponent<Hex>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!b)
        {
            // 村最大レベル以下
            if (level < GameManager.instance.maxVillageLevel)
            {
                // レベルが変わった時
                if (level != GameManager.instance.level)
                {
                    SetVillage();
                }
            }
        }
    }

    // 周りを村マスにする
    void SetVillage()
    {
        if (!b)
        {
            for (int i = 0; i < hex.nextNum.Length; i++)
            {
                INT2 num = hex.nextNum[i];
                GameObject obj = Map.instance.map[num.x, num.z];
                if (!obj.CompareTag("Village"))
                {
                    Unit unit = null;
                    Vector3 pos = obj.transform.position;

                    if (obj.GetComponent<Hex>().bUnit)
                        unit = obj.GetComponent<Hex>().Unit;

                    Destroy(obj);

                    obj = Instantiate(this.gameObject, pos, Quaternion.identity);
                    if (unit)
                    {
                        if (unit.bFriend)
                            obj.GetComponent<Hex>().SetUnit(unit);
                        else
                            obj.GetComponent<Hex>().SetStrayUnit(unit);
                    }
                    obj.GetComponent<Hex>().SetHexNum(num);
                    obj.GetComponent<Hex>().SetEnd();

                    Map.instance.map[num.x, num.z] = obj;
                    Map.instance.hexVillageList.Add(obj);
                }
            }

            b = true;
        }
    }

    // マップリセット時の村マス生成
    public void SetVillage(int villageNum)
    {
        if (!b)
        {
            for (int i = 0; i < hex.nextNum.Length; i++)
            {
                INT2 num = hex.nextNum[i];
                GameObject obj = Map.instance.map[num.x, num.z];
                if (!obj.CompareTag("Village"))
                {
                    Unit unit = null;
                    Vector3 pos = obj.transform.position;

                    if (obj.GetComponent<Hex>().bUnit)
                        unit = obj.GetComponent<Hex>().Unit;

                    Destroy(obj);

                    obj = Instantiate(this.gameObject, pos, Quaternion.identity);
                    if (unit)
                    {
                        if (unit.bFriend)
                            obj.GetComponent<Hex>().SetUnit(unit);
                        else
                            obj.GetComponent<Hex>().SetStrayUnit(unit);
                    }
                    obj.GetComponent<Hex>().SetHexNum(num);
                    obj.GetComponent<Hex>().SetEnd();
                    obj.GetComponent<VillageCollision>().villageNum = villageNum;

                    Map.instance.map[num.x, num.z] = obj;
                    Map.instance.hexVillageList.Add(obj);
                }
            }

            b = true;
        }
    }
}