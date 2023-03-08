using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexVillage : Hex
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 1);
        bReverse = true;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
