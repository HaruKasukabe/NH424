using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SCORE
{
    public UNIT_SCORE unitScore;
    public GameObject obj;
}

public class ScoreList : MonoBehaviour
{
    List<UNIT_SCORE> unitScoreList;
    List<SCORE> scoreList = new List<SCORE>();

    public GameObject origin;
    Vector2 firstPos = new Vector2(Screen.width / 2, Screen.height - 50.0f);
    Vector2 addNum = new Vector2(0, -30.0f);

    // Start is called before the first frame update
    void Start()
    {
        unitScoreList = ScoreManager.instance.scoreList;

        for(int i = 0; i < unitScoreList.Count; i++)
        {
            SCORE score;

            GameObject obj = Instantiate(origin);
            obj.transform.parent = transform;
            obj.GetComponent<Result>().SetText(unitScoreList[i]);
            obj.transform.position = firstPos;

            score.obj = obj;
            score.unitScore = unitScoreList[i];

            scoreList.Add(score);

            firstPos += addNum;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
