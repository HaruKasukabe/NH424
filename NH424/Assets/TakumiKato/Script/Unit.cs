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

    public float height = 0.65f;
    protected Vector3 OriginPos;
    protected Hex Hex;
    protected Hex OldHex;
    CapsuleCollider col;
    RaycastHit hit;
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
        col.enabled = false;
        var pos = transform.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            pos = new Vector3(hit.point.x, hit.point.y + height, hit.point.z);
            if (!(hit.transform.gameObject.CompareTag("Village") && bVillage))
            {
                pos.x = Mathf.Clamp(pos.x, OriginPos.x - sta.moveLong, OriginPos.x + sta.moveLong);
                pos.z = Mathf.Clamp(pos.z, OriginPos.z - sta.moveLong, OriginPos.z + sta.moveLong);
            }
        }

        Hex = HexManager.instance.GetNearHex(pos).GetComponent<Hex>();
        if (!OldHex)
            OldHex = Hex;
        if (Hex)
            Hex.SetCursol(true);

        transform.position = pos;
    }

    void OnMouseUp()
    {
        if (Hex.bDiscover)
        {
            col.enabled = true;

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
                    if(h.GetComponent<Hex>().SetReverse())
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
        else
        {
            col.enabled = true;

            OriginPos = new Vector3(OldHex.transform.position.x, OldHex.transform.position.y + height, OldHex.transform.position.z);
            transform.position = OriginPos;
            OldHex.SetCursol(false);
            OldHex.SetUnit(this);
        }
    }
}
