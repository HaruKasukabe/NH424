using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTag
{

}

[CreateAssetMenu(menuName = "ScriptableObject/UnitData")]
public class UnitData : ScriptableObject   //ScriptableObjectを継承する
{
    public GameObject obj;          // キャラクターのオブジェクト
    public int number;              // ケモコごとのナンバー
    public string charName;         // キャラクターの名前
    public UnitTag[] unitTag;       // 持っているタグ
    public GameObject sprite;       // キャラ画像

    public float food = 30.0f;       // 食材獲得量
    public float wood = 30.0f;       // 木材獲得量
    public float stone = 30.0f;      // 石材獲得量
    public float iron = 30.0f;       // 鉄材獲得量

    public UNIT_ACT abilityKind;    // 能力の種類
    public float maxHp;             // 体力
    public float cost;              // 仲間にするのに必要な食材の数
    public int moveLong;            // 移動距離
    public int moveNum;             // 移動回数
    public float serchRange;        // 探索範囲

    public void SpecialAbilitie()   // 特殊能力
    {

    }
}
