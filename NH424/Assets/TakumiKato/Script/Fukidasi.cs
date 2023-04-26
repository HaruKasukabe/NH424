using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fukidasi : MonoBehaviour
{
    Unit unit;
    GameObject obj;
    bool bFriend = false;

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponentInParent<Unit>();
        obj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (unit.bFriend && !bFriend)
        {
            obj.SetActive(false);
            bFriend = true;
        }
    }
}
