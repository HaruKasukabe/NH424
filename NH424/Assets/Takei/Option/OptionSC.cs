// ================================================
//  OptionSC.cs[���C���̃I�v�V�����Ǘ�]
// 
// Author:����y�s
//=================================================
// �ύX����
// 2023/02/27 �X�N���v�g�쐬�E�T�E���h�֘A�E
// �J�������x�E�I�v�V������ʍ쐬
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionSC : MonoBehaviour
{
    // �X�N���v�g�Q��
    public ManagementAudio m_audiosc; // �I�[�f�B�I�Ǘ��X�N���v�g

    // �ϐ��錾
    private float Camerasensitivity; // �J�������x�ۑ��֐�
    private bool OpenMOption; // ���C���̃I�v�V�����̉�ʂ��J����Ă��邩
    private bool OpenCOption; // �J�����̃I�v�V������ʂ��J����Ă��邩
    private bool OpenSOption; // �T�E���h�̃I�v�V������ʂ��J����Ă��邩
    private bool OpenPictorialBook;
    private bool OpenOption; // �I�v�V������ʂ��J����Ă��邩
    public GameObject ImageMOption; // ���C����Option��UI�摜
    public GameObject ImageCOption; // �J������Option��UI�摜
    public GameObject ImageSOption; // �T�E���h��Option��UI�摜
    public PictorialBook PictorialBook;

    [SerializeField] GameCamera cam;
    [SerializeField] PadSlider padSlider;
    [SerializeField] Button MOptionButton;

    // Initialize
    void Start()
    {
        OpenMOption = false;
        OpenCOption = false;
        OpenSOption = false;
        OpenPictorialBook = false;
        OpenOption = false;
        ImageMOption.SetActive(OpenMOption); // �ŏ��͔�\���̂���fals
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h��M�ŃI�v�V�������J��
        if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("Option"))
        {
            SetOption();
        }
        if (Input.GetButtonDown("Fire2") && OpenOption)
        {
            //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
            // �S�ẴI�v�V������ʂ��I�t�ɂ���
            OpenOption = false;
            OpenMOption = false;
            OpenCOption = false;
            OpenSOption = false;
            OpenPictorialBook = true;
            ImageMOption.SetActive(OpenMOption);
            ImageCOption.SetActive(OpenCOption);
            ImageSOption.SetActive(OpenSOption);
            NoPictorialBook();
            padSlider.nullSlider();
        }
    }

    // ==========================
    // �{�^�������֐�
    // ==========================
    // �Q�[���ɖ߂���������Ƃ�
    public void SetOption()
    {
        // �I�v�V������ʂ��J����Ă����Ȃ�
        if (!OpenOption)
        {
            OpenOption = true;
            // ���C���̃I�v�V������ʊJ��
            OpenMOption = true;
            ImageMOption.SetActive(OpenMOption);
            MOptionButton.Select();
        }
        else if (OpenOption)
        {
            // �S�ẴI�v�V������ʂ��I�t�ɂ���
            OpenOption = false;
            OpenMOption = false;
            OpenCOption = false;
            OpenSOption = false;
            OpenPictorialBook = true;
            ImageMOption.SetActive(OpenMOption);
            ImageCOption.SetActive(OpenCOption);
            ImageSOption.SetActive(OpenSOption);
            NoPictorialBook();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public void SetActiveFlg()
    {
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
    }
    // �J�����ݒ���������Ƃ�
    public void SetCameraSetting()
    {
        // �܂��̓��C���̃I�v�V������ʂ����
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
        OpenCOption = !OpenCOption;
        ImageCOption.SetActive(OpenCOption);
        if (OpenCOption)
            padSlider.SetCameraSlider();
        else
        {
            padSlider.nullSlider();
            MOptionButton.Select();
        }
    }
    // �T�E���h�ݒ�֘A���������Ƃ�
    public void SetSoundSetting()
    {
        // �܂��̓��C���̃I�v�V������ʂ𔽓]
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
        // �T�E���h�ݒ�̉�ʂ𔽓]
        OpenSOption = !OpenSOption;
        ImageSOption.SetActive(OpenSOption);
        if (OpenSOption)
            padSlider.SetSoundSlider();
        else
        {
            padSlider.nullSlider();
            MOptionButton.Select();
        }
    }
    
    public void SetPictorialBook()
    {
        // �܂��̓��C���̃I�v�V������ʂ𔽓]
        OpenMOption = !OpenMOption;
        ImageMOption.SetActive(OpenMOption);
        OpenPictorialBook = !OpenPictorialBook;
        if (OpenPictorialBook)
        {
            PictorialBook.SetDisplay();
        }
        else
        {
            PictorialBook.BacktoMapButton();
            MOptionButton.Select();
        }
    }
    void NoPictorialBook()
    {
        OpenPictorialBook = !OpenPictorialBook;
        if (OpenPictorialBook)
        {
            PictorialBook.SetDisplay();
        }
        else
            PictorialBook.BacktoMapButton();
    }

    // =============================
    // �J�����̃X���C�_�[�����֐�
    // =============================
    // �X���C�_�[�̒l�����C���Ǘ��N���X�ŕۑ�����
    public void SetCameraSensitivity(float sensitivity)
    {
        // �J�������x��ۑ�����
        cam.sensitiveMovePad =  sensitivity;
    }
    // �J��������֐��ŌĂяo���p
    public float ReturnCameraSensitivity()
    {
        return Camerasensitivity;
    }
    
    // =============================
    // �T�E���h�̃X���C�_�[�����֐�
    // =============================
    public void SetBGMVolume(float volume)
    {
        m_audiosc.SetBGMVolume(volume);
    }
    public void SetSEVolume(float volume)
    {
        m_audiosc.SetSEVolume(volume);
    }

    public bool bOpenOption()
    {
        return OpenOption;
    }
}
