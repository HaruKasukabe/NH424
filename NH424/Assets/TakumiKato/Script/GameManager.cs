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

    public SEASON season = SEASON.SPRING;
    public int seasonTurnNum = 5;
    public int nowTurn = 0;
    public List<Unit> activeUnitList = new List<Unit>();
    public int canActUnitNum = 0;

    public int level = 0;
    public float wood = 0.0f;
    public float food = 0.0f;
    public float stone = 0.0f;
    public float iron = 0.0f;
    public float levelUpNeed = 100.0f;

    float foodTime = 0;
    float Hex_Width = 1.1f;

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
    }

    void Update()
    {
        foodTime += Time.deltaTime;
        if (foodTime > 1.0f)
        {
            food += food / 1000.0f;
            foodTime = 0;
        }

        if (canActUnitNum <= 0)
            EndTurn();

        if(nowTurn == seasonTurnNum)
        {
            if(!SeasonMission.instance.Check(season))
                SceneManager.LoadScene("TitleScene");
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
        activeUnitList.Add(unit);
        canActUnitNum++;
    }

    public void EndTurn()
    {
        nowTurn++;
        canActUnitNum = activeUnitList.Count;
        for (int i = 0; i < activeUnitList.Count; i++)
            activeUnitList[i].SetAct();
    }

    public Vector3 Cal_HexPosToViewLocalPos(Vector3 hexPos)
    {
        // Y•ûŒü‚‚³
        float Hex_Height = Hex_Width * Mathf.Sin(60.0f * Mathf.Deg2Rad);

        // X•ûŒü‚Ì‚¸‚ê
        float Hex_Adjust = Hex_Width * Mathf.Cos(60.0f * Mathf.Deg2Rad);

        float grid_X = Hex_Width * hexPos.x + Hex_Adjust * Mathf.Abs(hexPos.z % 2);
        float grid_Z = Hex_Height * hexPos.z;

        return new Vector3(grid_X, 0.0f, grid_Z);
    }

    public bool IsEven(int num)
    {
        return (num % 2 == 0);
    }
}