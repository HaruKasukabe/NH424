using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/UnitData")]
public class UnitData : ScriptableObject   //ScriptableObject���p������
{
    public string id;          //�o�^ID

    public string charName;         //�L�����N�^�[�̖��O

    public UNIT_ACT abilityKind;    // �\�͂̎��
    public float maxHp;             // �̗�
    public float cost;              // ���Ԃɂ���̂ɕK�v�ȐH�ނ̐�
    public float moveLong;          // �ړ�����
    public float serchRange;        // �T���͈�

    public void SpecialAbilitie()   // ����\��
    {

    }
}
