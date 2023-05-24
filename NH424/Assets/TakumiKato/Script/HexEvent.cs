﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEvent : Hex
{
    GameObject child;
    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        bMaterialHex = true;
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);
        pickTime = 1;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        GetMaterial(UNIT_ACT.NULL);
        if (bReverse)
            child.SetActive(true);
    }
}