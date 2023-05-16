using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rotate : MonoBehaviour
{
    float rnd;
    [SerializeField] Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.Range(0.1f, 0.6f);　// ※ 1～9の範囲でランダムな整数値が返る
    }

    // Update is called once per frame
    void Update()
    {
       
        // transformを取得
        Transform myTransform = this.transform;

        // ワールド座標基準で、現在の回転量へ加算する
        myTransform.Rotate(rnd, rnd, rnd, Space.World);

            if (myTransform.position.y <= -10.0)
            {
                //fade.FadeIn(1f,()=> 
                SceneManager.LoadScene("StartScene");
            }
    }
}
