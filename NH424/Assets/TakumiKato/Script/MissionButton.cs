using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionButton : MonoBehaviour
{
    public static MissionButton instance = null;

    bool bCanMenu = true;
    RectTransform trs;
    public GameObject MissionMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trs = MissionMenu.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (bCanMenu)
        {
            trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
            bCanMenu = false;
        }
        else
        {
            trs.position += new Vector3(-9999, 0, 0);
            bCanMenu = true;
        }
    }
    public bool GetbMenu()
    {
        return bCanMenu;
    }
}
