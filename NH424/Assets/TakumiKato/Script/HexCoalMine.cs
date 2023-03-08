using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoalMine : Hex
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (bUnit)
        {
            if (Unit.sta.abilityKind == UNIT_ACT.COAL_MINE)
                GameManager.instance.iron += 5.0f;
            else
                GameManager.instance.iron += 1.0f;
        }
    }
}
