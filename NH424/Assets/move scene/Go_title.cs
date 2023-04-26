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
        if(Input.GetMouseButtonDown(0)) //左クリックが押されたら
        {
            SceneManager.LoadScene("/*タイトルシーンの名前*/"); //タイトルに移動する
        }
    }
}
