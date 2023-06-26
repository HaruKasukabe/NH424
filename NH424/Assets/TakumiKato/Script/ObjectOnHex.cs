//=============================================================================
//
// マスの上のオブジェクト クラス [ObjectOnHex.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnHex : MonoBehaviour
{
    Hex parentHex;  // このオブジェクトが置かれているマス
    List<Material> materialList = new List<Material>();
    public Material alphaMaterial;  // ケモコと重なった時に置き換えるマテリアル
    int listNum = 0;
    bool bChange = true;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent)
            parentHex = GetComponentInParent<Hex>();
        GetColorOfGameObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (parentHex.bUnit && bChange)
        {
            ChangeColorOfGameObject(gameObject);
            bChange = false;
        }
        if (!parentHex.bUnit && !bChange)
        {
            ResetColorOfGameObject(gameObject);
            listNum = 0;
            bChange = true;
        }
    }

    // オブジェクト(子オブジェクト)のマテリアルを全て置き換える
    private void ChangeColorOfGameObject(GameObject targetObject)
    {
        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            Material[] mat = targetRenderer.materials;

            for (int i = 0; i < mat.Length; i++)
                mat[i] = alphaMaterial;

            targetRenderer.materials = mat;
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }
    // オブジェクト(子オブジェクト)のマテリアルを全て元に戻す
    private void ResetColorOfGameObject(GameObject targetObject)
    {

        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            Material[] mat = targetRenderer.materials;

            //targetRenderer.material = materialList[listNum];
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i] = materialList[listNum];
                listNum++;
            }

            targetRenderer.materials = mat;
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ResetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }

    // オブジェクト(子オブジェクト)のマテリアルを全て取得してリストに追加
    private void GetColorOfGameObject(GameObject targetObject)
    {

        //入力されたオブジェクトのRendererを全て取得
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            for (int i = 0; i < targetRenderer.materials.Length; i++)
            {
                materialList.Add(targetRenderer.materials[i]);
            }
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            GetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }

    // このオブジェクトが置かれているマスを設定
    public void SetHex(Hex hex)
    {
        parentHex = hex;
    }
    // このオブジェクトが置かれているマスを取得
    public Hex GetHex()
    {
        return parentHex;
    }
}
