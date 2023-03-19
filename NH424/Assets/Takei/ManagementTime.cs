// ================================================
//  ManagementTime.cs[���ԊǗ��X�N���v�g]
// 
// Author:����y�s
//=================================================
// �ύX����
// 2023/02/25 �X�N���v�g�쐬
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementTime : MonoBehaviour
{
    // �񋓑̐錾
    enum Season
    {
        Spring = 0,
        Summer,
        Fall,
        Winter
    }
    // �ϐ��錾
    float seasontime; // �G�߂̃^�C�}�[�Ǘ�
    int lap; // �t�ďH�~������߂�����
    Season season; // �G�߂̗񋓑�
    bool StartFlg;


    // Start is called before the first frame update
    void Start()
    {
        StartFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartFlg)
        {
            // �^�C�}�[�v�Z(FPS60�z��)
            seasontime -= Time.deltaTime;
            Debug.Log("���ݎ���:" + seasontime);
            if (seasontime <= 0.0f)
            {
                if ((int)season == 3)
                {
                    lap += 1;
                    season = 0;
                    seasontime = 300.0f;
                }
                else
                {
                    season += 1;
                    seasontime = 300.0f;
                }

            }
        }
    }

    // �Q�[�����J�n
    public void GameStart()
    {
        // �^�C�}�[�����Z�b�g
        seasontime = 300.0f; // �f�t�H���g��5��
        season = 0;
        lap = 1;
        StartFlg = true;
    }

    // ���݂̋G�ߎ��Ԃ�Ԃ�
    public float GetSeasonTime()
    {
        return seasontime;
    }
    // ���݂̋G�߂�Ԃ�
    public string GetNowSeason()
    {
        switch(season)
        {
            case Season.Spring:
                return "�t";
            case Season.Summer:
                return "��";
            case Season.Fall:
                return "�H";
            case Season.Winter:
                return "�~";
        }
        return "�G���[";
    }
    // ���݂̏t�ďH�~�������ڂ��Ԃ�
    public int GetNowLap()
    {
        return lap;
    }
}
