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
        text.text = "ñÿçﬁÅF" + seasonMaterial + " à»è„" + " êŒçﬁÅF" + seasonMaterial + " à»è„" + " ìSçﬁÅF" + seasonMaterial + " à»è„";
    }
}
