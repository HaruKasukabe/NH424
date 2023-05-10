using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance = null;
    public GameObject[,] map;

    public GameObject hexVillage;
    public GameObject[] hex;
    public float[] hexOdds;
    public GameObject firstUnit;
    public GameObject[] unit;
    public GameObject[] house;
    public List<Unit> wildUnitList = new List<Unit>();
    GameObject houseNow;
    int houseNum = 0;
    public int mapSize = 30;
    public int startRound = 5;
    public int round;
    int centerNum;
    public INT2[] centerNextNum = new INT2[6];
    public float hexSizeX = 5.0f;
    public float hexSizeZ = 5.0f;
    public float startPos2 = 0.0f;
    float startPosOddX;
    int unitId = 1;
    public int unitProbability = 10;
    int resetHexNum = 55;

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
        Instantiate(firstUnit);
        GameManager.instance.AddFriendCatNum(firstUnit.GetComponent<Unit>());
        map = new GameObject[mapSize, mapSize];
        round = startRound * (6 + (startRound - 1) * 3) - 6;
        centerNum = mapSize / 2;
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
        startPosOddX = startPos2 + hexSizeX / 2;

        FLOAT2 pos = new FLOAT2(startPos2, startPos2);
        for (int z = 0; z < mapSize; z++)
        {
            if (GameManager.instance.IsEven(z))
                pos.x = startPos2;
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

                    houseNow = Instantiate(house[0], new Vector3(pos.x + 3.0f, 0.2f, pos.z), Quaternion.identity);
                    houseNow.transform.Rotate(new Vector3(0, 90, 0));
                    houseNow.GetComponent<ObjectOnHex>().SetHex(hex);
                }
                else if (BCenter(new INT2(x, z)))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();
                }
                else
                {
                    //int hexNum = Random.Range(0, hex.Length);
                    int hexNum = Choose(hexOdds);
                    map[x, z] = Instantiate(hex[hexNum], new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    map[x, z].GetComponent<Hex>().SetHexNum(new INT2(x, z));
                    map[x, z].SetActive(false);

                    if(Random.Range(0, unitProbability) == 0)
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject GetCenter()
    {
        return map[centerNum, centerNum];
    }
    public GameObject GetHex(INT2 num)
    {
        return map[num.x, num.z];
    }
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
    bool BCenter(INT2 num)
    {
        for (int i = 0; i < centerNextNum.Length; i++)
            if (num.x == centerNextNum[i].x)
                if(num.z == centerNextNum[i].z)
                    return true;

        return false;
    }
    public GameObject GetUnit(int number)
    {
        return unit[number];
    }
    public GameObject RandomGetUnitSprite()
    {
        return unit[Random.Range(0, unit.Length)].GetComponent<Unit>().sta.sprite;
    }
    public int GetUnitId()
    {
        unitId++;
        return unitId - 1;
    }
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
    public void ResetMap()
    {
        //map = new GameObject[mapSize, mapSize];
        for(int i = 0; i < wildUnitList.Count; i++)
        {
            if (!wildUnitList[i].bFriend)
                Destroy(wildUnitList[i].gameObject);
        }
        wildUnitList.Clear();
        round = startRound * (6 + (startRound - 1) * 3) - 6;
        startPosOddX = startPos2 + hexSizeX / 2;
        resetHexNum = 55;

        FLOAT2 pos = new FLOAT2(startPos2, startPos2);
        for (int z = 0; z < mapSize; z++)
        {
            if (GameManager.instance.IsEven(z))
                pos.x = startPos2;
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
                }
                else if (BCenter(new INT2(x, z)))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();
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

        KemokoListOut.instance.SetVillageHex();
    }
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
}