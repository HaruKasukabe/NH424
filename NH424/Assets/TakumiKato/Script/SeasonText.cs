using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonText : MonoBehaviour
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
        text.text = GameManager.instance.season + " " + GameManager.instance.nowTurn + "/" + GameManager.instance.seasonTurnNum;
    }
}