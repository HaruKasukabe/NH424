using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexQuarry : Hex
{
    GameObject child;
    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();

        child = transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        GetMaterial(UNIT_ACT.QUARRY);
        if (bReverse)
            child.SetActive(true);
    }
}
