using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUnit : Unit
{
    bool b = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        bFriend = true;
        bVillage = true;
        id = 0;
        GameManager.instance.AddUnit(this);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (!b)
        {
            GameObject g = Map.instance.GetCenter();
            OriginPos = new Vector3(g.transform.position.x, g.transform.position.y + height, g.transform.position.z);
            transform.position = OriginPos;
            b = true;
            OldHex = Hex = g.GetComponent<Hex>();
            Hex.SetUnit(this);
        }
    }
}
