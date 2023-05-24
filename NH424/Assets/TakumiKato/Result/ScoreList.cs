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
    //Vector2 firstPos = new Vector2(60, 313);
    Vector2 firstPos;
    Vector2 addNum = new Vector2(0, -60.0f);
    Vector3 pos = new Vector3(0.5f, 0.6f, 0);

    [SerializeField] Transform firstPosObj;

    // Start is called before the first frame update
    void Start()
    {
        firstPos = new Vector2(firstPosObj.position.x, firstPosObj.position.y);
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
