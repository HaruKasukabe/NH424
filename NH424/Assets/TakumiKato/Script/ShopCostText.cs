using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCostText : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "" + Map.instance.GetUnit(GetComponentInParent<UnitImage>().number).GetComponent<Unit>().sta.cost * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
