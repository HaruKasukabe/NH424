//=============================================================================
//
// �V�[�Y���~�b�V���� �N���X [SeasonMission.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonMission : MonoBehaviour
{
    public static SeasonMission instance = null;

    TextMeshProUGUI text;

    float baseSeasonMaterial = 30.0f;   // �ŏ��ɕK�v�ȑf�ޗ�
    public float seasonMaterial;

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
        text = GetComponent<TextMeshProUGUI>();
        seasonMaterial = baseSeasonMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "�؍ށF" + seasonMaterial + " �ȏ�" + " �΍ށF" + seasonMaterial + " �ȏ�" + " �S�ށF" + seasonMaterial + " �ȏ�";
    }

    // �V�[�Y���~�b�V�����B���m�F
    public bool Check()
    {
        if (BaseMission())
            return true;

        return false;
    }

    bool BaseMission()
    {
        // �f�ނ����邩�m�F
        if (GameManager.instance.wood >= seasonMaterial)
            if (GameManager.instance.stone >= seasonMaterial)
                if (GameManager.instance.iron >= seasonMaterial)
                {
                    seasonMaterial += baseSeasonMaterial;   // ����ɕK�v�ȑf�ޗʂ𑝉�
                    GameManager.instance.wood -= seasonMaterial;
                    GameManager.instance.stone -= seasonMaterial;
                    GameManager.instance.iron -= seasonMaterial;
                    return true;
                }
        return false;
    }
    bool SpringMission(int roundNum)
    {
        return true;
    }
    bool SummerMission(int roundNum)
    {
        return true;
    }
    bool FallMission(int roundNum)
    {
        return true;
    }
    bool WinterMission(int roundNum)
    {
        return true;
    }
}
