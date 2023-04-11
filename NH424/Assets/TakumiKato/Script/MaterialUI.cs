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
        text.text = "�؍ށF" + GameManager.instance.wood + "\n�΍ށF" + GameManager.instance.stone + "\n�S�ށF" + GameManager.instance.iron
                    + "\n\n�H�ށF" + GameManager.instance.food.ToString("f0");
    }
}
