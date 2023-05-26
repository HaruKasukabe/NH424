using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNIT_ACT    // �l�������ӂȑf�ށ@�����s�g�p
{
    GARDEN,
    FOREST,
    COAL_MINE,
    QUARRY,

    NULL,
}
public struct UNIT_SCORE
{
    public int number;              // �Ǘ��ԍ�
    public GameObject sprite;       // ���j�b�g�̉摜
    public string motifName;        // �L�����N�^�[�̃��`�[�t��
    public string charName;         // �L�����N�^�[�̖��O
    public int friendNum;           // ���̃��j�b�g�����Ԃ𑝂₵����
    public int reverseHexNum;       // �Ђ�����Ԃ����}�X�̐�
    public float food;              // ���܂Ŋl�������H��
    public float stone;             // ���܂Ŋl�������΍�
    public float wood;              // ���܂Ŋl�������؍�
    public float iron;              // ���܂Ŋl�������S��
}

public class Unit : MonoBehaviour
{
    public UnitData sta;                    // ��{�X�e�[�^�X
    public int id;                          // ���j�b�g��ނ��Ƃ�ID
    public bool bFriend = false;            // ���Ԃ��ǂ���
    public UNIT_SCORE score;                // �X�R�A
    public int actNum = 0;                  // �ړ��\��

    float move1HexLong = 1.0f;              // �P�}�X�̋���
    public float height = 0.2f;             // �}�X����̍���(�}�X��Y�ɂ�����{���Đ���)
    protected Vector3 OriginPos;            // ��������W
    public Hex Hex;                         // ������}�X�̏��
    public Hex OldHex;                      // �O�����}�X�̏��
    protected Hex OldHitHex;                // Ray�����������Ă���}�X���1�O�ɓ��������}�X�̏��
    CapsuleCollider col;                    // �J�v�Z���R���C�_�[
    RaycastHit hit;                         // �ړ��Ɏg�p
    RaycastHit hitDown;                     // �����ɂ���}�X�̎擾
    public bool bVillage = false;           // �����}�X�ɂ��邩�ǂ���
    public bool bMoveNumDisplay = false;    // �ړ��\�񐔂�\�������邩�ǂ���

    public GameObject effectObject;         // �t�F�[�Y�؂�ւ�莞�̃G�t�F�N�g
    private float deleteTime = 1.5f;        // �G�t�F�N�g�������܂ł̎���
    private float offsetY = -0.55f;         // �G�t�F�N�g�̍���

    // Start is called before the first frame update
    protected void Start()
    {
        col = GetComponent<CapsuleCollider>();  // �R���C�_�[�擾
        actNum = sta.moveNum;   // �ړ��\����ݒ�

        score.sprite = sta.sprite;
        score.motifName = sta.motifName;
        score.charName = sta.charName;
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    // �}�E�X�ł��񂾎��̑���
    void OnMouseDrag()
    {
        if (GameManager.instance.bMenuDisplay())    // �������j���[��\�����Ă��邩�ǂ���
        {
            if (bFriend && actNum > 0)
            {
                if(col.enabled)
                    GameManager.instance.SetCharacterUI(true, this);
                bMoveNumDisplay = true;
                col.enabled = false;
                var pos = transform.position;

                // �n�ʂ���ʒu���擾���Ĉړ�
                int mask = 1 << 6;  // Ray���}�X�ɂ���������Ȃ��悤�ɐݒ�
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, mask))
                {
                    pos = new Vector3(hit.point.x, hit.point.y + height, hit.point.z);
                    if (!(hit.transform.gameObject.CompareTag("Village") && bVillage))
                    {
                        float moveLimit = move1HexLong * sta.moveLong;
                        pos.x = Mathf.Clamp(pos.x, OriginPos.x - moveLimit, OriginPos.x + moveLimit);
                        pos.z = Mathf.Clamp(pos.z, OriginPos.z - moveLimit, OriginPos.z + moveLimit);
                    }
                }

                // �����ɂ���}�X���擾
                if (Physics.Raycast(pos, Vector3.down, out hitDown, 2.0f, mask))
                {
                    Hex hitHex = hitDown.transform.GetComponent<Hex>();
                    if (Hex != hitHex)
                    {
                        if (!Hex.bUnit && Hex.bDiscover)
                            OldHitHex = Hex;
                        Hex = hitHex;
                    }
                    Hex.SetCursol(true);
                }

                if (!OldHex)
                    OldHex = OldHitHex = Hex;

                // �ʒu��ݒ�
                transform.position = pos;
            }
        }
    }
    // �p�b�h�Œ͂�ł���Ƃ��̑���
    public void PadDrag(RaycastHit hit)
    {
        if (GameManager.instance.bMenuDisplay())
        {
            if (bFriend && actNum > 0)
            {
                if (col.enabled)
                    GameManager.instance.SetCharacterUI(true, this);
                bMoveNumDisplay = true;
                col.enabled = false;
                var pos = transform.position;

                pos = new Vector3(hit.point.x, hit.point.y + height, hit.point.z);
                if (!(hit.transform.gameObject.CompareTag("Village") && bVillage))
                {
                    float moveLimit = move1HexLong * sta.moveLong;
                    pos.x = Mathf.Clamp(pos.x, OriginPos.x - moveLimit, OriginPos.x + moveLimit);
                    pos.z = Mathf.Clamp(pos.z, OriginPos.z - moveLimit, OriginPos.z + moveLimit);
                }

                int mask = 1 << 6;
                if (Physics.Raycast(pos, Vector3.down, out hitDown, 2.0f, mask))
                {
                    Hex hitHex = hitDown.transform.GetComponent<Hex>();
                    if (Hex != hitHex)
                    {
                        if (!Hex.bUnit && Hex.bDiscover)
                            OldHitHex = Hex;
                        Hex = hitHex;
                    }
                    Hex.SetCursol(true);
                }

                if (!OldHex)
                    OldHex = OldHitHex = Hex;

                transform.position = pos;
            }
        }
    }

    // �}�E�X��������
    void OnMouseUp()
    {
        MouseUp();
    }

    // �͂�ł��郆�j�b�g����������̓���
    public void MouseUp()
    {
        if (GameManager.instance.bMenuDisplay())    // �������j���[��\�����Ă��邩�ǂ���
        {
            if (bFriend && actNum > 0)  // ���ԂŊ��܂��ړ��\��������Ƃ�
            {
                GameManager.instance.SetCharacterUI(false, this);
                col.enabled = true;
                if (Hex != OldHex)  // �O�ƍ��̃}�X�������łȂ����
                {
                    if (Hex.bDiscover)  // �}�X���������Ă���Ȃ�
                    {
                        if (Hex.bUnit && Hex.Unit != this)  // �}�X�Ƀ��j�b�g�����Ă��ꂪ�����łȂ��Ȃ�
                        {
                            if (!Hex.Unit.bFriend)  // �}�X�ɂ��郆�j�b�g�����ԂłȂ����
                                UpActUnit();
                            else
                                UpActSameHex();
                        }
                        else
                            UpActDefault();
                    }
                    else
                        UpActSameHex();
                }
                else
                    UpActSameHex();
            }
        }
    }

    // �ʏ퓮��
    void UpActDefault()
    {
        if (Hex.gameObject.CompareTag("Village"))
            bVillage = true;
        else
            bVillage = false;

        OriginPos = new Vector3(Hex.transform.position.x, Hex.transform.position.y + height, Hex.transform.position.z);
        transform.position = OriginPos;
        Hex.SetCursol(false);
        Hex.SetUnit(this);
        Hex.bGetMaterial = true;

        Vector3 origin = OldHex.transform.position;
        Vector3 target = Hex.transform.position;
        Vector3 direction = target - origin;
        float length = Vector3.Distance(origin, target);
        Ray ray = new Ray(origin, direction);
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(ray, sta.serchRange, length);
        for (int i = 0; i < hit.Length; i++)
        {
            Transform h = hit[i].transform;
            if (h.gameObject.CompareTag("Hex"))
            {
                if (h.GetComponent<Hex>().SetReverse())
                    score.reverseHexNum++;
            }
        }

        OldHex.DisUnit();
        OldHitHex = OldHex = Hex;

        actNum--;
        GameManager.instance.canActUnitNum--;
        bMoveNumDisplay = false;
    }
    // �����}�X�ɒu����
    void UpActSameHex()
    {
        OriginPos = new Vector3(OldHex.transform.position.x, OldHex.transform.position.y + height, OldHex.transform.position.z);
        transform.position = OriginPos;
        OldHex.SetCursol(false);
        OldHex.SetUnit(this);
        bMoveNumDisplay = false;
    }
    // �u�����}�X�Ƀ��j�b�g��������
    void UpActUnit()
    {
        if (OldHitHex.gameObject.CompareTag("Village"))
            bVillage = true;
        else
            bVillage = false;

        OriginPos = new Vector3(OldHitHex.transform.position.x, OldHitHex.transform.position.y + height, OldHitHex.transform.position.z);
        transform.position = OriginPos;
        OldHitHex.SetCursol(false);
        OldHitHex.SetUnit(this);
        OldHitHex.bGetMaterial = true;

        Vector3 origin = OldHitHex.transform.position;
        Vector3 target = OldHitHex.transform.position;
        Vector3 direction = target - origin;
        float length = Vector3.Distance(origin, target);
        Ray ray = new Ray(origin, direction);
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(ray, sta.serchRange, length);
        for (int i = 0; i < hit.Length; i++)
        {
            Transform h = hit[i].transform;
            if (h.gameObject.CompareTag("Hex"))
            {
                if (h.GetComponent<Hex>().SetReverse())
                    score.reverseHexNum++;
            }
        }

        OldHex.DisUnit();
        OldHex = OldHitHex;

        if(!Hex.Unit.bFriend)
            StartCoroutine(SelectButtons.instance.CorGetInput(Hex.Unit, this));

        actNum--;
        GameManager.instance.canActUnitNum--;
        bMoveNumDisplay = false;
    }
    // �u�����܂ܑf�ނ��l�����鎞
    public void ActMaterial()
    {
        if (bFriend && Hex.bMaterialHex)
        {
            Hex.bGetMaterial = true;
            actNum--;
            GameManager.instance.canActUnitNum--;
            bMoveNumDisplay = true;
        }
    }

    // �}�X��ݒ�
    public void SetHex(Hex hex)
    {
        if (hex.gameObject.CompareTag("Village"))
            bVillage = true;
        else
            bVillage = false;

        OldHex = OldHitHex = Hex = hex;
        OriginPos = new Vector3(Hex.transform.position.x, Hex.transform.position.y + height, Hex.transform.position.z);
        transform.position = OriginPos;
        Hex.SetCursol(false);
        Hex.SetUnit(this);
    }

    // ���Ԃɂ���Ƃ��̓���
    public bool BeMyFriend()
    {
        if (GameManager.instance.food >= sta.cost)
        {
            bFriend = true;
            GameManager.instance.food -= sta.cost;
            Hex.SetUnit(this);
            GameManager.instance.AddUnit(this);
            GameManager.instance.AddFriendCatNum(this);
            GameManager.instance.friendNum++;
            GameManager.instance.book.DiscoverCharacter(sta.number);

            return true;
        }
        return false;
    }

    // �^�[���؂�ւ�莞�̃��Z�b�g
    public void SetAct()
    {
        actNum = sta.moveNum;

        GameObject instantiateEffect = Instantiate(effectObject, transform.position + new Vector3(0f, offsetY, 0f), Quaternion.identity);
        Destroy(instantiateEffect, deleteTime);
    }

    // �폜���鎞�Ƀ}�X�ɐݒ肳��Ă��邱�̃��j�b�g���폜
    private void OnDestroy()
    {
        if (Hex)
        {
            Hex.DisUnit();
            OldHex.DisUnit();
        }
    }
}
