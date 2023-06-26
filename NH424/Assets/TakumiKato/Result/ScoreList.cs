//=============================================================================
//
//  �X�R�A���X�g�@�N���X [ScoreList.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�R�A���
public struct SCORE
{
    public UNIT_SCORE unitScore;    // �X�R�A
    public GameObject obj;          // �X�R�A�̃I�u�W�F�N�g
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
        unitScoreList = ScoreManager.instance.scoreList;    // ���j�b�g�̃X�R�A���X�g���擾

        for(int i = 0; i < unitScoreList.Count; i++)
        {
            SCORE score;

            GameObject obj = Instantiate(origin);   // �X�R�A�𐶐�
            obj.transform.parent = transform;
            obj.GetComponent<Result>().SetText(unitScoreList[i]);   // �e�L�X�g�Ɖ摜��ݒ�
            obj.transform.position = scorePos;

            score.obj = obj;    // �I�u�W�F�N�g��ݒ�
            score.unitScore = unitScoreList[i]; // �X�R�A��ݒ�

            scoreList.Add(score);   // �X�R�A���X�g�ɒǉ�

            scorePos += addNum; // ���̃X�R�A�̈ʒu�����炷
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
