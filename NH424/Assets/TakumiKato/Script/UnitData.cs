//=============================================================================
//
// ���j�b�g�f�[�^ �N���X [UnitData.cpp]
//
//=============================================================================
using System;
using System.Collections.Generic;
using UnityEngine;

// �^�O
public enum UnitTag
{
    ��l��,
    ���[�[�[��,
    ���H��,
    �Y�_���͒����,
    �C���^�[�l�b�g�C���j��,
    ������,
    �V�R,
    �錍�͏Ί�,
    �L���b,
    �����̔錍,
    �������̍D��,
    �Q��q�͈��,
    �ŔN���g,
    �M���F,
    ���m�Â���,
    �������Ⴂ����N���u,
    ���̃A�C�h��,
    �o���h,
    �؉A�ň�x��,

    MAX,
}

[CreateAssetMenu(menuName = "ScriptableObject/UnitData")]
public class UnitData : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private GameObject initObj;          // �L�����N�^�[�̃I�u�W�F�N�g
    [SerializeField] private int initNumber;              // �P���R���Ƃ̃i���o�[
    [SerializeField] private string initMotifName;        // �L�����N�^�[�̃��`�[�t��
    [SerializeField] private string initCharName;         // �L�����N�^�[�̖��O
    [SerializeField] private UnitTag[] initUnitTag;       // �����Ă���^�O
    [SerializeField] private GameObject initSprite;       // �L�����摜

    [SerializeField] private float initFood = 30.0f;       // �H�ފl����
    [SerializeField] private float initWood = 30.0f;       // �؍ފl����
    [SerializeField] private float initStone = 30.0f;      // �΍ފl����
    [SerializeField] private float initIron = 30.0f;       // �S�ފl����

    [SerializeField] private UNIT_ACT initAbilityKind;    // �\�͂̎��
    [SerializeField] private float initCost = 50;              // ���Ԃɂ���̂ɕK�v�ȐH�ނ̐�
    [SerializeField] private int initMoveLong = 2;            // �ړ�����
    [SerializeField] private int initMoveNum = 2;             // �ړ���
    [SerializeField] private float initSerchRange = 0.1f;        // �T���͈�

    [NonSerialized] public GameObject obj;
    [NonSerialized] public int number;
    [NonSerialized] public string motifName;
    [NonSerialized] public string charName;         
    [NonSerialized] public UnitTag[] unitTag;      
    [NonSerialized] public GameObject sprite;      

    [NonSerialized] public float food;      
    [NonSerialized] public float wood;       
    [NonSerialized] public float stone;
    [NonSerialized] public float iron;

    [NonSerialized] public UNIT_ACT abilityKind;   
    [NonSerialized] public float cost;             
    [NonSerialized] public int moveLong;
    [NonSerialized] public int moveNum;         
    [NonSerialized] public float serchRange;

    public void OnBeforeSerialize() { }
    public void OnAfterDeserialize()
    {
        // �����^�C���ł̏������ݗp�ɒl���R�s�[����
        obj = initObj;
        number = initNumber;
        motifName = initMotifName;
        charName = initCharName;
        unitTag = initUnitTag;
        sprite = initSprite;

        food = initFood;
        wood = initWood;
        stone = initStone;
        iron = initIron;

        abilityKind = initAbilityKind;
        cost = initCost;
        moveLong = initMoveLong;
        moveNum = initMoveNum;
        serchRange = initSerchRange;
    }
}
