//=============================================================================
//
// ユニット色変更 クラス [UnitColorChange.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitColorChange : MonoBehaviour
{
    List<Color> colorList = new List<Color>();
    int listNum = 0;
    bool b = true;
    float vol = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        GetColorOfGameObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // 移動可能数が0以下のとき
        if (this.GetComponentInParent<Unit>().actNum <= 0 && b)
        {
            ChangeColorOfGameObject(gameObject, new Color(-vol, -vol, -vol));
            b = false;
        }
        // 移動可能数が1以上のとき
        if (this.GetComponentInParent<Unit>().actNum > 0 && !b)
        {
            ResetColorOfGameObject(gameObject);
            listNum = 0;
            b = true;
        }    
    }

    private void ChangeColorOfGameObject(GameObject targetObject, Color color)
    {
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            targetRenderer.material.color += color;
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
        }
    }
    private void ResetColorOfGameObject(GameObject targetObject)
    {
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            targetRenderer.material.color = colorList[listNum];
            listNum++;
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ResetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }
    private void GetColorOfGameObject(GameObject targetObject)
    {
        //入力されたオブジェクトのRendererを全て取得
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            colorList.Add(targetRenderer.material.color);
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            GetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }
    }
}
