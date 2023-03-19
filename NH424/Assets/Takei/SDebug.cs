using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDebug : MonoBehaviour
{
    public ManagementTime mtime;
    bool flg;
    // Start is called before the first frame update
    void Start()
    {
        flg = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !flg)
        {
            mtime.GameStart();
            flg = true;
        }
    }
}
