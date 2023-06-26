//=============================================================================
//
// �w�N�X �N���X [Hex.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Unit Unit;                           // ���u����Ă��郆�j�b�g
    public INT2 hexNum;                         // ���̃}�X�̃}�b�v���W
    public INT2[] nextNum = new INT2[6];        // �אڂ��Ă���}�X�̃}�b�v���W

    protected Renderer rend;
    MeshRenderer mesh;

    int level;
    protected int pickTime = 3;                 // �f�ފl���\��
    public bool bUnit = false;                  // ���j�b�g����ɂ��邩
    bool bCursol = false;                       // �J�[�\�����}�X�̏�ɂ��邩
    public bool bReverse = false;               // ���̃}�X���J�����Ă��邩
    public bool bDiscover = false;              // ���̃}�X����������Ă��邩
    bool bSetDiscover = false;                  // ����̃}�X�𔭌��ݒ�ɂ�����
    bool bEnd = false;                          // ���̃}�X���}�b�v�̒[�ł��邩
    bool bSetNextHex = false;                   // �}�b�v�g���Q�T�ڂ����邩
    public bool bMaterialHex = false;           // �f�ރ}�X���ǂ���
    public bool bGarden = false;                // �H�ރ}�X���ǂ���
    public bool bGetMaterial = false;           // true���ɑf�ފl��
    [SerializeField] GameObject normalHex;      // �ʏ�w�N�X�I�u�W�F�N�g

    [SerializeField] GameObject effectObject;   // ���j�b�g��u�����Ƃ��̃G�t�F�N�g
    float deleteTime = 1;                       // �����܂ł̎���
    float offsetY = 0.15f;                      // ����

    [SerializeField] GameObject iventEffect;    // �C�x���g�}�X�ɐݒ肳�ꂽ���̃G�t�F�N�g
    GameObject iventEffectNow;                  // ������C�x���g�G�t�F�N�g
    float iventOffsetY = -1.25f;                // ����
    float iventMaterial = 30.0f;                // �C�x���g�}�X�̑f�ފl����


    // Start is called before the first frame update
    protected void Awake()
    {
        rend = GetComponent<Renderer>();
        mesh = GetComponent<MeshRenderer>();
        mesh.material.EnableKeyword("_EMISSION");
        rend.material.color = new Color32(0, 0, 0, 1); // ��(����������Ă��Ȃ����)
    }

    // Update is called once per frame
    protected void Update()
    {
        mesh.material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));

        // �������ꂽ�ꍇ
        if(bDiscover && !bReverse)
        {
            mesh.material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f));  // ������
        }
        // �J�[�\��������ꍇ
        if (bCursol)
        {
            mesh.material.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f));  // ��
            bCursol = false;
        }

        // �אڂ��Ă���}�X�𔭌��ݒ�ɂ��Ă��Ȃ������]����Ă���ꍇ
        if (!bSetDiscover && bReverse)
        {
            for (int i = 0; i < nextNum.Length; i++)
            {
                Hex hex = Map.instance.GetHex(nextNum[i]).GetComponent<Hex>();
                hex.bDiscover = true;
                if (hex.bUnit && hex.gameObject.activeSelf)
                    if (!hex.Unit.bFriend)
                    {
                        hex.Unit.gameObject.SetActive(true);
                        Tutorial.instance.No_Kemoko();  // �����Ԃ̃`���[�g���A���\��
                    }
                bSetDiscover = true;
            }
        }

        // ���x���A�b�v�������@or  �ŏ��̃}�b�v��������̃��E���h�����܂�����Ƃ�
        if ((level != GameManager.instance.level) || Map.instance.round > 0)
            SetNextHex2();
        else if (bSetNextHex)
            SetNextHex();
    }

    // �f�ފl��
    protected void GetMaterial(UNIT_ACT act)
    {
        if (bUnit && bGetMaterial)
        {
            if (Unit.bFriend)
            {
                int idol = UnitTagAbility.instance.GetVillageIdol();

                switch (act)
                {
                    case UNIT_ACT.GARDEN:
                        float food;
                        if (GameManager.instance.season == SEASON.FALL)
                            food = Unit.sta.food * 1.5f;
                        else
                            food = Unit.sta.food;
                        GameManager.instance.food += food;
                        Unit.score.food += food;
                        for(int i = 0; i < Unit.sta.unitTag.Length; i++)
                            if(Unit.sta.unitTag[i] == UnitTag.�M���F)
                            {
                                GameManager.instance.food += 30;
                                Unit.score.food += 30;
                            }
                        if (Unit.bGourmetSeason)
                        {
                            GameManager.instance.food += 50;
                            Unit.score.food += 50;
                            Unit.bGourmetSeason = false;
                        }
                        if (Unit.bCarbonatedSeason)
                        {
                            GameManager.instance.food += 50;
                            Unit.score.food += 50;
                            Unit.bCarbonatedSeason = false;
                        }
                        if(Unit.bSleepSeason)
                        {
                            GameManager.instance.food += food;
                            Unit.score.food += food;
                        }
                        GameManager.instance.food += 10 * idol;
                        Unit.score.food += 10 * idol;
                        break;
                    case UNIT_ACT.FOREST:
                        GameManager.instance.wood += Unit.sta.wood;
                        Unit.score.wood += Unit.sta.wood;
                        GameManager.instance.wood += 10 * idol;
                        Unit.score.wood += 10 * idol;
                        if (Unit.bSleepSeason)
                        {
                            GameManager.instance.wood += Unit.sta.wood;
                            Unit.score.wood += Unit.sta.wood;
                        }
                        break;
                    case UNIT_ACT.QUARRY:
                        GameManager.instance.stone += Unit.sta.stone;
                        Unit.score.stone += Unit.sta.stone;
                        GameManager.instance.stone += 10 * idol;
                        Unit.score.stone += 10 * idol;
                        if (Unit.bSleepSeason)
                        {
                            GameManager.instance.stone += Unit.sta.stone;
                            Unit.score.stone += Unit.sta.stone;
                        }
                        break;
                    case UNIT_ACT.COAL_MINE:
                        GameManager.instance.iron += Unit.sta.iron;
                        Unit.score.iron += Unit.sta.iron;
                        GameManager.instance.iron += 10 * idol;
                        Unit.score.iron += 10 * idol;
                        if (Unit.bSleepSeason)
                        {
                            GameManager.instance.iron += Unit.sta.iron;
                            Unit.score.iron += Unit.sta.iron;
                        }
                        break;
                    case UNIT_ACT.ALL:
                        GameManager.instance.food += iventMaterial;
                        Unit.score.food += iventMaterial;
                        GameManager.instance.food += 10 * idol;
                        Unit.score.food += 10 * idol;
                        if (Unit.bSleepSeason)
                        {
                            GameManager.instance.food += iventMaterial;
                            Unit.score.food += iventMaterial;
                        }
                        GameManager.instance.wood += iventMaterial;
                        Unit.score.wood += iventMaterial;
                        GameManager.instance.wood += 10 * idol;
                        Unit.score.wood += 10 * idol;
                        if (Unit.bSleepSeason)
                        {
                            GameManager.instance.wood += iventMaterial;
                            Unit.score.wood += iventMaterial;
                        }
                        GameManager.instance.stone += iventMaterial;
                        Unit.score.stone += iventMaterial;
                        GameManager.instance.stone += 10 * idol;
                        Unit.score.stone += 10 * idol;
                        if (Unit.bSleepSeason)
                        {
                            GameManager.instance.stone += iventMaterial;
                            Unit.score.stone += iventMaterial;
                        }
                        GameManager.instance.iron += iventMaterial;
                        Unit.score.iron += iventMaterial;
                        GameManager.instance.iron += 10 * idol;
                        Unit.score.iron += 10 * idol;
                        if (Unit.bSleepSeason)
                        {
                            GameManager.instance.iron += iventMaterial;
                            Unit.score.iron += iventMaterial;
                        }
                        break;
                    case UNIT_ACT.NULL:
                    default:
                        break;
                }

                bGetMaterial = false;
                pickTime--;             // �f�ފl���\��-1

                // �f�ފl���\�񐔂�0�ȉ��̎�
                if (pickTime < 1)
                    ChangeNormalHex();  // �ʏ�w�N�X�ɐ؂�ւ�
            }
        }
    }

    // �}�b�v�g��
    void SetNextHex()
    {
        // �}�b�v�[�̃}�X�Ȃ��
        if (bEnd)
        {
            for (int i = 0; i < nextNum.Length; i++)
            {
                INT2 num = nextNum[i];
                GameObject obj = Map.instance.map[num.x, num.z];
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    obj.GetComponent<Hex>().SetEnd();
                    Map.instance.round--;
                }
            }
        }
    }
    // �}�b�v�g���Q
    void SetNextHex2()
    {
        // �}�b�v�[�̃}�X�Ȃ��
        if (bEnd)
        {
            for (int i = 0; i < nextNum.Length; i++)
            {
                INT2 num = nextNum[i];
                GameObject obj = Map.instance.map[num.x, num.z];
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    obj.GetComponent<Hex>().SetEnd();
                    obj.GetComponent<Hex>().bSetNextHex = true;
                    Map.instance.round--;
                }
            }
        }
    }

    // �ʏ�w�N�X�ɐ؂�ւ�
    void ChangeNormalHex()
    {
        GameObject newObj = Instantiate(normalHex, transform.position, Quaternion.identity);
        Hex newHex = newObj.GetComponent<Hex>();

        newHex.rend.material.color = new Color32(255, 255, 255, 1);
        newHex.SetHexNum(hexNum);
        newHex.bReverse = true;

        if (Unit)
        {
            newHex.SetUnit(Unit);
            Unit.SetHex(newHex);
        }

        Map.instance.map[hexNum.x, hexNum.z] = newObj;
        Destroy(this.gameObject);
    }

    // �C�x���g�w�N�X�ɐ؂�ւ�(0:�t�A1:�~)
    public void ChangeEventHex(int num)
    {
        GameObject newObj = Instantiate(Map.instance.iventHex[num], transform.position, Quaternion.identity);
        Hex newHex = newObj.GetComponent<Hex>();

        newHex.rend.material.color = new Color32(255, 255, 255, 1);
        newHex.SetHexNum(hexNum);
        newHex.bDiscover = true;
        newHex.bReverse = true;

        if (Unit)
        {
            newHex.SetUnit(Unit);
            Unit.SetHex(newHex);
        }

        Map.instance.map[hexNum.x, hexNum.z] = newObj;
        Destroy(this.gameObject);
    }

    // �J�[�\�������邩��ݒ�
    public void SetCursol(bool b)
    {
        bCursol = b;
    }

    // �}�b�v�̏I�[�ɐݒ�
    public void SetEnd()
    {
        bEnd = true;
        level = GameManager.instance.level;
    }

    // ���j�b�g�̏���ݒ�
    public void SetUnit(Unit unit)
    {
        if (bReverse)
        {
            Unit = unit;
            bUnit = true;

            GameObject instantiateEffect = Instantiate(effectObject, transform.position + new Vector3(0f, offsetY, 0f), Quaternion.identity);
            Destroy(instantiateEffect, deleteTime);
        }
        else if (bDiscover)
        {
            Unit = unit;
            Unit.score.reverseHexNum++;
            rend.material.color = new Color32(255, 255, 255, 1);
            bUnit = true;
            bReverse = true;

            GameObject instantiateEffect = Instantiate(effectObject, transform.position + new Vector3(0f, offsetY, 0f), Quaternion.identity);
            Destroy(instantiateEffect, deleteTime);
        }
    }
    // �����Ԃ̃��j�b�g�̏���ݒ�
    public void SetStrayUnit(Unit unit)
    {
        Unit = unit;
        Unit.SetHex(this);
        bUnit = true;
    }

    // �}�X��������
    public bool SetReverse()
    {
        if (!bReverse)
        {
            rend.material.color = new Color32(255, 255, 255, 1);
            bReverse = true;

            GameObject instantiateEffect = Instantiate(effectObject, transform.position + new Vector3(0f, offsetY, 0f), Quaternion.identity);
            Destroy(instantiateEffect, deleteTime);

            return true;
        }
        else
            return false;
    }

    // �ォ�烆�j�b�g�����Ȃ��Ȃ������Ƀ��j�b�g�̏����Ȃ���
    public void DisUnit()
    {
        Unit = null;
        bUnit = false;
    }

    // ���͂̃w�N�X�ԍ���ݒ�
    public void SetHexNum(INT2 num)
    {
        hexNum = num;

        // ����������
        if (GameManager.instance.IsEven(hexNum.z))
        {
            nextNum[0] = new INT2(hexNum.x - 1, hexNum.z);
            nextNum[1] = new INT2(hexNum.x - 1, hexNum.z + 1);
            nextNum[2] = new INT2(hexNum.x, hexNum.z + 1);
            nextNum[3] = new INT2(hexNum.x + 1, hexNum.z);
            nextNum[4] = new INT2(hexNum.x, hexNum.z - 1);
            nextNum[5] = new INT2(hexNum.x - 1, hexNum.z - 1);
        }
        else
        {
            nextNum[0] = new INT2(hexNum.x - 1, hexNum.z);
            nextNum[1] = new INT2(hexNum.x, hexNum.z + 1);
            nextNum[2] = new INT2(hexNum.x + 1, hexNum.z + 1);
            nextNum[3] = new INT2(hexNum.x + 1, hexNum.z);
            nextNum[4] = new INT2(hexNum.x, hexNum.z - 1);
            nextNum[5] = new INT2(hexNum.x + 1, hexNum.z - 1);
        }
    }

    // �C�x���g�}�X�G�t�F�N�g�𐶐��A�폜
    public void SetEventHex(bool b)
    {
        if (b)
            iventEffectNow = Instantiate(iventEffect, transform.position + new Vector3(0f, iventOffsetY, 0f), Quaternion.identity);
        else
            Destroy(iventEffectNow);
    }
}
