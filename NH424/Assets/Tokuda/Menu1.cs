// ================================================
// MenuSC.cs[���j���[��ʂ̊Ǘ�]
// 
// Author:���c��
//=================================================
// �쐬����
// 2023/05/02 
//=================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu1 : MonoBehaviour
{

    //�ϐ��錾
    private bool OpenMMenu; //���C���̃��j���[�̉�ʂ��J����Ă��邩
    private bool OpenOMenu; //���j���[�̃I�v�V������ʂ��J����Ă��邩
    private bool OpenSMenu; //���j���[�̑����ʂ��J����Ă��邩
    private bool OpenZMenu; //���j���[�̐}�Ӊ�ʂ��J����Ă��邩
    private bool OpenMenu;  //���j���[��ʂ��J����Ă��邩
    public GameObject ImageMMenu;
    public GameObject ImageOMenu;
    public GameObject ImageSMenu;
    public GameObject ImageZMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        OpenMMenu = false;
        OpenOMenu = false;
        OpenSMenu = false;
        OpenMenu = false;
        ImageMMenu.SetActive(OpenMMenu);  //�ŏ��͔�\���̂���false
    }

    // Update is called once per frame
    void Update()
    {
        //�L�[�{�[�h��L�Ń��j���[���J��
        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Menu"))
        {
            //���j���[��ʂ��J����Ă��Ȃ�
            if(!OpenMenu)
            {
                OpenMenu = true;
                //���j���[��ʂ��J��
                OpenMMenu = true;
                ImageMMenu.SetActive(OpenMMenu);

                GameManager.instance.SetUICursol(true);
            }
        }
    }

    // ==========================
    // �{�^�������֐�
    // ==========================
    // �Q�[���ɖ߂���������Ƃ�
    public void SetMenu()
    {
        //���j���[��ʂ��J����Ă��Ȃ�
        if(!OpenMenu)
        {
            OpenMenu = true;
            //���j���[��ʂ��J��
            OpenMMenu = true;
            ImageMMenu.SetActive(OpenMMenu);
        }
        else if (OpenMenu)
        {
            //�S�Ẵ��j���[��ʂ��I�t�ɂ���
            OpenMenu = false;
            OpenMMenu = false;
            OpenOMenu = false;
            OpenSMenu = false;
            OpenZMenu = false;
            ImageMMenu.SetActive(OpenMMenu);
            ImageOMenu.SetActive(OpenOMenu);
            ImageSMenu.SetActive(OpenSMenu);
            ImageZMenu.SetActive(OpenZMenu);
        }
    }
    public void SetActiveFlg()
    {
        OpenMMenu = !OpenMMenu;
        ImageMMenu.SetActive(OpenMMenu);
    }

    //�I�v�V�������������Ƃ�
    public void SetOptionSetting()
    {
        //�܂��̓��j���[��ʂ����
        OpenMMenu = !OpenMMenu;
        ImageMMenu.SetActive(OpenMMenu);
        OpenOMenu = !OpenOMenu;
        ImageOMenu.SetActive(OpenOMenu);
    }
   
    //������@���������Ƃ�
    public void SetSousaSetting()
    {
        //�܂��̓��j���[��ʂ����
        OpenMMenu = !OpenMMenu;
        ImageMMenu.SetActive(OpenMMenu);
        OpenSMenu = !OpenSMenu;
        ImageSMenu.SetActive(OpenSMenu);
    }
}
