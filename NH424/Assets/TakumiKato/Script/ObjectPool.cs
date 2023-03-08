using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //弾のプレファブ
    [SerializeField] Area hex;
    //生成する数
    [SerializeField] int maxCount;
    //生成した弾を格納するQueue
    Queue<Area> hexQueue;
    //初回生成時のポジション
    Vector3 setPos = new Vector3(-100, 0, -100);

    //起動時の処理
    private void Awake()
    {
        //Queueの初期化
        hexQueue = new Queue<Area>();

        //弾を生成するループ
        for (int i = 0; i < maxCount; i++)
        {
            //生成
            Area tmpArea = Instantiate(hex, setPos, Quaternion.identity, transform);
            //Queueに追加
            hexQueue.Enqueue(tmpArea);
        }
    }


    //弾を貸し出す処理
    public Area Launch(Vector3 _pos)
    {
        //Queueが空ならnull
        if (hexQueue.Count <= 0) return null;

        //Queueから弾を一つ取り出す
        Area tmpArea = hexQueue.Dequeue();
        //弾を表示する
        tmpArea.gameObject.SetActive(true);
        tmpArea.GetComponent<HexSort>().enabled = true;
        //渡された座標に弾を移動する
        tmpArea.ShowInStage(_pos);
        //呼び出し元に渡す
        return tmpArea;
    }

    //弾の回収処理
    public void Collect(Area _hex)
    {
        //弾のゲームオブジェクトを非表示
        _hex.gameObject.SetActive(false);
        //Queueに格納
        hexQueue.Enqueue(_hex);
    }
}
