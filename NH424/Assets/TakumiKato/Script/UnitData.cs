//=============================================================================
//
// ユニットデータ クラス [UnitData.cpp]
//
//=============================================================================
using System;
using System.Collections.Generic;
using UnityEngine;

// タグ
public enum UnitTag
{
    主人公,
    ラーーーン,
    美食家,
    炭酸水は定期便,
    インターネット海を泳ぐ,
    蒼い炎,
    天然,
    秘訣は笑顔,
    キラッ,
    長寿の秘訣,
    可愛いもの好き,
    寝る子は育つ,
    最年長組,
    熱き友,
    モノづくり,
    ちっちゃいもんクラブ,
    村のアイドル,
    バンド,
    木陰で一休み,

    MAX,
}

[CreateAssetMenu(menuName = "ScriptableObject/UnitData")]
public class UnitData : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private GameObject initObj;          // キャラクターのオブジェクト
    [SerializeField] private int initNumber;              // ケモコごとのナンバー
    [SerializeField] private string initMotifName;        // キャラクターのモチーフ名
    [SerializeField] private string initCharName;         // キャラクターの名前
    [SerializeField] private UnitTag[] initUnitTag;       // 持っているタグ
    [SerializeField] private GameObject initSprite;       // キャラ画像

    [SerializeField] private float initFood = 30.0f;       // 食材獲得量
    [SerializeField] private float initWood = 30.0f;       // 木材獲得量
    [SerializeField] private float initStone = 30.0f;      // 石材獲得量
    [SerializeField] private float initIron = 30.0f;       // 鉄材獲得量

    [SerializeField] private UNIT_ACT initAbilityKind;    // 能力の種類
    [SerializeField] private float initCost = 50;              // 仲間にするのに必要な食材の数
    [SerializeField] private int initMoveLong = 2;            // 移動距離
    [SerializeField] private int initMoveNum = 2;             // 移動回数
    [SerializeField] private float initSerchRange = 0.1f;        // 探索範囲

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
        // ランタイムでの書き込み用に値をコピーする
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
