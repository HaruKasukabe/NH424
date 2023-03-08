using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    //追記　オブジェクトプール用コントローラー格納用変数宣言
    ObjectPool objectPool;

    void Start()
    {
        //追記　オブジェクトプールを取得
        objectPool = transform.parent.GetComponent<ObjectPool>();
        gameObject.SetActive(false);
    }

    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        //追記　下の回収処理を呼び出す
        HideFromStage();
    }


    public void ShowInStage(Vector3 _pos)
    {
        //追記　positionを渡された座標に設定
        transform.position = _pos;
    }

    public void HideFromStage()
    {
        //オブジェクトプールのCollect関数を呼び出し自身を回収
        objectPool.Collect(this);
    }
}
