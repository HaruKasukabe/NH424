using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialUI : MonoBehaviour
{
    TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "季節：" + GameManager.instance.season + "\nターン：" + GameManager.instance.nowTurn + "/" + GameManager.instance.seasonTurnNum
                    + "\nレベル：" + GameManager.instance.level
                    + "\n食材：" + GameManager.instance.food + "\n木材：" + GameManager.instance.wood 
                    + "\n石材：" + GameManager.instance.stone + "\n鉄材：" + GameManager.instance.iron;
    }
}
