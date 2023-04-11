// ================================================
//  ManagementAudio.cs[�I�[�f�B�I�Ǘ�]
// 
// Author:����y�s
//=================================================
// �ύX����
// 2023/03/04 �I�[�f�B�I�Ǘ��\�[�X�쐬
// 2023/04/08 BGM�Đ��@�\����
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ManagementAudio : MonoBehaviour
{
    // �񋓑̐錾
    public enum SEASONBGM
    {
        Spring = 0,
        Summer,
        Fall,
        Winter
    }

    // �ϐ��錾
    public SEASONBGM SeasonBGM; // ���݂̋G��BGM
    private int fadeBGM; // BGM�t�F�[�h�p
    public float FadeSecond = 2.0f; // �t�F�[�h�ɂ����鎞��
    private float NowBGMVolume; // ���݂�Volume
    public AudioSource ASBGM; // BGM1�̃I�[�f�B�I�\�[�X
    public AudioSource ASSE; // SE�̃I�[�f�B�I�\�[�X
    [SerializeField] AudioClip ACBGMSpring; // BGM�̃N���b�v(�\�[�X)������Ȃ�ʃX�N���v�g�ŊǗ��\��
    [SerializeField] AudioClip ACBGMSummer; // BGM�̃N���b�v(�\�[�X)������Ȃ�ʃX�N���v�g�ŊǗ��\��
    [SerializeField] AudioClip ACBGMFall; // BGM�̃N���b�v(�\�[�X)������Ȃ�ʃX�N���v�g�ŊǗ��\��
    [SerializeField] AudioClip ACBGMWinter; // BGM�̃N���b�v(�\�[�X)������Ȃ�ʃX�N���v�g�ŊǗ��\��

    // Initialize
    void Start()
    {
        ASBGM = GetComponent<AudioSource>();
        ASBGM.loop = true;
        ASSE = GetComponent<AudioSource>();
        ASSE.loop = false;

        // Resources.Load�Ŏw�肷��ꍇ�g���q�͏ȗ�
        ACBGMSpring = (AudioClip)Resources.Load("BGM/Spring");
        ACBGMSummer = (AudioClip)Resources.Load("BGM/Summer");
        ACBGMFall = (AudioClip)Resources.Load("BGM/Fall");
        ACBGMWinter = (AudioClip)Resources.Load("BGM/Winter");

        fadeBGM = 0; // 0:�����l 1:�t�F�[�h�A�E�g 2:�t�F�[�h�C��
        NowBGMVolume = 0.6f; // 1.0��100%
        ASBGM.volume = NowBGMVolume; // �f�t�H���g�̉��ʂ��w��
        SeasonBGM = 0; // �����l���t
        FadeSecond = FadeSecond * 60; // �t���[�����[�g�ɒ������ߕb��*60fps
    }

    // �X�V����
    void Update()
    {
        // BGM�؂�ւ����t�F�[�h�֐�
        // �t�F�[�h�A�E�g
        if (fadeBGM == 1)
        {
            // �w�肵���b�������ăt�F�[�h�A�E�g����
            ASBGM.volume -= NowBGMVolume / FadeSecond;
            // �t�F�[�h�A�E�g�����������玟�̋Ȃ�
            if (ASBGM.volume <= 0.0f)
            {
                // BGM���Đ���~
                ASBGM.Stop();
                // ���̋Ȃ��Đ��J�n �������͗񋓑̂�switch��
                switch(SeasonBGM)
                {
                    case SEASONBGM.Spring:// �t
                        PlayBGM(ACBGMSpring);
                        break;
                    case SEASONBGM.Summer: // ��
                        PlayBGM(ACBGMSummer);
                        break;
                    case SEASONBGM.Fall: // �H
                        PlayBGM(ACBGMFall);
                        break;
                    case SEASONBGM.Winter: // �~
                        PlayBGM(ACBGMWinter);
                        break;
                    default:
                        break;
                }
                // �t�F�[�h�C�������J�n
                fadeBGM = 2;
            }

        }
        // �t�F�[�h�C��
        else if (fadeBGM == 2)
        {
            // �w�肵���b���ŉ��ʂ��グ��
            ASBGM.volume += NowBGMVolume / FadeSecond;
            if (ASBGM.volume >= NowBGMVolume)
            {
                // �t�F�[�h�Ǘ�Num�������l�ɖ߂�
                fadeBGM = 0;
            }
        }
        
    }

    // =============================
    // ���ʒ��ߊǗ��֐�
    // =============================
    // BGM�̉���
    public void SetBGMVolume(float volume)
    {
        NowBGMVolume = volume;
        ASBGM.volume = NowBGMVolume;

    }
    // SE�̉���
    public void SetSEVolume(float volume)
    {
        ASSE.volume = volume;
    }

    // =============================
    // �T�E���h�̍Đ������֐�
    // =============================
    // BGM�̍Đ��Ǘ�
    private void PlayBGM(AudioClip BGMclip)
    {

            ASBGM.PlayOneShot(BGMclip);

    }
    // SE�̍Đ��Ǘ�
    public void PlaySE(AudioClip SEclip)
    {
        ASSE.PlayOneShot(SEclip);
    }
    // =============================
    // �O������BGM�̍Đ�������֐�
    // =============================
    // �������֐��ŌĂяo���p
    public void StartBGM()
    {
        PlayBGM(ACBGMSpring);
    }
    // ���̋Ȃ��Đ����邽�߂ɋG�߂̕ς��ڂŌĂяo��
    public void SelectBGM()
    {
        // �t�F�[�h�A�E�g�J�n
        fadeBGM = 1;
        // ���̋Ȃ�
        SeasonBGM++;
        // �~�Ȃ�t�ɖ߂�
        if (SeasonBGM >= SEASONBGM.Winter)
            SeasonBGM = SEASONBGM.Spring;
    }
}
