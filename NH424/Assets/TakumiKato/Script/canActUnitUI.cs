using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canActUnitUI : MonoBehaviour
{
    public Image[] colors;
    int actUnitNum;

    // Start is called before the first frame update
    void Start()
    {
        actUnitNum = GameManager.instance.canActUnitNum - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (actUnitNum != GameManager.instance.canActUnitNum - 1)
        {
            actUnitNum = GameManager.instance.canActUnitNum - 1;
            for (int i = 0; i < 5; i++)
            {
                if (actUnitNum >= i)
                    colors[i].color = new Color(1,1,1,1);
                else
                    colors[i].color = new Color(1,1,1, 0.25f);
            }
        }
    }
}
