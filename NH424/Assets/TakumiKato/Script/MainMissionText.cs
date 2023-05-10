using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMissionText : MonoBehaviour
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
        text.text = "レベル " + SeasonMission.instance.level[(int)GameManager.instance.season + GameManager.instance.seasonRoundNum * 4] + " 以上";
    }
}
