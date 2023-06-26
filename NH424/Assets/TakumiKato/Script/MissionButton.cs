//=============================================================================
//
// �~�b�V�����{�^�� �N���X [MissionButton.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionButton : MonoBehaviour
{
    public static MissionButton instance = null;

    bool bCanMenu = true;           // ���j���[��\���ł��邩
    RectTransform trs;              // �~�b�V�������j���[��transform
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
        // LB�������Ȃ���
        if (Input.GetButton("Trigger_L"))
        {
            // A������
            if (Input.GetButtonDown("Fire1"))
            {
                if (bCanMenu)   // ���j���[��\������
                {
                    trs.position = new Vector2(Screen.width / 2, Screen.height / 2);
                    GameManager.instance.SetUICursol(true);
                    bCanMenu = false;
                }
                else    // ���j���[���\���ɂ���
                {
                    //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
                    trs.position += new Vector3(-9999, 0, 0);
                    GameManager.instance.SetUICursol(false);
                    bCanMenu = true;
                }
            }
        }
        else if(Input.GetButtonDown("Fire2") && !bCanMenu)  // B�������ƃ��j���[���\���ɂ���
        {
            //ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
            trs.position += new Vector3(-9999, 0, 0);
            GameManager.instance.SetUICursol(false);
            bCanMenu = true;
        }
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

    // true : ����\��, false : ���\��
    public bool GetbMenu()
    {
        return bCanMenu;
    }
}
