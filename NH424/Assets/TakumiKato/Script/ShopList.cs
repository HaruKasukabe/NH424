using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    public static ShopList instance = null;
    List<UnitImage> ImageList = new List<UnitImage>();

    int maxShopNum = 5;

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
        for (int i = 0; i < maxShopNum; i++)
        {
            GameObject obj = Instantiate(Map.instance.RandomGetUnitSprite(), new Vector3(-1000, 0, 0), Quaternion.identity);
            obj.transform.SetParent(transform);
            UnitImage image = obj.GetComponent<UnitImage>();
            image.bShop = true;
            image.id = i;

            ImageList.Add(image);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Add(int id, int number)
    {
        GameObject obj = Instantiate(Map.instance.GetUnit(number));
        obj.transform.rotation = new Quaternion(0, 180, 0, 0);
        Unit unit = obj.GetComponent<Unit>();

        if (unit.sta.cost * 2 <= GameManager.instance.food)
        {
            unit.id = Map.instance.GetUnitId();
            unit.bFriend = true;
            GameManager.instance.AddSelectUnit(unit);

            GameManager.instance.food -= unit.sta.cost * 2;
            RemoveList(id);
        }
        else
        {
            Destroy(obj);
            Destroy(unit);
        }
    }
    void RemoveList(int id)
    {
        for (int i = 0; i < ImageList.Count; i++)
        {
            if (ImageList[i].id == id)
            {
                Destroy(ImageList[i].gameObject);
                ImageList.RemoveAt(i);
            }
        }
    }
    public void ChengeList()
    {
        for (int i = 0; i < ImageList.Count; i++)
            Destroy(ImageList[i].gameObject);
        ImageList.Clear();

        for (int i = 0; i < maxShopNum; i++)
        {
            GameObject obj = Instantiate(Map.instance.RandomGetUnitSprite(), new Vector3(-1000, 0, 0), Quaternion.identity);
            obj.transform.SetParent(transform);
            UnitImage image = obj.GetComponent<UnitImage>();
            image.bShop = true;
            image.id = i;

            ImageList.Add(image);
        }
    }
}
