using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //public GameObject originObject;
    //public int nMapX;
    //public int nMapY;
    //public float Hex_Width = 1.2f;
    //GameObject[,] obj;
    //float x = 0.0f;
    //float y = 0.0f;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    obj = new GameObject[nMapX, nMapY];
    //    for(int i = 0; i < nMapX; ++i)
    //    {
    //        y = 0.0f;
    //        for(int j = 0; j < nMapY; ++j)
    //        {
    //            obj[i, j] = Instantiate(originObject, new Vector3(x, 0.0f, y), Quaternion.identity);
    //            obj[i, j].transform.position = Cal_HexPosToViewLocalPos(obj[i, j].transform.position);

    //            y += 1.0f;
    //        }
    //        x += 1.0f;
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public Vector3 Cal_HexPosToViewLocalPos(Vector3 hexPos)
    //{
    //    // Y•ûŒü‚‚³
    //    float Hex_Height = Hex_Width * Mathf.Sin(60.0f * Mathf.Deg2Rad);

    //    // X•ûŒü‚Ì‚¸‚ê
    //    float Hex_Adjust = Hex_Width * Mathf.Cos(60.0f * Mathf.Deg2Rad);

    //    float grid_X = Hex_Width * hexPos.x + Hex_Adjust * Mathf.Abs(hexPos.z % 2);
    //    float grid_Z = Hex_Height * hexPos.z;

    //    return new Vector3(grid_X, 0.0f, grid_Z);
    //}
}
