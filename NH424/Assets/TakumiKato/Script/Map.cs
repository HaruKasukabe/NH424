using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct FLOAT2
{
    public float x;
    public float z;

    public FLOAT2(float a, float b)
    {
        x = a;
        z = b;
    }
};

public class Map : MonoBehaviour
{
    public GameObject FirstVillage;
    public GameObject originObject;
    [SerializeField] ObjectPool objectPool;
    public float nMapNum = 5.0f;
    public float startPos = 100.0f;
    int level = 0;
    FLOAT2[] pos;
    float x;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        x = z = startPos;
        pos = new FLOAT2[]
        {
            new FLOAT2(-nMapNum,  nMapNum),
            new FLOAT2( nMapNum,  nMapNum),
            new FLOAT2( nMapNum, -nMapNum),
            new FLOAT2(-nMapNum, -nMapNum)
        };

        Instantiate(FirstVillage, new Vector3(x + 2.0f, 0.0f, z + 3.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (level != GameManager.instance.level)
        {
            level = GameManager.instance.level;

            for (int i = 0; i < 4; i++)
            {
                x = startPos + pos[i].x * level;
                z = startPos + pos[i].z * level;

                //Instantiate(originObject, new Vector3(x, 0.0f, z), Quaternion.identity);
                objectPool.Launch(new Vector3(x, 0.0f, z));

                for (int j = 0; j < level * 2 - 1; j++)
                {
                    int times = j + 1;
                    switch(i)
                    {
                        case 0:
                            //Instantiate(originObject, new Vector3(x + nMapNum * times, 0.0f, z), Quaternion.identity);
                            objectPool.Launch(new Vector3(x + nMapNum * times, 0.0f, z));
                            break;
                        case 1:
                            //Instantiate(originObject, new Vector3(x, 0.0f, z - nMapNum * times), Quaternion.identity);
                            objectPool.Launch(new Vector3(x, 0.0f, z - nMapNum * times));
                            break;
                        case 2:
                            //Instantiate(originObject, new Vector3(x - nMapNum * times, 0.0f, z), Quaternion.identity);
                            objectPool.Launch(new Vector3(x - nMapNum * times, 0.0f, z));
                            break;
                        case 3:
                            //Instantiate(originObject, new Vector3(x, 0.0f, z + nMapNum * times), Quaternion.identity);
                            objectPool.Launch(new Vector3(x, 0.0f, z + nMapNum * times));
                            break;
                    }
                }
            }
        }
    }
}
