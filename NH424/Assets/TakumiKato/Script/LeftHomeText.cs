//=============================================================================
//
// ºj[Ì¶¤ÌÆÌeLXg NX [LeftHomeText.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftHomeText : MonoBehaviour
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
        text.text = "»ÝÌLv" + GameManager.instance.level + "\nØÎSKvfÞÊF" + GameManager.instance.levelUpNeed;
    }
}
