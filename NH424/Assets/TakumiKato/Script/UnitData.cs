using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/UnitData")]
public class UnitData : ScriptableObject   //ScriptableObjectを継承する
{
    public string id;          //登録ID

    public string charName;         //キャラクターの名前

    public UNIT_ACT abilityKind;    // 能力の種類
    public float maxHp;             // 体力
    public float cost;              // 仲間にするのに必要な食材の数
    public float moveLong;          // 移動距離
    public float serchRange;        // 探索範囲

    public void SpecialAbilitie()   // 特殊能力
    {

    }
}
