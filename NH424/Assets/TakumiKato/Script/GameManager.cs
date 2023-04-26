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

    bool bFirstReset = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
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
    }

    void Update()
    {
        if(bFirstReset)
        {
            FirstReset();
            if(canActUnitNum > 0)
                bFirstReset = false;
        }

        if (friendCatList.Count >= 20)// || Input.GetKeyDown(KeyCode.A))
        {
            //fade.FadeIn(2.0f, () => { SceneManager.LoadScene("ClearScene"); });
            SceneManager.LoadScene("ClearScene");
        }

        if(!bFirstReset)
            if (canActUnitNum <= 0 || (Input.GetButtonDown("TurnEnd") && bMenuDisplay()))
                EndTurn();

        if (nowTurn == seasonTurnNum)
        {
            if (!SeasonMission.instance.Check(season))
            {
                //fade.FadeIn(2.0f, () => { SceneManager.LoadScene("TitleScene"); });
                SceneManager.LoadScene("TitleScene");
            }
            m_audiosc.SelectBGM();
            nowTurn = 1;
            season++;
            SeasonIconUI.SetSeasonIcon();
            
            if(season == SEASON.MAX)
            {
                season = SEASON.SPRING;
                seasonRoundNum++;
            }
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
        if (MissionButton.instance.GetbMenu())
        {
            if (ShopButton.instance.GetbMenu() && VillageButton.instance.GetbMenu())
                return true;
            else
                return false;
        }
        else
            return false;
    }
    public void SetUICursol(bool act)
    {
        UICursol.gameObject.SetActive(act);
    }
    public void SetCharacterUI(bool act, Unit unit)
    {
        CharacterUI.gameObject.SetActive(act);
        if(act)
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
}