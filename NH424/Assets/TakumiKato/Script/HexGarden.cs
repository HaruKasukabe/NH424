using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGarden : Hex
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

        if(bUnit)
        {
            if (Unit.sta.abilityKind == UNIT_ACT.GARDEN)
                GameManager.instance.food += 5.0f;
            else
                GameManager.instance.food += 1.0f;
        }
    }
}
