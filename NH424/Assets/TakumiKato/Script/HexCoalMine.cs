using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoalMine : Hex
{
    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        GetMaterial(UNIT_ACT.COAL_MINE);
    }
}
