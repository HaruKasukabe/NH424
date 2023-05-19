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
    public enum GAMEBGM
    {
        Spring = 0,
        Summer,
        Fall,
        Winter,
        Conversation,
        PictorialBook,
        Shop,
        Village
    }
    public enum GAMESE
    {
        Back = 0,
        Click,
        GameStart,
        Select,
        Start,
    }

    // �ϐ��錾
    // �A�N�Z�X�C���q����
    // public           :���N���X����Q�Ɖ\�AUnityEditor��ŕҏW�\
    // private          :���N���X����Q�ƕs�AUnityEditor��ŕҏW�s��
    // [SerializeField] :���N���X����Q�ƕs�AUnityEditor��ŕҏW�\
    public static ManagementAudio instance = null;
    public GAMEBGM GameBGM;    // �Q�[��BGM�񋓑�
    private GAMEBGM nowSeason; // ���݂̋G�ߕۊǗp�ϐ�
    public GAMESE gameSE;      // �Q�[��SE�񋓑�
    private int fadeBGM; // BGM�t�F�[�h�p
    private float FadeSecond = 4.0f; // �t�F�[�h�ɂ����鎞��
    private float NowBGMVolume; // ���݂�Volume
    private string NowBGMName;
    private bool UseUIflg;
    private bool Closeflg;
    [SerializeField] AudioSource ASBGM; // BGM1�̃I�[�f�B�I�\�[�X
    [SerializeField] AudioSource ASSE; // SE�̃I�[�f�B�I�\�[�X

    private List<AudioClip> ACGameBGM = new List<AudioClip>(); // �Q�[��BGM�i�[List
    private List<AudioClip> ACGameSE = new List<AudioClip>();  // �Q�[��SE�i�[List

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Initialize
    void Start()
    {
        ASBGM.loop = true;

        ASSE.loop = false;
        UseUIflg = false;
        Closeflg = false;
        // BGM�N���b�v�����X�g�ɂ܂Ƃ߂鏉�����֐�
        InitBGMClip();
        InitSEClip();

        fadeBGM = 0; // 0:�����l 1:�t�F�[�h�A�E�g 2:�t�F�[�h�C��
        NowBGMVolume = 0.2f; // 1.0��100%
        ASBGM.volume = NowBGMVolume; // �f�t�H���g�̉��ʂ��w��
        GameBGM = 0; // �����l���t
        FadeSecond = FadeSecond * 60; // �t���[�����[�g�ɒ������ߕb��*60fps
        ASSE.volume = 0.2f;
        StartBGM();
    }

    // �X�V����
    void Update()
    {

        // �f�o�b�O����
        if (Input.GetKeyDown(KeyCode.A)&&!UseUIflg)
        {
            OpenUIBGM("Conversation");
            Debug.Log("��b�Đ�");
        }
        else if(Input.GetKeyDown(KeyCode.S)&&!UseUIflg)
        {
            OpenUIBGM("PictorialBook");
        }
        else if(Input.GetKeyDown(KeyCode.D)&&!UseUIflg)
        {
            OpenUIBGM("Shop");
        }
        else if(Input.GetKeyDown(KeyCode.F)&&!UseUIflg)
        {
            OpenUIBGM("Village");
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&UseUIflg)
        {
            CloseUIBGM();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextSeason();
            Debug.Log("���̋Ȃ�");
        }


        // BGM�؂�ւ����t�F�[�h�֐�
        // �t�F�[�h�A�E�g
        if (fadeBGM == 1)
        {
            ASBGM.volume -= NowBGMVolume / FadeSecond;
            if (ASBGM.volume <= 0.0f)
            {
                ASBGM.Stop();
                if (!UseUIflg)
                {
                    NextSeasonBGM();
                    Debug.Log("�G�ߋȍĐ�");
                }
                else if(UseUIflg)
                {
                    PlayUIBGM();
                    Debug.Log("UI�ȍĐ�");
                }
                fadeBGM = 2;
            }
        }
        else if (fadeBGM == 2)
        {
            ASBGM.volume += NowBGMVolume / FadeSecond;
            if(ASBGM.volume >= 60.0f)
            {
                fadeBGM = 0;
            }
        }

    }

    // =============================
    // BGM�ESE�������֐�
    // =============================
    private void InitBGMClip()
    {
        AudioClip ACtemp;
        ACtemp = (AudioClip)Resources.Load("BGM/Spring");
        ACGameBGM.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("BGM/Summer");
        ACGameBGM.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("BGM/Fall");
        ACGameBGM.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("BGM/Winter");
        ACGameBGM.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("BGM/Conversation");
        ACGameBGM.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("BGM/PictorialBook");
        ACGameBGM.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("BGM/Shop");
        ACGameBGM.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("BGM/Village");
        ACGameBGM.Add(ACtemp);
    }
    private void InitSEClip()
    {
        AudioClip ACtemp;
        ACtemp = (AudioClip)Resources.Load("SE/Back");
        ACGameSE.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("SE/Click");
        ACGameSE.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("SE/GameStart");
        ACGameSE.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("SE/Select");
        ACGameSE.Add(ACtemp);
        ACtemp = (AudioClip)Resources.Load("SE/Start");
        ACGameSE.Add(ACtemp);
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
    private void PlaySE(AudioClip SEclip)
    {
        ASSE.PlayOneShot(SEclip);
    }
    // =============================
    // �O������BGM�̍Đ�������֐�
    // =============================
    // �������֐��ŌĂяo���p
    public void StartBGM()
    {
        PlayBGM(ACGameBGM[(int)GAMEBGM.Spring]);
    }

    public void NextSeason()
    {
        fadeBGM = 1;
    }
    // ���̋G��BGM
    private void NextSeasonBGM()
    {
        if (!Closeflg)
        {
            GameBGM++;
        }
        // �~�Ȃ�t�ɖ߂�
        if (GameBGM > GAMEBGM.Winter)
            GameBGM = GAMEBGM.Spring;
        switch (GameBGM)
        {
            case GAMEBGM.Spring:// �t
                PlayBGM(ACGameBGM[(int)GAMEBGM.Spring]);
                break;
            case GAMEBGM.Summer: // ��
                PlayBGM(ACGameBGM[(int)GAMEBGM.Summer]);
                break;
            case GAMEBGM.Fall: // �H
                PlayBGM(ACGameBGM[(int)GAMEBGM.Fall]);
                break;
            case GAMEBGM.Winter: // �~
                PlayBGM(ACGameBGM[(int)GAMEBGM.Winter]);
                break;
            default:
                break;
        }
        Closeflg = false;
    }
    
    // �O�����瑺�Ȃǂ�BGM�Ăяo��
    public void OpenUIBGM(string PlaceBGM)
    {
        // �t�F�[�h�A�E�g�J�n
        fadeBGM = 1;
        // ����BGM���w��
        nowSeason = GameBGM;
        NowBGMName = PlaceBGM;
        UseUIflg = true;
    }
    public void PlayUIBGM()
    {
        if(NowBGMName == "Conversation")
        {
            PlayBGM(ACGameBGM[(int)GAMEBGM.Conversation]);
        }
        else if (NowBGMName == "PictorialBook")
        {
            PlayBGM(ACGameBGM[(int)GAMEBGM.PictorialBook]);
        }
        else if (NowBGMName == "Shop")
        {
            PlayBGM(ACGameBGM[(int)GAMEBGM.Shop]);
        }
        else if (NowBGMName == "Village")
        {
            PlayBGM(ACGameBGM[(int)GAMEBGM.Village]);
        }
        else
        {
            Debug.Log("�Đ��ł��܂���");
        }

    }
    public void CloseUIBGM()
    {
        fadeBGM = 1;
        UseUIflg = false;
        Closeflg = true;
    }

    // �w���SE���Đ����邽�߂ɌĂяo��
    public void PublicPlaySE(GAMESE se)
    {
        PlaySE(ACGameSE[(int)se]);
    }
}
