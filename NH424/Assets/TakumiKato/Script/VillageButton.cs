using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageButton : MonoBehaviour
{
    public static VillageButton instance = null;

    bool bCanMenu = true;
    RectTransform trs;
    RectTransform listTrs;
    public GameObject villageMenu;
    public GameObject villageImageList;

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
        trs = villageMenu.GetComponent<RectTransform>();
        listTrs = villageImageList.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Trigger_L"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (bCanMenu)
                {
                    trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
                    listTrs.position = new Vector2(Screen.width / 2, Screen.height / 6);
                    GameManager.instance.SetUICursol(true);

                    bCanMenu = false;
                }
                else
                {
                    trs.position += new Vector3(-9999, 0, 0);
                    listTrs.position += new Vector3(-9999, 0, 0);
                    GameManager.instance.SetUICursol(false);

                    bCanMenu = true;
                }
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            trs.position += new Vector3(-9999, 0, 0);
            listTrs.position += new Vector3(-9999, 0, 0);
            bCanMenu = true;
        }
    }

    public void OnClick()
    {
        if (bCanMenu)
        {
            trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
            listTrs.position = new Vector2(Screen.width / 2, Screen.height / 6);

            bCanMenu = false;
        }
        else
        {
            trs.position += new Vector3(-9999, 0, 0);
            listTrs.position += new Vector3(-9999, 0, 0);

            bCanMenu = true;
        }
    }
    public bool GetbMenu()
    {
        return bCanMenu;
    }
}
