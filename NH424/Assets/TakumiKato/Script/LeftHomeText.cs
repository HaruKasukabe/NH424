//=============================================================================
//
// �����j���[�̍����̉Ƃ̃e�L�X�g �N���X [LeftHomeText.cpp]
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
        text.text = "���݂�Lv" + GameManager.instance.level + "\n�ؐΓS�K�v�f�ޗʁF" + GameManager.instance.levelUpNeed;
    }
}
