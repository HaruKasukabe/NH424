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
        text.text = "�G�߁F" + GameManager.instance.season + "\n�^�[���F" + GameManager.instance.nowTurn + "/" + GameManager.instance.seasonTurnNum
                    + "\n���x���F" + GameManager.instance.level
                    + "\n�H�ށF" + GameManager.instance.food + "\n�؍ށF" + GameManager.instance.wood 
                    + "\n�΍ށF" + GameManager.instance.stone + "\n�S�ށF" + GameManager.instance.iron;
    }
}
