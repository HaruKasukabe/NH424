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
        if (this.GetComponentInParent<Unit>().actNum <= 0 && b)
        {
            ChangeColorOfGameObject(gameObject, new Color(-vol, -vol, -vol));
            b = false;
        }
        if (this.GetComponentInParent<Unit>().actNum > 0 && !b)
        {
            ResetColorOfGameObject(gameObject);
            listNum = 0;
            b = true;
        }    
    }

    private void ChangeColorOfGameObject(GameObject targetObject, Color color)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            targetRenderer.material.color += color;
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
        }

    }
    private void ResetColorOfGameObject(GameObject targetObject)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            targetRenderer.material.color = colorList[listNum];
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
            colorList.Add(targetRenderer.material.color);
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            GetColorOfGameObject(targetObject.transform.GetChild(i).gameObject);
        }

    }
}
