//=============================================================================
//
// ゲームマネージャー クラス [GameManager.cpp]
//
//=============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 季節
public enum SEASON
{
    SPRING,
    SUMMER,
    FALL,
    WINTER,

    MAX
}
// float x, z
public struct FLOAT2
{
    public float x;
    public float z;

    public FLOAT2(float a, float b)
    {
        x = a;
        z = b;
    }
};
// int x, z
public struct INT2
{
    public int x;
    public int z;

    public INT2(int a, int b)
    {
        x = a;
        z = b;
    }
};

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] ManagementAudio m_audiosc;         // オーディオ管理スクリプト
    [SerializeField] GameObject UICursol;               // UIカーソルオブジェクト
    [SerializeField] GameObject CharacterUI;            // キャラクターのステータス情報UIオブジェクト
    [SerializeField] Fade fade;                         // フェイド
    [SerializeField] GameObject stage;                  // ユニットの移動範囲オブジェクト
    [SerializeField] Vector3[] stageScale;              // 移動範囲の大きさ配列
    [SerializeField] Material[] skyMat;                 // 空のマテリアル4季節分

    public SEASON season = SEASON.SPRING;               // 季節
    int seasonRoundNum = 0;                             // 季節を1週した数
    int seasonTurnNum = 31;                             // 季節のターン数
    public int nowTurn = 1;                             // 今のターン数
    public int canActUnitNum = 0;                       // ユニットを動かせる数
    int originActUnitNum;                               // ターン初めのユニットを動かせる数
    int moveNumTotal = 0;                               // 今いるすべてのユニットの移動可能回数を合計した数
    public List<int> friendCatList = new List<int>();   // 仲間にした種類のリスト
    public int friendNum = 1;                           // 仲間の数
    bool bClear = false;                                // クリアした時のプログラムを実行したか

    public int level = 0;                               // 現在の村レベル
    public int maxVillageLevel = 3;                     // 村を拡大する最大レベル
    public float food = 0.0f;                           // 食材
    public float wood = 0.0f;                           // 木材
    public float stone = 0.0f;                          // 石材
    public float iron = 0.0f;                           // 鉄材
    public float levelUpNeed = 100.0f;                  // レベルアップに必要な素材の数

    public bool bFirstReset = true;                     // 最初のリセットをしたか

    [SerializeField] OptionSC option;                   // オプション
    public PictorialBook book;                          // 図鑑

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

    void Start()
    {
        fade.FadeOut(2.0f);         // フェイドアウト
        book.DiscoverCharacter(0);  // 最初のユニットの図鑑解放
    }

    void Update()
    {
        // 最初のリセット
        if(bFirstReset)
        {
            FirstReset();
            if(canActUnitNum > 0)
                bFirstReset = false;
        }

        // 仲間にした種類数が20以上の時クリア
        if (friendCatList.Count >= 20 && !bClear)
        {
            WindowEffect.instance.PlayClearEffect();    // ゲームクリアエフェクトを再生
            StartCoroutine(DelayCoroutine(3, () =>      // ３つ待ってから
            {
                ScoreManager.instance.ScoreAdd(KemokoListOut.instance.outUnitList, KemokoListVillage.instance.villageUnitList); // ユニットのスコアを残す
                fade.FadeIn(2.0f, () => { SceneManager.LoadScene("ResultScene"); });    // リザルトシーンにフェイドイン
            }));
            bClear = true;
        }

        // ターンを終了
        if(!bFirstReset)    // 最初のリセットをちゃんとしたか
            if (canActUnitNum <= 0 || (Input.GetButtonDown("TurnEnd") && bMenuDisplay()))
                EndTurn();

        // 今のターンが季節の最大ターン数になったとき
        if (nowTurn == seasonTurnNum)
        {
            // 季節ミッションをクリアできなかったら
            if (!SeasonMission.instance.Check())
            {
                WindowEffect.instance.PlayOverEffect(); // ゲームオーバーエフェクトを再生
                StartCoroutine(DelayCoroutine(3, () =>  // ３つ待ってから
                {
                    ScoreManager.instance.ScoreAdd(KemokoListOut.instance.outUnitList, KemokoListVillage.instance.villageUnitList); // ユニットのスコアを残す
                    fade.FadeIn(2.0f, () => { SceneManager.LoadScene("ResultScene"); });    // リザルトシーンにフェイドイン
                }));
            }

            m_audiosc.NextSeason(); // BGMを次の季節のに
            nowTurn = 1;            // 今のターンを1に設定
            season++;               // 次の季節に

            // 季節が冬であれば
            if (season == SEASON.MAX)
            {
                season = SEASON.SPRING;
                seasonRoundNum++;   // 季節周回数を+1
            }

            WindowEffect.instance.ChangeSeasonEffect();     // 季節エフェクトを切り替え
            RenderSettings.skybox = skyMat[(int)season];    // 空の色を切り替え
            SeasonIconUI.instance.SetSeasonIcon();          // 季節アイコンUIを切り替え
            SeasonEvent.instance.ResetEvent();              // 季節イベントをリセット
            KemokoListOut.instance.SetGourmet();            // 美食家のタグ能力リセット
            KemokoListOut.instance.SetCarbonated();         // 炭酸水のタグ能力リセット
        }

        // Bボタンを押したときにUIカーソルを消す
        if (Input.GetButtonDown("Fire2"))
            SetUICursol(false);
    }

    // 村レベルアップ
    public void LevelUp()
    {
        if(wood >= levelUpNeed)
            if (stone >= levelUpNeed)
                if (iron >= levelUpNeed)
                {
                    wood -= levelUpNeed;
                    stone -= levelUpNeed;
                    iron -= levelUpNeed;
                    level++;
                    levelUpNeed = 100.0f * (level + 1);     // レベルアップに必要な素材の数を設定

                    // レベルが4以下の時家のモデルを切り替え
                    if(level < 5)
                        Map.instance.LevelUpHouse();
                }
    }

    // ユニットを追加
    public void AddUnit(Unit unit)
    {
        if (!KemokoListOut.instance.Add(unit))
            KemokoListVillage.instance.Add(unit);
    }
    // ショップで買ったユニットを追加
    public void AddSelectUnit(Unit unit)
    {
        if (!KemokoListOut.instance.SelectAdd(unit))
            KemokoListVillage.instance.Add(unit);
    }

    // ターン終了
    public void EndTurn()
    {
        // 何かメニューを表示していない時
        if (bMenuDisplay())
        {
            nowTurn++;  // ターン数を+1
            List<Unit> unitList = KemokoListOut.instance.outUnitList;

            ShopList.instance.ChengeList(); // ショップの並びを変更
            KemokoListOut.instance.SetSleep(); // 寝る子は育つのタグ能力フラグセット

            moveNumTotal = KemokoListOut.instance.GetMoveNumTotal();
            if (moveNumTotal < KemokoListOut.instance.maxOutNum)    // 最大移動可能数が5より小さい場合
                originActUnitNum = canActUnitNum = moveNumTotal;
            else
                originActUnitNum = canActUnitNum = KemokoListOut.instance.maxOutNum;

            for (int i = 0; i < unitList.Count; i++)
                unitList[i].SetAct();   // ユニットの移動可能回数をリセット
        }
    }

    // 最初のリセット
    void FirstReset()
    {
        List<Unit> unitList = KemokoListOut.instance.outUnitList;

        ShopList.instance.ChengeList();
        KemokoListOut.instance.SetSleep();

        moveNumTotal = KemokoListOut.instance.GetMoveNumTotal();
        if (moveNumTotal < KemokoListOut.instance.maxOutNum)
           originActUnitNum = canActUnitNum = moveNumTotal;
        else
           originActUnitNum = canActUnitNum = KemokoListOut.instance.maxOutNum;

        for (int i = 0; i < unitList.Count; i++)
            unitList[i].SetAct();
    }

    // 偶数奇数判定
    public bool IsEven(int num)
    {
        return (num % 2 == 0);
    }

    // 何かメニューを表示しているか
    public bool bMenuDisplay()
    {
        if (!Tutorial.instance.Main.activeSelf && !option.bOpenOption())
        {
            if (SelectButtons.instance.GetbFriendSelect() && book.GetOpenFlg())
            {
                if (MissionButton.instance.GetbMenu())
                {
                    if (ShopButton.instance.GetbMenu() && VillageButton.instance.GetbMenu())
                    {
                        SetUICursol(false);
                        return true;
                    }
                    else
                    {
                        SetUICursol(true);
                        return false;
                    }
                }
                else
                {
                    SetUICursol(true);
                    return false;
                }
            }
            else
            {
                SetUICursol(true);
                return false;
            }
        }
        else
            return false;
    }

    // UIカーソルを設定
    public void SetUICursol(bool act)
    {
        UICursol.gameObject.SetActive(act);
    }

    // 選択したユニットの情報UIを設定
    public void SetCharacterUI(bool act, Unit unit)
    {
        CharacterUI.gameObject.SetActive(act);
        stage.SetActive(act);
        Vector3 pos = new Vector3(unit.transform.position.x, unit.transform.position.y + 0.2f, unit.transform.position.z);
        stage.transform.position = pos;
        stage.transform.localScale = stageScale[unit.sta.moveLong];

        if (act)
            CharacterUI.GetComponentInChildren<SelectCharaUI>().SetUnit(unit);
    }

    // 仲間の種類数追加
    public void AddFriendCatNum(Unit unit)
    {
        bool bAdd = true;
        for(int i = 0; i < friendCatList.Count; i++)
        {
            if (friendCatList[i] == unit.sta.number)
                bAdd = false;
        }
        // 今まで仲間にしていなかったユニットなら
        if(bAdd)
        {
            friendCatList.Add(unit.sta.number);
        }
    }

    // 仲間にしたことがある種類のユニットか判定
    public bool bFriendCat(int number)
    {
        bool bFriend = false;
        for (int i = 0; i < friendCatList.Count; i++)
        {
            if (friendCatList[i] == number)
                bFriend = true;
        }
        return bFriend;
    }

    // １回でもユニットが動いているか
    public bool bMoveUnitThisTurn()
    {
        if (canActUnitNum != originActUnitNum)
            return false;
        else
            return true;
    }

    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}