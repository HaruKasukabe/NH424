using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DebugConversation : MonoBehaviour
{
    // �\����
    // �ϐ��錾
    public ConverSation conversation; // ��b�Ǘ��X�N���v�g�Ăяo��
    bool flg;
    string FileName = "CSV/DebugText";

    // Start is called before the first frame update
    void Start()
    {
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �t�@�C���ǂݍ��݊֐��Ăяo��
        if (Input.GetKey(KeyCode.K) && !flg)
        {
            flg = conversation.ConverSationInit(FileName);
        }
        if(flg)
            flg = conversation.ActiveGameIvent();
        Debug.Log(flg);
    }

    
}
