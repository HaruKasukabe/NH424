//=============================================================================
//
// ���Ԃɂ�����ސ�UI�̃e�L�X�g �N���X [FriendCatNumText.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FriendCatNumText : MonoBehaviour
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
        text.text = "" + GameManager.instance.friendCatList.Count + "/20";  // ���Ԃɂ�����ސ�/�ő��ސ�(20)
    }
}
