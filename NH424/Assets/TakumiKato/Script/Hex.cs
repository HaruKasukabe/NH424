using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Unit Unit;
    public INT2 hexNum;
    public INT2[] nextNum = new INT2[6];

    protected Renderer rend;
    MeshRenderer mesh;

    int level;
    protected int turn = 0;
    public bool bUnit = false;
    bool bCursol = false;
    public bool bReverse = false;
    public bool bDiscover = false;
    bool bSetDiscover = false;
    bool bEnd = false;

    // Start is called before the first frame update
    protected void Awake()
    {
        rend = GetComponent<Renderer>();
        mesh = GetComponent<MeshRenderer>();
        mesh.material.EnableKeyword("_EMISSION");
        rend.material.color = new Color32(0, 0, 0, 1);
    }

    // Update is called once per frame
    protected void Update()
    {
        mesh.material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));

        if(bDiscover && !bReverse)
        {
            mesh.material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f));
        }
        if (bCursol)
        {
            mesh.material.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f));
            bCursol = false;
        }


        if (!bSetDiscover && bReverse)
        {
            for (int i = 0; i < nextNum.Length; i++)
            {
                Hex hex = Map.instance.GetHex(nextNum[i]).GetComponent<Hex>();
                hex.SetDiscover(true);
                if (hex.bUnit && hex.gameObject.activeSelf)
                    hex.Unit.gameObject.SetActive(true);
                bSetDiscover = true;
            }
        }

        if((level != GameManager.instance.level) || Map.instance.round > 0)
        {
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
    }

    protected void GetMaterial(UNIT_ACT act)
    {
        if (turn != GameManager.instance.nowTurn)
        {
            if (bUnit)
            {
                if (Unit.bFriend)
                {
                    switch (act)
                    {
                        case UNIT_ACT.COAL_MINE:
                            if (Unit.sta.abilityKind == UNIT_ACT.COAL_MINE)
                                GameManager.instance.iron += 100.0f;
                            else
                                GameManager.instance.iron += 50.0f;
                            break;
                        case UNIT_ACT.FOREST:
                            if (Unit.sta.abilityKind == UNIT_ACT.FOREST)
                                GameManager.instance.wood += 100.0f;
                            else
                                GameManager.instance.wood += 50.0f;
                            break;
                        case UNIT_ACT.GARDEN:
                            if (Unit.sta.abilityKind == UNIT_ACT.GARDEN)
                                GameManager.instance.food += 100.0f;
                            else
                                GameManager.instance.food += 50.0f;
                            break;
                        case UNIT_ACT.QUARRY:
                            if (Unit.sta.abilityKind == UNIT_ACT.QUARRY)
                                GameManager.instance.stone += 100.0f;
                            else
                                GameManager.instance.stone += 50.0f;
                            break;
                    }
                }
            }
            turn = GameManager.instance.nowTurn;
        }
    }
    public void SetCursol(bool b)
    {
        bCursol = b;
    }
    public void SetDiscover(bool b)
    {
        bDiscover = b;
    }
    public void SetEnd()
    {
        bEnd = true;
        level = GameManager.instance.level;
    }
    public void SetUnit(Unit unit)
    {
        if (bDiscover || bReverse)
        {
            Unit = unit;
            rend.material.color = new Color32(255, 255, 255, 1);
            bUnit = true;
            bReverse = true;
        }
    }
    public void SetStrayUnit(Unit unit)
    {
        Unit = unit;
        Unit.SetHex(this);
        bUnit = true;
    }
    public bool SetReverse()
    {
        if (!bReverse)
        {
            rend.material.color = new Color32(255, 255, 255, 1);
            bReverse = true;
            return true;
        }
        else
            return false;
    }
    public void DisUnit()
    {
        Unit = null;
        bUnit = false;
    }
    public void SetHexNum(INT2 num)
    {
        hexNum = num;
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
}
