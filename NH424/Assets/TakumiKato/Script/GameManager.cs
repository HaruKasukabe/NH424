using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SEASON
{
    SPRING,
    SUMMER,
    FALL,
    WINTER,

    MAX
}
struct FLOAT2
{
    public float x;
    public float z;

    public FLOAT2(float a, float b)
    {
        x = a;
        z = b;
    }
};
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
    public ManagementAudio m_audiosc; // オーディオ管理スクリプト
    public GameObject UICursol;
    public GameObject CharacterUI;
    public Fade fade;
    public GameObject stage;
    public Vector3[] stageScale;
    public Material[] mat;

    public SEASON season = SEASON.SPRING;
    public int seasonRoundNum = 0;
    public int seasonTurnNum = 31;
    public int nowTurn = 1;
    public int canActUnitNum = 0;
    public int moveNumTotal = 0;
    public List<int> friendCatList = new List<int>();
    public int friendNum = 1;

    public int level = 0;
    public int maxVillageLevel = 3;
    public float food = 0.0f;
    public float wood = 0.0f;
    public float stone = 0.0f;
    public float iron = 0.0f;
    public float levelUpNeed = 100.0f;

    public bool bFirstReset = true;

    public OptionSC option;

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
        fade.FadeOut(2.0f);
        m_audiosc.StartBGM();
        WindowEffect.instance.PlaySeasonEffect();
    }

    void Update()
    {
        if(bFirstReset)
        {
            FirstReset();
            if(canActUnitNum > 0)
                bFirstReset = false;
        }

        if (friendCatList.Count >= 20)
        {
            WindowEffect.instance.PlayClearEffect();
            StartCoroutine(DelayCoroutine(3, () =>
            {
                //fade.FadeIn(2.0f, () => { SceneManager.LoadScene("TitleScene"); });
                ScoreManager.instance.ScoreAdd(KemokoListOut.instance.outUnitList, KemokoListVillage.instance.villageUnitList);
                SceneManager.LoadScene("ResultScene");
            }));
        }

        if(!bFirstReset)
            if (canActUnitNum <= 0 || (Input.GetButtonDown("TurnEnd") && bMenuDisplay()))
                EndTurn();

        if (nowTurn == seasonTurnNum)
        {
            if (!SeasonMission.instance.Check(season, seasonRoundNum))
            {
                WindowEffect.instance.PlayOverEffect();
                StartCoroutine(DelayCoroutine(3, () =>
                {
                    //fade.FadeIn(2.0f, () => { SceneManager.LoadScene("TitleScene"); });
                    ScoreManager.instance.ScoreAdd(KemokoListOut.instance.outUnitList, KemokoListVillage.instance.villageUnitList);
                    SceneManager.LoadScene("ResultScene");
                }));
            }
            m_audiosc.NextSeason();
            nowTurn = 1;
            season++;

            if (season == SEASON.MAX)
            {
                season = SEASON.SPRING;
                seasonRoundNum++;
            }

            WindowEffect.instance.PlaySeasonEffect();
            RenderSettings.skybox = mat[(int)season];
            SeasonIconUI.SetSeasonIcon();
            SeasonEvent.instance.ResetEvent();      
        }
    }

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
                    levelUpNeed = 100.0f * (level + 1);
                    if(level < 4)
                        Map.instance.LevelUpHouse();
                }
    }

    public void AddUnit(Unit unit)
    {
        if (!KemokoListOut.instance.Add(unit))
            KemokoListVillage.instance.Add(unit);
    }
    public void AddSelectUnit(Unit unit)
    {
        if (!KemokoListOut.instance.SelectAdd(unit))
            KemokoListVillage.instance.Add(unit);
    }

    public void EndTurn()
    {
        if (bMenuDisplay())
        {
            nowTurn++;
            List<Unit> unitList = KemokoListOut.instance.outUnitList;

            ShopList.instance.ChengeList();

            moveNumTotal = KemokoListOut.instance.GetMoveNumTotal();
            if (moveNumTotal < KemokoListOut.instance.maxOutNum)
                canActUnitNum = moveNumTotal;
            else
                canActUnitNum = KemokoListOut.instance.maxOutNum;

            for (int i = 0; i < unitList.Count; i++)
                unitList[i].SetAct();
        }
    }
    void FirstReset()
    {
        List<Unit> unitList = KemokoListOut.instance.outUnitList;

        ShopList.instance.ChengeList();

        moveNumTotal = KemokoListOut.instance.GetMoveNumTotal();
        if (moveNumTotal < KemokoListOut.instance.maxOutNum)
            canActUnitNum = moveNumTotal;
        else
            canActUnitNum = KemokoListOut.instance.maxOutNum;

        for (int i = 0; i < unitList.Count; i++)
            unitList[i].SetAct();
    }

    public bool IsEven(int num)
    {
        return (num % 2 == 0);
    }
    public bool MoveUnitInFhase()
    {
        int max = KemokoListOut.instance.maxOutNum;
        if (moveNumTotal >= max)
        {
            if (canActUnitNum == max)
                return true;
            else
                return false;
        }
        else
        {
            if (canActUnitNum == moveNumTotal)
                return true;
            else
                return false;
        }
    }
    public bool bMenuDisplay()
    {
        if (SelectButtons.instance.GetbFriendSelect())
        {
            if (!Tutorial.instance.Main.activeSelf)
            {
                if (MissionButton.instance.GetbMenu() && !option.bOpenOption())
                {
                    if (ShopButton.instance.GetbMenu() && VillageButton.instance.GetbMenu())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        else
        {
            SetUICursol(true);
            return false;
        }
    }
    public void SetUICursol(bool act)
    {
        UICursol.gameObject.SetActive(act);
    }
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
    public void AddFriendCatNum(Unit unit)
    {
        bool bAdd = true;
        for(int i = 0; i < friendCatList.Count; i++)
        {
            if (friendCatList[i] == unit.sta.number)
                bAdd = false;
        }
        if(bAdd)
        {
            friendCatList.Add(unit.sta.number);
        }
    }
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
    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}