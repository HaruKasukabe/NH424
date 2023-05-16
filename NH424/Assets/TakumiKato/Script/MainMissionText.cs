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
        float seasonMaterial = SeasonMission.instance.seasonMaterial;
        text.text = "�؍ށF" + seasonMaterial + " �ȏ�" + " �΍ށF" + seasonMaterial + " �ȏ�" + " �S�ށF" + seasonMaterial + " �ȏ�";
    }
}
