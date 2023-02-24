using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexaSort : MonoBehaviour
{
    private GameObject[] ChildObject;
    private List<GameObject> GrandChildObject;
    public float Hex_Width = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        ChildObject = new GameObject[this.transform.childCount];
        GrandChildObject = new List<GameObject>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            ChildObject[i] = this.transform.GetChild(i).gameObject;
            ChildObject[i].transform.position = Cal_HexPosToViewLocalPos(ChildObject[i].transform.position);

            for(int j = 0; j < ChildObject[i].transform.childCount; j++)
            {
                GrandChildObject.Add(ChildObject[i].transform.GetChild(i).gameObject);
                GrandChildObject = Cal_HexPosToViewLocalPos(GrandChildObject.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 Cal_HexPosToViewLocalPos(Vector3 hexPos)
    {
        // Y•ûŒü‚‚³
        float Hex_Height = Hex_Width * Mathf.Sin(60.0f * Mathf.Deg2Rad);

        // X•ûŒü‚Ì‚¸‚ê
        float Hex_Adjust = Hex_Width * Mathf.Cos(60.0f * Mathf.Deg2Rad);

        float grid_X = Hex_Width * hexPos.x + Hex_Adjust * Mathf.Abs(hexPos.z % 2);
        float grid_Z = Hex_Height * hexPos.z;

        return new Vector3(grid_X, 0.0f, grid_Z);
    }
}
