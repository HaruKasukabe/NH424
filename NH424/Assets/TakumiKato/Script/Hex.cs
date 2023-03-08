using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    protected Unit Unit;
    Renderer rend;
    MeshRenderer mesh;
    int level = 0;

    protected bool bUnit = false;
    bool bCursol = false;
    public bool bReverse = false;
    public bool bDiscover = false;
    bool bSetDiscover = false;

    // Start is called before the first frame update
    protected void Start()
    {
        rend = GetComponent<Renderer>();
        mesh = GetComponent<MeshRenderer>();
        mesh.material.EnableKeyword("_EMISSION");
        level = GameManager.instance.level;
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


        if (!bSetDiscover)
        {
            if (bReverse || level != GameManager.instance.level)
            {
                List<Transform> list = HexManager.instance.GetDiscoverHex(transform.position);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].GetComponent<Hex>().SetDiscover(true);
                    bSetDiscover = true;
                }
            }
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
    public void SetUnit(Unit unit)
    {
        if (bDiscover)
        {
            Unit = unit;
            rend.material.color = new Color32(255, 255, 255, 1);
            bUnit = true;
            bReverse = true;
        }
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
}
