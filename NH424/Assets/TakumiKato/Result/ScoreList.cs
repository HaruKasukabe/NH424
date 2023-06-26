//=============================================================================
//
//  スコアリスト　クラス [ScoreList.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スコア情報
public struct SCORE
{
    public UNIT_SCORE unitScore;    // スコア
    public GameObject obj;          // スコアのオブジェクト
}

public class ScoreList : MonoBehaviour
{
    List<UNIT_SCORE> unitScoreList;
    List<SCORE> scoreList = new List<SCORE>();

    public GameObject origin;
    Vector2 scorePos = new Vector2(340, -162);
    Vector2 addNum = new Vector2(0, -120.0f);
    Vector3 pos = new Vector3(0.5f, 0.6f, 0);

    [SerializeField] Transform firstPosObj;

    // Start is called before the first frame update
    void Start()
    {
        unitScoreList = ScoreManager.instance.scoreList;    // ユニットのスコアリストを取得

        for(int i = 0; i < unitScoreList.Count; i++)
        {
            SCORE score;

            GameObject obj = Instantiate(origin);   // スコアを生成
            obj.transform.parent = transform;
            obj.GetComponent<Result>().SetText(unitScoreList[i]);   // テキストと画像を設定
            obj.transform.position = scorePos;

            score.obj = obj;    // オブジェクトを設定
            score.unitScore = unitScoreList[i]; // スコアを設定

            scoreList.Add(score);   // スコアリストに追加

            scorePos += addNum; // 次のスコアの位置をずらす
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
