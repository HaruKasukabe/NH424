using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitImage : MonoBehaviour
{
    public bool bOut;
    public bool bShop = false;
    bool bShopActive = false;
    public int id;
    public int number;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + Map.instance.GetUnit(number).GetComponent<Unit>().sta.cost;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + Map.instance.GetUnit(number).GetComponent<Unit>().sta.motifName;
    }

    // Update is called once per frame
    void Update()
    {
        if(bShop && !bShopActive)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            bShopActive = true;
        }
    }

    public void Button()
    {
        if (bShop)
        {
            ShopList.instance.Add(id, number);
        }
        else
        {
            if (bOut)
                KemokoListOut.instance.SelectOutUnit(id);
            else
                KemokoListVillage.instance.SelectVillageUnit(id);
        }
    }
}
