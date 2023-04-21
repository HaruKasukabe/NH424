using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageCollision : MonoBehaviour
{
    Hex hex;
    int level;
    bool b = false;

    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.instance.level;
        hex = GetComponent<Hex>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!b)
        {
            if (level < GameManager.instance.maxVillageLevel)
            {
                if (level != GameManager.instance.level)
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

                            Map.instance.map[num.x, num.z] = obj;
                        }
                    }

                    b = true;
                }
            }
        }
    }

    //GameObject GetChildTag(Transform obj, string tag)
    //{
    //    for (var i = 0; i < obj.childCount; ++i)
    //    {
    //        Transform child = obj.GetChild(i);
    //        if (child.gameObject.CompareTag(tag))
    //        {
    //            return child.gameObject;
    //        }
    //    }

    //    return null;
    //}
}