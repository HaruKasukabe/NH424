// ================================================
//  Choice.cs[���C�����j���[�̑I���Ǘ�]
// 
// Author:���c��
//=================================================
// �ύX����
// 2023/05/08 �X�N���v�g�쐬
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Canvas/MainMenu/Button").GetComponent<Button>();
        //�{�^�����I�����ꂽ��ԂɂȂ�
        button.Select();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
