using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RightHomeText : MonoBehaviour
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
        text.text = "Lv" + GameManager.instance.level + 1 + "\n�T���͈�UP\n���X�~�b�V��������";
    }
}
