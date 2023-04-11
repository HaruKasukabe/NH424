using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNIT_ACT
{
    GARDEN,
    FOREST,
    COAL_MINE,
    QUARRY,

    NULL,
}

public class Unit : MonoBehaviour
{
    public UnitData sta;
    public int id;
    float hp;
    public bool bFriend = false;
    public int actNum = 0;

    float move1HexLong = 1.0f;
    public float height = 0.2f;
    protected Vector3 OriginPos;
    public Hex Hex;
    public Hex OldHex;
    protected Hex OldHitHex;
    CapsuleCollider col;
    RaycastHit hit;
    RaycastHit hitDown;
    Vector3 oldHexa;
    public bool bVillage = false;
    public bool bMoveNumDisplay = false;

    public GameObject effectObject;
    private float deleteTime = 1.5f;
    private float offsetY = -0.55f;

    // Start is called before the first frame update
    protected void Start()
    {
        col = GetComponent<CapsuleCollider>();
        hp = sta.maxHp;
        actNum = sta.moveNum;
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    virtual public void Special()
    {

    }

    void OnMouseDrag()
    {
        if (GameManager.instance.bMenuDisplay())
        {
            if (bFriend && actNum > 0)
            {
                bMoveNumDisplay = true;
                col.enabled = false;
                var pos = transform.position;

                int mask = 1 << 6;
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

    void OnMouseUp()
    {
        MouseUp();
    }

    public void PadDrag(RaycastHit hit)
    {
        if (GameManager.instance.bMenuDisplay())
        {
            if (bFriend && actNum > 0)
            {
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

    public void MouseUp()
    {
        if (GameManager.instance.bMenuDisplay())
        {
            if (bFriend && actNum > 0)
            {
                col.enabled = true;
                if (Hex != OldHex)
                {
                    if (Hex.bDiscover)
                    {
                        if (Hex.bUnit && Hex.Unit != this)
                        {
                            if (!Hex.Unit.bFriend)
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

        Vector3 origin = OldHex.transform.position;
        Vector3 target = Hex.transform.position;
        Vector3 direction = target - origin;
        float length = Vector3.Distance(origin, target);
        Ray ray = new Ray(origin, direction);
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(ray, sta.serchRange, length);
        bool delHp = false;
        for (int i = 0; i < hit.Length; i++)
        {
            Transform h = hit[i].transform;
            if (h.gameObject.CompareTag("Hex"))
            {
                if (h.GetComponent<Hex>().SetReverse())
                    delHp = true;
            }
        }

        OldHex.DisUnit();
        OldHitHex = OldHex = Hex;

        if (delHp)
            hp -= 3.0f;
        else
            hp -= 1.0f;

        actNum--;
        GameManager.instance.canActUnitNum--;
        GameManager.instance.moveNumTotal--;
        bMoveNumDisplay = false;
    }
    void UpActSameHex()
    {
        OriginPos = new Vector3(OldHex.transform.position.x, OldHex.transform.position.y + height, OldHex.transform.position.z);
        transform.position = OriginPos;
        OldHex.SetCursol(false);
        OldHex.SetUnit(this);
        bMoveNumDisplay = false;
    }
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

        Vector3 origin = OldHitHex.transform.position;
        Vector3 target = OldHitHex.transform.position;
        Vector3 direction = target - origin;
        float length = Vector3.Distance(origin, target);
        Ray ray = new Ray(origin, direction);
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(ray, sta.serchRange, length);
        bool delHp = false;
        for (int i = 0; i < hit.Length; i++)
        {
            Transform h = hit[i].transform;
            if (h.gameObject.CompareTag("Hex"))
            {
                if (h.GetComponent<Hex>().SetReverse())
                    delHp = true;
            }
        }

        OldHex.DisUnit();
        OldHex = OldHitHex;

        if (delHp)
            hp -= 3.0f;
        else
            hp -= 1.0f;

        if(!Hex.Unit.bFriend)
        {
            StartCoroutine(SelectButtons.instance.CorGetInput(Hex.Unit));
        }

        actNum--;
        GameManager.instance.canActUnitNum--;
        GameManager.instance.moveNumTotal--;
        bMoveNumDisplay = false;
    }

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

    public void BeMyFriend()
    {
        if (GameManager.instance.food > sta.cost)
        {
            bFriend = true;
            GameManager.instance.food -= sta.cost;
            Hex.SetUnit(this);
            GameManager.instance.AddUnit(this);
            GameManager.instance.friendNum++;
        }
    }

    public void SetAct()
    {
        actNum = sta.moveNum;

        GameObject instantiateEffect = Instantiate(effectObject, transform.position + new Vector3(0f, offsetY, 0f), Quaternion.identity);
        Destroy(instantiateEffect, deleteTime);
    }

    private void OnDestroy()
    {
        if (Hex)
        {
            Hex.DisUnit();
            OldHex.DisUnit();
        }
    }
}
