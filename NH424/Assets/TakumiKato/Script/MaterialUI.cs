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
        text.text = "" + GameManager.instance.wood + "\n" + GameManager.instance.stone + "\n" + GameManager.instance.iron
                    + "\n" + GameManager.instance.food.ToString("f0");
    }
}
