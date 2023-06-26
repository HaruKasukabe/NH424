//=============================================================================
//
// �}�b�v �N���X [Map.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance = null;
    public GameObject[,] map;   // �}�b�v
    public List<GameObject> hexVillageList = new List<GameObject>();    // ���w�N�X�̃��X�g

    [SerializeField] GameObject hexVillage;         // ���w�N�X�̃I�u�W�F�N�g
    [SerializeField] GameObject[] hex;              // �S�Ẵw�N�X�̃I�u�W�F�N�g���܂Ƃ߂��z��
    [SerializeField] float[] hexOdds;               // �w�N�X�̂��ꂼ��̏o��m��
    public GameObject[] iventHex;                   // �C�x���g�w�N�X�̃I�u�W�F�N�g
    public GameObject firstUnit;                    // �ŏ��̃��j�b�g
    public GameObject[] unit;                       // �S�Ẵ��j�b�g�̃I�u�W�F�N�g���܂Ƃ߂��z��
    [SerializeField] GameObject[] house;            // �Ƃ̃I�u�W�F�N�g���܂Ƃ߂��z��
    List<Unit> wildUnitList = new List<Unit>();     // �}�b�v�ɂ����ǂ̃P���R���X�g
    GameObject houseNow;                            // ���}�b�v��ɂ���ƃI�u�W�F�N�g
    int houseNum = 0;                               // �ƊǗ��ԍ�
    [SerializeField] int mapSize = 30;              // �}�b�v�̑傫��
    [SerializeField] int startRound = 5;            // �Q�[���X�^�[�g���̃}�b�v�̑傫��(�}�b�v����)
    public int round;                               // ��������̃w�N�X�̐�
    int centerNum;                                  // �}�b�v�̒��S���W
    public INT2[] centerNextNum = new INT2[6];      // �}�b�v�̒��S���͂U�}�X�̍��W
    [SerializeField] float hexSizeX = 5.0f;         // �w�N�X�̉��̑傫��
    [SerializeField] float hexSizeZ = 5.0f;         // �w�N�X�̏c�̑傫��
    float startPosEvenX = 0.0f;                     // ������̍ŏ���X���W
    float startPosOddX;                             // ���̍ŏ���X���W
    int unitId = 1;                                 // ���j�b�g�Ǘ�Id
    [SerializeField] int unitProbability = 10;      // ��ǃP���R�����m��

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
        Instantiate(firstUnit);     // �ŏ��̃��j�b�g�𐶐�
        GameManager.instance.AddFriendCatNum(firstUnit.GetComponent<Unit>());   // �ŏ��̃��j�b�g�𒇊Ԃɂ�����ރ��X�g�ɒǉ�
        map = new GameObject[mapSize, mapSize];
        round = startRound * (6 + (startRound - 1) * 3) - 6;
        centerNum = mapSize / 2;

        // ���S�̎��͂̍��W��ݒ�
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
            // ����������ōŏ���X���ς��
            if (GameManager.instance.IsEven(z))
                pos.x = startPosEvenX;
            else
                pos.x = startPosOddX;

            for (int x = 0; x < mapSize; x++)
            {
                // ���S���W
                if ((x == centerNum) && (z == centerNum))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();
                    hexVillageList.Add(map[x, z]);

                    // �ƃI�u�W�F�N�g�𐶐�
                    houseNow = Instantiate(house[0], new Vector3(pos.x + 3.0f, 0.2f, pos.z), Quaternion.identity);
                    houseNow.transform.Rotate(new Vector3(0, 90, 0));
                    houseNow.GetComponent<ObjectOnHex>().SetHex(hex);
                }
                // ���S������͂P�}�X�̍��W
                else if (BCenterNext(new INT2(x, z)))
                {
                    map[x, z] = Instantiate(hexVillage, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    Hex hex = map[x, z].GetComponent<Hex>();
                    hex.SetHexNum(new INT2(x, z));
                    hex.SetEnd();
                    hexVillageList.Add(map[x, z]);
                }
                // ����ȊO
                else
                {
                    int hexNum = Choose(hexOdds);
                    map[x, z] = Instantiate(hex[hexNum], new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
                    map[x, z].GetComponent<Hex>().SetHexNum(new INT2(x, z));
                    map[x, z].SetActive(false);

                    // �m���Ŗ�ǃP���R�𐶐�
                    if(Random.Range(0, unitProbability) == 0)
                    {
                        GameObject obj = Instantiate(unit[Random.Range(0, unit.Length)], new Vector3(pos.x, 0.2f, pos.z), Quaternion.identity);
                        obj.transform.rotation = new Quaternion(0, 180, 0, 0);  // �����͌��������Ă���̂�180�x��]
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

    // ���S���擾
    public GameObject GetCenter()
    {
        return map[centerNum, centerNum];
    }

    // �w�N�X�I�u�W�F�N�g���擾
    public GameObject GetHex(INT2 num)
    {
        return map[num.x, num.z];
    }

    // �P���R�����Ȃ����}�X���擾
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

    // ���S�̎���̃}�X���ǂ�������
    bool BCenterNext(INT2 num)
    {
        for (int i = 0; i < centerNextNum.Length; i++)
            if (num.x == centerNextNum[i].x)
                if(num.z == centerNextNum[i].z)
                    return true;

        return false;
    }

    // �P���R�̃I�u�W�F�N�g���擾
    public GameObject GetUnit(int number)
    {
        return unit[number];
    }

    // �P���R�̃C���[�W�������_���Ɏ擾
    public GameObject RandomGetUnitSprite()
    {
        return unit[Random.Range(0, unit.Length)].GetComponent<Unit>().sta.sprite;
    }

    // ���j�b�g�Ǘ��ԍ����擾
    public int GetUnitId()
    {
        unitId++;
        return unitId - 1;
    }

    // �Ƃ����x���A�b�v
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

    // ���Z�b�g���̃}�b�v����
    public void ResetMap()
    {
        // �}�b�v�ɂ����ǃP���R������
        for(int i = 0; i < wildUnitList.Count; i++)
        {
            if (!wildUnitList[i].bFriend)
                Destroy(wildUnitList[i].gameObject);
        }
        wildUnitList.Clear();
        hexVillageList.Clear();
        round = (startRound + GameManager.instance.level * 2) * (6 + (startRound + GameManager.instance.level * 2 - 1) * 3) - 6;    // ���x���ɍ��킹�Ď������v�Z
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

        KemokoListOut.instance.SetVillageHex();     // �O�ɂ��郆�j�b�g��S���^�񒆂ɏW�߂�


        Invoke("SetVillage", 1);            // �S�Ẵ}�X����������Ă��瑺�}�X�𐶐�
        SeasonEvent.instance.ResetMap();    // �C�x���g�}�X���Đݒ�
    }

    // �����_����num�̃}�X���擾
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

    // �d�݂������ă����_���Ɏ擾
    int Choose(float[] probs)
    {

        float total = 0;

        //�z��̗v�f�������ďd�݂̌v�Z
        foreach (float elem in probs)
        {
            total += elem;
        }

        //�d�݂̑�����0����1.0�̗����������Ē��I���s��
        float randomPoint = Random.value * total;

        //i���z��̍ő�v�f���ɂȂ�܂ŌJ��Ԃ�
        for (int i = 0; i < probs.Length; i++)
        {
            //�����_���|�C���g���d�݂�菬�����Ȃ�
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                //�����_���|�C���g���d�݂��傫���Ȃ炻�̒l�������Ď��̗v�f��
                randomPoint -= probs[i];
            }
        }

        //�������P�̎��A�z�񐔂�-�P���v�f�̍Ō�̒l��Choose�z��ɖ߂��Ă���
        return probs.Length - 1;
    }

    // ���x���ɍ��킹�đ��}�X�𐶐�
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