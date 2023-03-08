using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    //�ǋL�@�I�u�W�F�N�g�v�[���p�R���g���[���[�i�[�p�ϐ��錾
    ObjectPool objectPool;

    void Start()
    {
        //�ǋL�@�I�u�W�F�N�g�v�[�����擾
        objectPool = transform.parent.GetComponent<ObjectPool>();
        gameObject.SetActive(false);
    }

    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        //�ǋL�@���̉���������Ăяo��
        HideFromStage();
    }


    public void ShowInStage(Vector3 _pos)
    {
        //�ǋL�@position��n���ꂽ���W�ɐݒ�
        transform.position = _pos;
    }

    public void HideFromStage()
    {
        //�I�u�W�F�N�g�v�[����Collect�֐����Ăяo�����g�����
        objectPool.Collect(this);
    }
}
