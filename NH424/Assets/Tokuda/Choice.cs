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
    public Button button;
    public Button pictoButton;

    // Start is called before the first frame update
    void Start()
    {
        //�{�^�����I�����ꂽ��ԂɂȂ�
        button.Select();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void select()
    {
        button.Select();
    }
    public void selectPicto()
    {
        pictoButton.Select();
    }
}
