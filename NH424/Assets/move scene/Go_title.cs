using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //���N���b�N�������ꂽ��
        {
            SceneManager.LoadScene("/*�^�C�g���V�[���̖��O*/"); //�^�C�g���Ɉړ�����
        }
    }
}
