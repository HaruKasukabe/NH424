//=============================================================================
//
// ヘクス クラス [Hex.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Unit Unit;                           // 今置かれているユニット
    public INT2 hexNum;                         // このマスのマップ座標
    public INT2[] nextNum = new INT2[6];        // 隣接しているマスのマップ座標

    protected Renderer rend;
    MeshRenderer mesh;

    int level;
    protected int pickTime = 3;                 // 素材獲得可能回数
    public bool bUnit = false;                  // ユニットが上にいるか
    bool bCursol = false;                       // カーソルがマスの上にあるか
    public bool bReverse = false;               // このマスが開けられているか
    public bool bDiscover = false;              // このマスが発見されているか
    bool bSetDiscover = false;                  // 周りのマスを発見設定にしたか
    bool bEnd = false;                          // このマスがマップの端であるか
    bool bSetNextHex = false;                   // マップ拡張２週目をするか
    public bool bMaterialHex = false;           // 素材マスかどうか
    public bool bGarden = false;                // 食材マスかどうか
    public bool bGetMaterial = false;           // true時に素材獲得
    [SerializeField] GameObject normalHex;      // 通常ヘクスオブジェクト

    [SerializeField] GameObject effectObject;   // ユニットを置いたときのエフェクト
    float deleteTime = 1;                       // 消すまでの時間
    float offsetY = 0.15f;                      // 高さ

    [SerializeField] GameObject iventEffect;    // イベントマスに設定された時のエフェクト
    GameObject iventEffectNow;                  // 今あるイベントエフェクト
    float iventOffsetY = -1.25f;                // 高さ
    float iventMaterial = 30.0f;                // イベントマスの素材獲得量


    // Start is called before the first frame update
    protected void Awake()
    {
        rend = GetComponent<Renderer>();
        mesh = GetComponent<MeshRenderer>();
        mesh.material.EnableKeyword("_EMISSION");
        rend.material.color = new Color32(0, 0, 0, 1); // 黒(発見もされていない状態)
    }

    // Update is called once per frame
    protected void Update()
    {
        mesh.material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));

        // 発見された場合
        if(bDiscover && !bReverse)
        {
            mesh.material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f));  // 薄い白
        }
        // カーソルがある場合
        if (bCursol)
        {
            mesh.material.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f));  // 白
            bCursol = false;
        }

        // 隣接しているマスを発見設定にしていない且つ反転されている場合
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
                        Tutorial.instance.No_Kemoko();  // 未仲間のチュートリアル表示
                    }
                bSetDiscover = true;
            }
        }

        // レベルアップした時　or  最初のマップ生成するのラウンド数がまだあるとき
        if ((level != GameManager.instance.level) || Map.instance.round > 0)
            SetNextHex2();
        else if (bSetNextHex)
            SetNextHex();
    }

    // 素材獲得
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
                            if(Unit.sta.unitTag[i] == UnitTag.熱き友)
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
                pickTime--;             // 素材獲得可能回数-1

                // 素材獲得可能回数が0以下の時
                if (pickTime < 1)
                    ChangeNormalHex();  // 通常ヘクスに切り替え
            }
        }
    }

    // マップ拡張
    void SetNextHex()
    {
        // マップ端のマスならば
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
    // マップ拡張２
    void SetNextHex2()
    {
        // マップ端のマスならば
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

    // 通常ヘクスに切り替え
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

    // イベントヘクスに切り替え(0:春、1:冬)
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

    // カーソルがあるかを設定
    public void SetCursol(bool b)
    {
        bCursol = b;
    }

    // マップの終端に設定
    public void SetEnd()
    {
        bEnd = true;
        level = GameManager.instance.level;
    }

    // ユニットの情報を設定
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
    // 未仲間のユニットの情報を設定
    public void SetStrayUnit(Unit unit)
    {
        Unit = unit;
        Unit.SetHex(this);
        bUnit = true;
    }

    // マスをあける
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

    // 上からユニットがいなくなった時にユニットの情報をなくす
    public void DisUnit()
    {
        Unit = null;
        bUnit = false;
    }

    // 周囲のヘクス番号を設定
    public void SetHexNum(INT2 num)
    {
        hexNum = num;

        // 偶数か判定
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

    // イベントマスエフェクトを生成、削除
    public void SetEventHex(bool b)
    {
        if (b)
            iventEffectNow = Instantiate(iventEffect, transform.position + new Vector3(0f, iventOffsetY, 0f), Quaternion.identity);
        else
            Destroy(iventEffectNow);
    }
}
