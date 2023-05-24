using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    public List<UNIT_SCORE> scoreList = new List<UNIT_SCORE>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScoreAdd(List<Unit> outList, List<Unit> villageList)
    {
        scoreList.Clear();
        for (int i = 0; i < outList.Count; i++)
        {
            outList[i].score.number = scoreList.Count;
            scoreList.Add(outList[i].score);
        }
        for (int i = 0; i < villageList.Count; i++)
        {
            villageList[i].score.number = scoreList.Count;
            scoreList.Add(villageList[i].score);
        }
    }
}
