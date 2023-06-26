//=============================================================================
//
// �}�X�̏�̃I�u�W�F�N�g �N���X [ObjectOnHex.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnHex : MonoBehaviour
{
    Hex parentHex;  // ���̃I�u�W�F�N�g���u����Ă���}�X
    List<Material> materialList = new List<Material>();
    public Material alphaMaterial;  // �P���R�Əd�Ȃ������ɒu��������}�e���A��
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

    // �I�u�W�F�N�g(�q�I�u�W�F�N�g)�̃}�e���A����S�Ēu��������
    private void ChangeColorOfGameObject(GameObject targetObject)
    {
        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            Material[] mat = targetRenderer.materials;

            for (int i = 0; i < mat.Length; i++)
                mat[i] = alphaMaterial;

            targetRenderer.materials = mat;
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }
    // �I�u�W�F�N�g(�q�I�u�W�F�N�g)�̃}�e���A����S�Č��ɖ߂�
    private void ResetColorOfGameObject(GameObject targetObject)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
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

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ResetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }

    // �I�u�W�F�N�g(�q�I�u�W�F�N�g)�̃}�e���A����S�Ď擾���ă��X�g�ɒǉ�
    private void GetColorOfGameObject(GameObject targetObject)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            for (int i = 0; i < targetRenderer.materials.Length; i++)
            {
                materialList.Add(targetRenderer.materials[i]);
            }
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            GetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }

    // ���̃I�u�W�F�N�g���u����Ă���}�X��ݒ�
    public void SetHex(Hex hex)
    {
        parentHex = hex;
    }
    // ���̃I�u�W�F�N�g���u����Ă���}�X���擾
    public Hex GetHex()
    {
        return parentHex;
    }
}
