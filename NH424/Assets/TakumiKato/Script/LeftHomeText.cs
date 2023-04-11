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
        text.text = "åªç›ÇÃLv" + GameManager.instance.level + "\nñÿêŒìSïKóvëfçﬁó ÅF" + GameManager.instance.levelUpNeed;
    }
}
