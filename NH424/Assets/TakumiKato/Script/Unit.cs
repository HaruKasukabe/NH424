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
    float hp;
    public bool bFriend = false;
    protected bool bAct = false;

    public float height = 0.65f;
    protected Vector3 OriginPos;
    protected Hex Hex;
    protected Hex OldHex;
    protected Hex OldHitHex;
    CapsuleCollider col;
    RaycastHit hit;
    RaycastHit hitDown;
    Vector3 oldHexa;
    public bool bVillage = false;

    // Start is called before the first frame update
    protected void Start()
    {
        col = GetComponent<CapsuleCollider>();
        hp = sta.maxHp;
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
        if (bFriend && !bAct)
        {
            col.enabled = false;
            var pos = transform.position;

            int mask = 1 << 6;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, mask))
            {
                pos = new Vector3(hit.point.x, hit.point.y + height, hit.point.z);
                if (!(hit.transform.gameObject.CompareTag("Village") && bVillage))
                {
                    pos.x = Mathf.Clamp(pos.x, OriginPos.x - sta.moveLong, OriginPos.x + sta.moveLong);
                    pos.z = Mathf.Clamp(pos.z, OriginPos.z - sta.moveLong, OriginPos.z + sta.moveLong);
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

    void OnMouseUp()
    {
        if (bFriend && !bAct)
        {
            col.enabled = true;
            if (Hex != OldHex)
            {
                if (Hex.bDiscover)
                {

                    if (Hex.bUnit && Hex.Unit != this)
                    {
                        UpActUnit();
                    }
                    else
                        UpActDefault();

                    bAct = true;
                    GameManager.instance.canActUnitNum--;
                }
                else
                    UpActSameHex();
            }
            else
                UpActSameHex();
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
        OldHex = Hex;

        if (delHp)
            hp -= 3.0f;
        else
            hp -= 1.0f;
    }
    void UpActSameHex()
    {
        OriginPos = new Vector3(OldHex.transform.position.x, OldHex.transform.position.y + height, OldHex.transform.position.z);
        transform.position = OriginPos;
        OldHex.SetCursol(false);
        OldHex.SetUnit(this);
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
    }

    public void SetHex(Hex hex)
    {
        if (hex.gameObject.CompareTag("Village"))
            bVillage = true;
        else
            bVillage = false;

        OldHex = OldHitHex = Hex = hex;
    }

    public void BeMyFriend()
    {
        if (GameManager.instance.food > sta.cost)
        {
            bFriend = true;
            GameManager.instance.food -= sta.cost;
            Hex.SetUnit(this);
            GameManager.instance.AddUnit(this);
        }
    }

    public void SetAct()
    {
        bAct = false;
    }
}
