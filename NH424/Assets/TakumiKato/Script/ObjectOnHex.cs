using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnHex : MonoBehaviour
{
    Hex parentHex;
    List<Material> materialList = new List<Material>();
    public Material alphaMaterial;
    int listNum = 0;
    bool b = true;

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
        if (parentHex.bUnit && b)
        {
            ChangeColorOfGameObject(gameObject);
            b = false;
        }
        if (!parentHex.bUnit && !b)
        {
            ResetColorOfGameObject(gameObject);
            listNum = 0;
            b = true;
        }
    }

    private void ChangeColorOfGameObject(GameObject targetObject)
    {

        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            //for (int i = 0; i < targetRenderer.materials.Length; i++)
            //    targetRenderer.materials[i] = alphaMaterial;

            Material[] mat = targetRenderer.materials;

            //targetRenderer.material = materialList[listNum];
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
    private void GetColorOfGameObject(GameObject targetObject)
    {

        //入力されたオブジェクトのRendererを全て取得
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            for (int i = 0; i < targetRenderer.materials.Length; i++)
            {
                //targetRenderer.materials[i] = materialList[listNum];
                //listNum++;
                materialList.Add(targetRenderer.materials[i]);
            }
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            GetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }

    public void SetHex(Hex hex)
    {
        parentHex = hex;
    }
    public Hex GetHex()
    {
        return parentHex;
    }
}
