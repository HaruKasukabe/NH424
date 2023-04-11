using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTag
{

}

[CreateAssetMenu(menuName = "ScriptableObject/UnitData")]
public class UnitData : ScriptableObject   //ScriptableObject���p������
{
    public GameObject obj;          // �L�����N�^�[�̃I�u�W�F�N�g
    public int number;              // �P���R���Ƃ̃i���o�[
    public string charName;         // �L�����N�^�[�̖��O
    public UnitTag[] unitTag;       // �����Ă���^�O
    public GameObject sprite;       // �L�����摜

    public float food = 30.0f;       // �H�ފl����
    public float wood = 30.0f;       // �؍ފl����
    public float stone = 30.0f;      // �΍ފl����
    public float iron = 30.0f;       // �S�ފl����

    public UNIT_ACT abilityKind;    // �\�͂̎��
    public float maxHp;             // �̗�
    public float cost;              // ���Ԃɂ���̂ɕK�v�ȐH�ނ̐�
    public int moveLong;            // �ړ�����
    public int moveNum;             // �ړ���
    public float serchRange;        // �T���͈�

    public void SpecialAbilitie()   // ����\��
    {

    }
}
