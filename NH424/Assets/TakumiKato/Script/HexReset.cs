using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexReset : Hex
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

        if(bUnit && Unit.bFriend)
        {
            DestroyObjects(Map.instance.map);
            Map.instance.ResetMap();
        }
    }

    private void DestroyObjects(GameObject[,] objects)
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}
