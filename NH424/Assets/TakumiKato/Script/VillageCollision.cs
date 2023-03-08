using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageCollision : MonoBehaviour
{
    int level;
    int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.instance.level;
    }

    // Update is called once per frame
    void Update()
    {
        if(num < 2)
        {
            if (level != GameManager.instance.level)
            {
                List<Transform> hexList = HexManager.instance.GetVillageHex(transform.position);
                for (int i = 0; i < hexList.Count; i++)
                {
                    Vector3 pos = hexList[i].transform.position;
                    HexManager.instance.AddVillage(Instantiate(this.gameObject, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity).transform, 0);
                    Destroy(hexList[i].gameObject);
                    
                }
                num++;
            }
        }
    }
}