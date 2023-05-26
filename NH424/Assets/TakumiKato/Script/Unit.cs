using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNIT_ACT    // 獲得が得意な素材　※今不使用
{
    GARDEN,
    FOREST,
    COAL_MINE,
    QUARRY,

    NULL,
}
public struct UNIT_SCORE
{
    public int number;              // 管理番号
    public GameObject sprite;       // ユニットの画像
    public string motifName;        // キャラクターのモチーフ名
    public string charName;         // キャラクターの名前
    public int friendNum;           // このユニットが仲間を増やした数
    public int reverseHexNum;       // ひっくり返したマスの数
    public float food;              // 今まで獲得した食材
    public float stone;             // 今まで獲得した石材
    public float wood;              // 今まで獲得した木材
    public float iron;              // 今まで獲得した鉄材
}

public class Unit : MonoBehaviour
{
    public UnitData sta;                    // 基本ステータス
    public int id;                          // ユニット種類ごとのID
    public bool bFriend = false;            // 仲間かどうか
    public UNIT_SCORE score;                // スコア
    public int actNum = 0;                  // 移動可能回数

    float move1HexLong = 1.0f;              // １マスの距離
    public float height = 0.2f;             // マスからの高さ(マスのYにこれを＋して生成)
    protected Vector3 OriginPos;            // 今いる座標
    public Hex Hex;                         // 今いるマスの情報
    public Hex OldHex;                      // 前いたマスの情報
    protected Hex OldHitHex;                // Rayが今当たっているマスより1つ前に当たったマスの情報
    CapsuleCollider col;                    // カプセルコライダー
    RaycastHit hit;                         // 移動に使用
    RaycastHit hitDown;                     // 今下にあるマスの取得
    public bool bVillage = false;           // 今村マスにいるかどうか
    public bool bMoveNumDisplay = false;    // 移動可能回数を表示させるかどうか

    public GameObject effectObject;         // フェーズ切り替わり時のエフェクト
    private float deleteTime = 1.5f;        // エフェクトを消すまでの時間
    private float offsetY = -0.55f;         // エフェクトの高さ

    // Start is called before the first frame update
    protected void Start()
    {
        col = GetComponent<CapsuleCollider>();  // コライダー取得
        actNum = sta.moveNum;   // 移動可能数を設定

        score.sprite = sta.sprite;
        score.motifName = sta.motifName;
        score.charName = sta.charName;
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    // マウスでつかんだ時の操作
    void OnMouseDrag()
    {
        if (GameManager.instance.bMenuDisplay())    // 何かメニューを表示しているかどうか
        {
            if (bFriend && actNum > 0)
            {
                if(col.enabled)
                    GameManager.instance.SetCharacterUI(true, this);
                bMoveNumDisplay = true;
                col.enabled = false;
                var pos = transform.position;

                // 地面から位置を取得して移動
                int mask = 1 << 6;  // Rayがマスにしか当たらないように設定
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

                // 今下にあるマスを取得
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

                // 位置を設定
                transform.position = pos;
            }
        }
    }
    // パッドで掴んでいるときの操作
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

    // マウス離した時
    void OnMouseUp()
    {
        MouseUp();
    }

    // 掴んでいるユニットを放した時の動作
    public void MouseUp()
    {
        if (GameManager.instance.bMenuDisplay())    // 何かメニューを表示しているかどうか
        {
            if (bFriend && actNum > 0)  // 仲間で且つまだ移動可能数があるとき
            {
                GameManager.instance.SetCharacterUI(false, this);
                col.enabled = true;
                if (Hex != OldHex)  // 前と今のマスが同じでなければ
                {
                    if (Hex.bDiscover)  // マスが見つかっているなら
                    {
                        if (Hex.bUnit && Hex.Unit != this)  // マスにユニットがいてそれが自分でないなら
                        {
                            if (!Hex.Unit.bFriend)  // マスにいるユニットが仲間でなければ
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

    // 通常動作
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
    // 同じマスに置く時
    void UpActSameHex()
    {
        OriginPos = new Vector3(OldHex.transform.position.x, OldHex.transform.position.y + height, OldHex.transform.position.z);
        transform.position = OriginPos;
        OldHex.SetCursol(false);
        OldHex.SetUnit(this);
        bMoveNumDisplay = false;
    }
    // 置いたマスにユニットがいた時
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
    // 置いたまま素材を獲得する時
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

    // マスを設定
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

    // 仲間にするときの動作
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

    // ターン切り替わり時のリセット
    public void SetAct()
    {
        actNum = sta.moveNum;

        GameObject instantiateEffect = Instantiate(effectObject, transform.position + new Vector3(0f, offsetY, 0f), Quaternion.identity);
        Destroy(instantiateEffect, deleteTime);
    }

    // 削除する時にマスに設定されているこのユニットを削除
    private void OnDestroy()
    {
        if (Hex)
        {
            Hex.DisUnit();
            OldHex.DisUnit();
        }
    }
}
