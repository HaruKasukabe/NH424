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

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            targetRenderer.material = alphaMaterial;
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }
    private void ResetColorOfGameObject(GameObject targetObject)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            targetRenderer.material = materialList[listNum];
            listNum++;
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ResetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }
    private void GetColorOfGameObject(GameObject targetObject)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            materialList.Add(targetRenderer.material);
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
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
