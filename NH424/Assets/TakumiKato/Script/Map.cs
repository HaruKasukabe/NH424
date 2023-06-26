//=============================================================================
//
// マップ クラス [Map.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance = null;
    public GameObject[,] map;   // マップ
    public List<GameObject> hexVillageList = new List<GameObject>();    // 村ヘクスのリスト

    [SerializeField] GameObject hexVillage;         // 村ヘクスのオブジェクト
    [SerializeField] GameObject[] hex;              // 全てのヘクスのオブジェクトをまとめた配列
    [SerializeField] float[] hexOdds;               // ヘクスのそれぞれの出る確率
    public GameObject[] iventHex;                   // イベントヘクスのオブジェクト
    public GameObject firstUnit;                    // 最初のユニット
    public GameObject[] unit;                       // 全てのユニットのオブジェクトをまとめた配列
    [SerializeField] GameObject[] house;            // 家のオブジェクトをまとめた配列
    List<Unit> wildUnitList = new List<Unit>();     // マップにいる野良のケモコリスト
    GameObject houseNow;                            // 今マップ上にある家オブジェクト
    int houseNum = 0;                               // 家管理番号
    [SerializeField] int mapSize = 30;              // マップの大きさ
    [SerializeField] int startRound = 5;            // ゲームスタート時のマップの大きさ(マップ周数)
    public int round;                               // 周数からのヘクスの数
    int centerNum;                                  // マップの中心座標
    public INT2[] centerNextNum = new INT2[6];      // マップの中心周囲６マスの座標
    [SerializeField] float hexSizeX = 5.0f;         // ヘクスの横の大きさ
    [SerializeField] float hexSizeZ = 5.0f;         // ヘクスの縦の大きさ
    float startPosEvenX = 0.0f;                     // 偶数列の最初のX座標
    float startPosOddX;                             // 奇数列の最初のX座標
    int unitId = 1;                                 // ユニット管理Id
    [SerializeField] int unitProbability = 10;      // 野良ケモコ生成確率

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Instantiate(firstUnit);     // 最初のユニットを生成
        GameManager.instance.AddFriendCatNum(firstUnit.GetComponent<Unit>());   // 最初のユニットを仲間にした種類リストに追加
        map = new GameObject[mapSize, mapSize];
        round = startRound * (6 + (startRound - 1) * 3) - 6;
        centerNum = mapSize / 2;

        // 中心の周囲の座標を設定
        if (GameManager.instance.IsEven(centerNum))
        {
            centerNextNum[0] = new INT2(centerNum - 1, centerNum);
            centerNextNum[1] = new INT2(centerNum - 1, centerNum + 1);
            centerNextNum[2] = new INT2(centerNum, centerNum + 1);
            centerNextNum[3] = new INT2(centerNum + 1, centerNum);
            centerNextNum[4] = new INT2(centerNum, centerNum - 1);
            centerNextNum[5] = new INT2(centerNum - 1, centerNum - 1);
        }
        else
        {
            centerNextNum[0] = new INT2(centerNum - 1, centerNum);
            centerNextNum[1] = new INT2(centerNum, centerNum + 1);
            centerNextNum[2] = new INT2(centerNum + 1, centerNum + 1);
            centerNextNum[3] = new INT2(centerNum + 1, centerNum);
            centerNextNum[4] = new INT2(centerNum, centerNum - 1);
            centerNextNum[5] = new INT2(centerNum + 1, centerNum - 1);
        }

        startPosOddX = startPosEvenX + hexSizeX / 2;

        FLOAT2 pos = new FLOAT2(startPosEvenX, startPosEvenX);
        for (int z = 0; z < mapSize; z++)
        {
            // 偶数か奇数かで最初のXが変わる
            if (GameManager.instance.IsEven(z))
                pos.x = startPosEvenX;
            else
                pos.x = startPosOddX;

            for (int x = 0; x < mapSize; x++)
            {
                // 中心座標
                if ((x == centerNum) && (z == centerNum))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();
                    hexVillageList.Add(map[x, z]);

                    // 家オブジェクトを生成
                    houseNow = Instantiate(house[0], new Vector3(pos.x + 3.0f, 0.2f, pos.z), Quaternion.identity);
                    houseNow.transform.Rotate(new Vector3(0, 90, 0));
                    houseNow.GetComponent<ObjectOnHex>().SetHex(hex);
                }
                // 中心から周囲１マスの座標
                else if (BCenterNext(new INT2(x, z)))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();
                    hexVillageList.Add(map[x, z]);
                }
                // それ以外
                else
                {
                    int hexNum = Choose(hexOdds);
                    map[x, z] = Instantiate(hex[hexNum], new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    map[x, z].GetComponent<Hex>().SetHexNum(new INT2(x, z));
                    map[x, z].SetActive(false);

                    // 確率で野良ケモコを生成
                    if(Random.Range(0, unitProbability) == 0)
                    {
                        GameObject obj = Instantiate(unit[Random.Range(0, unit.Length)], new Vector3(pos.x, 0.2f, pos.z), Quaternion.identity);
                        obj.transform.rotation = new Quaternion(0, 180, 0, 0);  // 初期は後ろを向いているので180度回転
                        Unit objUnit = obj.GetComponent<Unit>();
                        map[x, z].GetComponent<Hex>().SetStrayUnit(objUnit);
                        objUnit.id = unitId;
                        wildUnitList.Add(objUnit);
                        obj.SetActive(false);

                        unitId++;
                    }
                }
                pos.x += hexSizeX;
            }
            pos.z += hexSizeZ;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 中心を取得
    public GameObject GetCenter()
    {
        return map[centerNum, centerNum];
    }

    // ヘクスオブジェクトを取得
    public GameObject GetHex(INT2 num)
    {
        return map[num.x, num.z];
    }

    // ケモコがいない村マスを取得
    public Hex GetVillageHex()
    {
        Hex hex = map[centerNum, centerNum].GetComponent<Hex>();
        if (!hex.bUnit)
            return hex;

        for (int i = 0; i < centerNextNum.Length; i++)
        {
            hex = map[centerNextNum[i].x, centerNextNum[i].z].GetComponent<Hex>();
            if (!hex.bUnit)
                return hex;
        }

        return null;
    }

    // 中心の周りのマスかどうか判定
    bool BCenterNext(INT2 num)
    {
        for (int i = 0; i < centerNextNum.Length; i++)
            if (num.x == centerNextNum[i].x)
                if(num.z == centerNextNum[i].z)
                    return true;

        return false;
    }

    // ケモコのオブジェクトを取得
    public GameObject GetUnit(int number)
    {
        return unit[number];
    }

    // ケモコのイメージをランダムに取得
    public GameObject RandomGetUnitSprite()
    {
        return unit[Random.Range(0, unit.Length)].GetComponent<Unit>().sta.sprite;
    }

    // ユニット管理番号を取得
    public int GetUnitId()
    {
        unitId++;
        return unitId - 1;
    }

    // 家をレベルアップ
    public void LevelUpHouse()
    {
        houseNum++;
        Vector3 pos = houseNow.transform.position;
        Hex hex = houseNow.GetComponent<ObjectOnHex>().GetHex();
        Destroy(houseNow);
        if(houseNum == 1)
            houseNow = Instantiate(house[houseNum], new Vector3(pos.x - 3.0f, pos.y + 0.35f, pos.z), Quaternion.identity);
        else
            houseNow = Instantiate(house[houseNum], new Vector3(pos.x, pos.y + 0.1f, pos.z), Quaternion.identity);
        houseNow.transform.Rotate(new Vector3(0, 90, 0));
        houseNow.GetComponent<ObjectOnHex>().SetHex(hex);
    }

    // リセット時のマップ生成
    public void ResetMap()
    {
        // マップにいる野良ケモコを消す
        for(int i = 0; i < wildUnitList.Count; i++)
        {
            if (!wildUnitList[i].bFriend)
                Destroy(wildUnitList[i].gameObject);
        }
        wildUnitList.Clear();
        hexVillageList.Clear();
        round = (startRound + GameManager.instance.level * 2) * (6 + (startRound + GameManager.instance.level * 2 - 1) * 3) - 6;    // レベルに合わせて周数を計算
        startPosOddX = startPosEvenX + hexSizeX / 2;

        FLOAT2 pos = new FLOAT2(startPosEvenX, startPosEvenX);
        for (int z = 0; z < mapSize; z++)
        {
            if (GameManager.instance.IsEven(z))
                pos.x = startPosEvenX;
            else
                pos.x = startPosOddX;

            for (int x = 0; x < mapSize; x++)
            {
                if ((x == centerNum) && (z == centerNum))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();

                    houseNow.GetComponent<ObjectOnHex>().SetHex(hex);
                }
                else if (BCenterNext(new INT2(x, z)))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();
                    hexVillageList.Add(map[x, z]);
                }
                else
                {
                    int hexNum = Choose(hexOdds);
                    map[x, z] = Instantiate(hex[hexNum], new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    map[x, z].GetComponent<Hex>().SetHexNum(new INT2(x, z));
                    map[x, z].SetActive(false);

                    if (Random.Range(0, unitProbability) == 0)
                    {
                        GameObject obj = Instantiate(unit[Random.Range(0, unit.Length)], new Vector3(pos.x, 0.2f, pos.z), Quaternion.identity);
                        obj.transform.rotation = new Quaternion(0, 180, 0, 0);
                        Unit objUnit = obj.GetComponent<Unit>();
                        map[x, z].GetComponent<Hex>().SetStrayUnit(objUnit);
                        objUnit.id = unitId;
                        wildUnitList.Add(objUnit);
                        obj.SetActive(false);

                        unitId++;
                    }
                }
                pos.x += hexSizeX;
            }
            pos.z += hexSizeZ;
        }

        KemokoListOut.instance.SetVillageHex();     // 外にいるユニットを全員真ん中に集める


        Invoke("SetVillage", 1);            // 全てのマスが生成されてから村マスを生成
        SeasonEvent.instance.ResetMap();    // イベントマスを再設定
    }

    // ランダムにnum個のマスを取得
    public List<Hex> GetRandomHex(int num)
    {
        List<Hex> list = new List<Hex>();
        int x, z;

        while(list.Count < num)
        {
            x = Random.Range(0, mapSize);
            z = Random.Range(0, mapSize);
            if(list.Count > 0)
            {
                while (list[0].hexNum.x == x)
                    x = Random.Range(0, mapSize);
            }
            if (map[x, z].gameObject.activeSelf)
                if (!map[x, z].gameObject.CompareTag("Village"))
                    list.Add(map[x, z].GetComponent<Hex>());
        }

        return list;
    }

    // 重みを加えてランダムに取得
    int Choose(float[] probs)
    {

        float total = 0;

        //配列の要素を代入して重みの計算
        foreach (float elem in probs)
        {
            total += elem;
        }

        //重みの総数に0から1.0の乱数をかけて抽選を行う
        float randomPoint = Random.value * total;

        //iが配列の最大要素数になるまで繰り返す
        for (int i = 0; i < probs.Length; i++)
        {
            //ランダムポイントが重みより小さいなら
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                //ランダムポイントが重みより大きいならその値を引いて次の要素へ
                randomPoint -= probs[i];
            }
        }

        //乱数が１の時、配列数の-１＝要素の最後の値をChoose配列に戻している
        return probs.Length - 1;
    }

    // レベルに合わせて村マスを生成
    void SetVillage()
    {
        int villageNum = GameManager.instance.level;
        if (GameManager.instance.level >= GameManager.instance.maxVillageLevel)
            villageNum = GameManager.instance.maxVillageLevel;

        while (villageNum > 0)
        {
            for (int i = 0; i < hexVillageList.Count; i++)
            {
                VillageCollision village = hexVillageList[i].GetComponent<VillageCollision>();
                if (village.villageNum != villageNum)
                    village.SetVillage(villageNum);
            }

            villageNum--;
        }
    }
}