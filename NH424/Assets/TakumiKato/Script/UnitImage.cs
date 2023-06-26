//=============================================================================
//
// ユニットのイメージ画像 クラス [UnitImage.cpp]
//
//=============================================================================
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
            // 価格テキストと背景を表示
            for(int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            // まだ仲間にしたことのない種類の時
            if(!GameManager.instance.bFriendCat(number))
                transform.GetChild(3).gameObject.SetActive(true);   // ビックリマークを表示

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
