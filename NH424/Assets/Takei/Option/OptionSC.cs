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

public class OptionSC : MonoBehaviour
{
    // �ϐ��錾
    private float Camerasensitivity; // �J�������x�ۑ��֐�
    private bool OpenMOption; // ���C���̃I�v�V�����̉�ʂ��J����Ă��邩
    private bool OpenCOption; // �J�����̃I�v�V������ʂ��J����Ă��邩
    private bool OpenSOption; // �T�E���h�̃I�v�V������ʂ��J����Ă��邩
    private bool OpenOption; // �I�v�V������ʂ��J����Ă��邩
    public GameObject ImageMOption; // ���C����Option��UI�摜
    public GameObject ImageCOption; // �J������Option��UI�摜
    public GameObject ImageSOption; // �T�E���h��Option��UI�摜
    public AudioSource ASbgm; // BGM�̃I�[�f�B�I�\�[�X
    public AudioSource ASSE; // SE�̃I�[�f�B�I�\�[�X
    [SerializeField]AudioClip BGM1; // BGM�̃N���b�v(�\�[�X)������Ȃ�ʃX�N���v�g�ŊǗ��\��


    // Initialize
    void Start()
    {
        OpenMOption = false;
        OpenCOption = false;
        OpenSOption = false;
        OpenOption = false;
        ImageMOption.SetActive(OpenMOption); // �ŏ��͔�\���̂���false
        ASbgm = GetComponent<AudioSource>();
        ASbgm.loop = true;
        ASSE = GetComponent<AudioSource>();
        ASSE.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h��M�ŃI�v�V�������J��
        if (Input.GetKeyDown(KeyCode.M))
        {
            // �I�v�V������ʂ��J����Ă����Ȃ�
            if(!OpenOption)
            {
                OpenOption = true;
                // ���C���̃I�v�V������ʊJ��
                OpenMOption = true;
                ImageMOption.SetActive(OpenMOption);
            }
            else if(OpenOption)
            {
                // �S�ẴI�v�V������ʂ��I�t�ɂ���
                OpenOption = false;
                OpenMOption = false;
                OpenCOption = false;
                OpenSOption = false;
                ImageMOption.SetActive(OpenMOption);
                ImageCOption.SetActive(OpenCOption);
                ImageSOption.SetActive(OpenSOption);
            }
            
        }
        // �f�o�b�O�p�C���v�b�g
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayBGM(BGM1);
        }
    }

    // ==========================
    // �{�^�������֐�
    // ==========================
    // �Q�[���ɖ߂���������Ƃ�
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
    }

    // =============================
    // �J�����̃X���C�_�[�����֐�
    // =============================
    // �X���C�_�[�̒l�����C���Ǘ��N���X�ŕۑ�����
    public void SetCameraSensitivity(float sensitivity)
    {
        // �J�������x��ۑ�����
         Camerasensitivity =  sensitivity;
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
        ASbgm.volume = volume;
    }
    public void SetSEVolume(float volume)
    {
        ASSE.volume = volume;
    }

    // =============================
    // �T�E���h�̍Đ������֐�
    // =============================
    // BGM�̍Đ��Ǘ�
    public void PlayBGM(AudioClip BGMclip)
    {
        // Play���ƈ����w��ł��Ȃ��̂�PlayOneShot���g�p
        ASbgm.PlayOneShot(BGMclip);
    }
    // SE�̍Đ��Ǘ�
    public void PlaySE(AudioClip SEclip)
    {
        ASSE.PlayOneShot(SEclip);
    }

}
