using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexSort : MonoBehaviour
{
    private GameObject ChildObject;

    // Start is called before the first frame update
    void Start()
    {
        List<Transform> list = new List<Transform>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            list.Add(child.transform);
            ChildObject = child.gameObject;
            ChildObject.transform.position = GameManager.instance.Cal_HexPosToViewLocalPos(ChildObject.transform.position);
        }

        HexManager.instance.AddArea(list);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
