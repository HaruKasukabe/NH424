// ================================================
//  ManagementAudio.cs[オーディオ管理]
// 
// Author:武井遥都
//=================================================
// 変更履歴
// 2023/03/04 オーディオ管理ソース作成
// 2023/04/08 BGM再生機能完成
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ManagementAudio : MonoBehaviour
{
    // 列挙体宣言
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

    // 変数宣言
    // アクセス修飾子メモ
    // public           :他クラスから参照可能、UnityEditor上で編集可能
    // private          :他クラスから参照不可、UnityEditor上で編集不可
    // [SerializeField] :他クラスから参照不可、UnityEditor上で編集可能
    public static ManagementAudio instance = null;
    public GAMEBGM GameBGM;    // ゲームBGM列挙体
    private GAMEBGM nowSeason; // 現在の季節保管用変数
    public GAMESE gameSE;      // ゲームSE列挙体
    private int fadeBGM; // BGMフェード用
    private float FadeSecond = 4.0f; // フェードにかかる時間
    private float NowBGMVolume; // 現在のVolume
    private string NowBGMName;
    private bool UseUIflg;
    private bool Closeflg;
    [SerializeField] AudioSource ASBGM; // BGM1のオーディオソース
    [SerializeField] AudioSource ASSE; // SEのオーディオソース

    private List<AudioClip> ACGameBGM = new List<AudioClip>(); // ゲームBGM格納List
    private List<AudioClip> ACGameSE = new List<AudioClip>();  // ゲームSE格納List

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
        // BGMクリップをリストにまとめる初期化関数
        InitBGMClip();
        InitSEClip();

        fadeBGM = 0; // 0:初期値 1:フェードアウト 2:フェードイン
        NowBGMVolume = 0.2f; // 1.0が100%
        ASBGM.volume = NowBGMVolume; // デフォルトの音量を指定
        GameBGM = 0; // 初期値が春
        FadeSecond = FadeSecond * 60; // フレームレートに直すため秒数*60fps
        ASSE.volume = 0.2f;
        StartBGM();
    }

    // 更新処理
    void Update()
    {

        // デバッグ処理
        if (Input.GetKeyDown(KeyCode.A)&&!UseUIflg)
        {
            OpenUIBGM("Conversation");
            Debug.Log("会話再生");
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
            Debug.Log("次の曲へ");
        }


        // BGM切り替え時フェード関数
        // フェードアウト
        if (fadeBGM == 1)
        {
            ASBGM.volume -= NowBGMVolume / FadeSecond;
            if (ASBGM.volume <= 0.0f)
            {
                ASBGM.Stop();
                if (!UseUIflg)
                {
                    NextSeasonBGM();
                    Debug.Log("季節曲再生");
                }
                else if(UseUIflg)
                {
                    PlayUIBGM();
                    Debug.Log("UI曲再生");
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
    // BGM・SE初期化関数
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
    // 音量調節管理関数
    // =============================
    // BGMの音量
    public void SetBGMVolume(float volume)
    {
        NowBGMVolume = volume;
        ASBGM.volume = NowBGMVolume;
    }
    // SEの音量
    public void SetSEVolume(float volume)
    {
        ASSE.volume = volume;
    }

    // =============================
    // サウンドの再生処理関数
    // =============================
    // BGMの再生管理
    private void PlayBGM(AudioClip BGMclip)
    {
            ASBGM.PlayOneShot(BGMclip);
    }
    // SEの再生管理
    private void PlaySE(AudioClip SEclip)
    {
        ASSE.PlayOneShot(SEclip);
    }
    // =============================
    // 外部からBGMの再生をする関数
    // =============================
    // 初期化関数で呼び出す用
    public void StartBGM()
    {
        PlayBGM(ACGameBGM[(int)GAMEBGM.Spring]);
    }

    public void NextSeason()
    {
        fadeBGM = 1;
    }
    // 次の季節BGM
    private void NextSeasonBGM()
    {
        if (!Closeflg)
        {
            GameBGM++;
        }
        // 冬なら春に戻す
        if (GameBGM > GAMEBGM.Winter)
            GameBGM = GAMEBGM.Spring;
        switch (GameBGM)
        {
            case GAMEBGM.Spring:// 春
                PlayBGM(ACGameBGM[(int)GAMEBGM.Spring]);
                break;
            case GAMEBGM.Summer: // 夏
                PlayBGM(ACGameBGM[(int)GAMEBGM.Summer]);
                break;
            case GAMEBGM.Fall: // 秋
                PlayBGM(ACGameBGM[(int)GAMEBGM.Fall]);
                break;
            case GAMEBGM.Winter: // 冬
                PlayBGM(ACGameBGM[(int)GAMEBGM.Winter]);
                break;
            default:
                break;
        }
        Closeflg = false;
    }
    
    // 外部から村などのBGM呼び出し
    public void OpenUIBGM(string PlaceBGM)
    {
        // フェードアウト開始
        fadeBGM = 1;
        // 次のBGMを指定
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
            Debug.Log("再生できません");
        }

    }
    public void CloseUIBGM()
    {
        fadeBGM = 1;
        UseUIflg = false;
        Closeflg = true;
    }

    // 指定のSEを再生するために呼び出す
    public void PublicPlaySE(GAMESE se)
    {
        PlaySE(ACGameSE[(int)se]);
    }
}
