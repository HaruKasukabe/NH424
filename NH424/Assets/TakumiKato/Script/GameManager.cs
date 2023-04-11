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
    SPRING_2,
    SUMMER_2,
    FALL_2,
    WINTER_2
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
    public Fade fade;

    public SEASON season = SEASON.SPRING;
    public int seasonTurnNum = 5;
    public int nowTurn = 0;
    public int canActUnitNum = 0;
    public int moveNumTotal = 0;
    public int clearFriendNum = 3;
    public int friendNum = 1;

    public int level = 0;
    public float food = 0.0f;
    public float wood = 0.0f;
    public float stone = 0.0f;
    public float iron = 0.0f;
    public float levelUpNeed = 100.0f;

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
        moveNumTotal = KemokoListOut.instance.GetMoveNumTotal();
        if (moveNumTotal < 5)
            canActUnitNum = moveNumTotal;
        else
            canActUnitNum = KemokoListOut.instance.maxOutNum;

        fade.FadeOut(2.0f);
    }

    void Update()
    {
        if (friendNum >= clearFriendNum)// || Input.GetKeyDown(KeyCode.A))
        {
            //fade.FadeIn(2.0f, () => { SceneManager.LoadScene("ClearScene"); });
            SceneManager.LoadScene("ClearScene");
        }

        if (canActUnitNum <= 0)
            EndTurn();

        if (nowTurn == seasonTurnNum)
        {
            if (!SeasonMission.instance.Check(season))
            {
                //fade.FadeIn(2.0f, () => { SceneManager.LoadScene("TitleScene"); });
                SceneManager.LoadScene("TitleScene");
            }
            nowTurn = 0;
            season++;
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
}