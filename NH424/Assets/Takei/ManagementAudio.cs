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
    public enum SEASONBGM
    {
        Spring = 0,
        Summer,
        Fall,
        Winter
    }

    // 変数宣言
    public SEASONBGM SeasonBGM; // 現在の季節BGM
    private int fadeBGM; // BGMフェード用
    public float FadeSecond = 2.0f; // フェードにかかる時間
    private float NowBGMVolume; // 現在のVolume
    public AudioSource ASBGM; // BGM1のオーディオソース
    public AudioSource ASSE; // SEのオーディオソース
    [SerializeField] AudioClip ACBGMSpring; // BGMのクリップ(ソース)増えるなら別スクリプトで管理予定
    [SerializeField] AudioClip ACBGMSummer; // BGMのクリップ(ソース)増えるなら別スクリプトで管理予定
    [SerializeField] AudioClip ACBGMFall; // BGMのクリップ(ソース)増えるなら別スクリプトで管理予定
    [SerializeField] AudioClip ACBGMWinter; // BGMのクリップ(ソース)増えるなら別スクリプトで管理予定

    // Initialize
    void Start()
    {
        ASBGM = GetComponent<AudioSource>();
        ASBGM.loop = true;
        ASSE = GetComponent<AudioSource>();
        ASSE.loop = false;

        // Resources.Loadで指定する場合拡張子は省略
        ACBGMSpring = (AudioClip)Resources.Load("BGM/Spring");
        ACBGMSummer = (AudioClip)Resources.Load("BGM/Summer");
        ACBGMFall = (AudioClip)Resources.Load("BGM/Fall");
        ACBGMWinter = (AudioClip)Resources.Load("BGM/Winter");

        fadeBGM = 0; // 0:初期値 1:フェードアウト 2:フェードイン
        NowBGMVolume = 0.6f; // 1.0が100%
        ASBGM.volume = NowBGMVolume; // デフォルトの音量を指定
        SeasonBGM = 0; // 初期値が春
        FadeSecond = FadeSecond * 60; // フレームレートに直すため秒数*60fps
    }

    // 更新処理
    void Update()
    {
        // BGM切り替え時フェード関数
        // フェードアウト
        if (fadeBGM == 1)
        {
            // 指定した秒数かけてフェードアウトする
            ASBGM.volume -= NowBGMVolume / FadeSecond;
            // フェードアウトが完了したら次の曲へ
            if (ASBGM.volume <= 0.0f)
            {
                // BGMを再生停止
                ASBGM.Stop();
                // 次の曲を再生開始 実装時は列挙体でswitch文
                switch(SeasonBGM)
                {
                    case SEASONBGM.Spring:// 春
                        PlayBGM(ACBGMSpring);
                        break;
                    case SEASONBGM.Summer: // 夏
                        PlayBGM(ACBGMSummer);
                        break;
                    case SEASONBGM.Fall: // 秋
                        PlayBGM(ACBGMFall);
                        break;
                    case SEASONBGM.Winter: // 冬
                        PlayBGM(ACBGMWinter);
                        break;
                    default:
                        break;
                }
                // フェードイン処理開始
                fadeBGM = 2;
            }

        }
        // フェードイン
        else if (fadeBGM == 2)
        {
            // 指定した秒数で音量を上げる
            ASBGM.volume += NowBGMVolume / FadeSecond;
            if (ASBGM.volume >= NowBGMVolume)
            {
                // フェード管理Numを初期値に戻す
                fadeBGM = 0;
            }
        }
        
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
    public void PlaySE(AudioClip SEclip)
    {
        ASSE.PlayOneShot(SEclip);
    }
    // =============================
    // 外部からBGMの再生をする関数
    // =============================
    // 初期化関数で呼び出す用
    public void StartBGM()
    {
        PlayBGM(ACBGMSpring);
    }
    // 次の曲を再生するために季節の変わり目で呼び出し
    public void SelectBGM()
    {
        // フェードアウト開始
        fadeBGM = 1;
        // 次の曲へ
        SeasonBGM++;
        // 冬なら春に戻す
        if (SeasonBGM >= SEASONBGM.Winter)
            SeasonBGM = SEASONBGM.Spring;
    }
}
