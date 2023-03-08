using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //�e�̃v���t�@�u
    [SerializeField] Area hex;
    //�������鐔
    [SerializeField] int maxCount;
    //���������e���i�[����Queue
    Queue<Area> hexQueue;
    //���񐶐����̃|�W�V����
    Vector3 setPos = new Vector3(-100, 0, -100);

    //�N�����̏���
    private void Awake()
    {
        //Queue�̏�����
        hexQueue = new Queue<Area>();

        //�e�𐶐����郋�[�v
        for (int i = 0; i < maxCount; i++)
        {
            //����
            Area tmpArea = Instantiate(hex, setPos, Quaternion.identity, transform);
            //Queue�ɒǉ�
            hexQueue.Enqueue(tmpArea);
        }
    }


    //�e��݂��o������
    public Area Launch(Vector3 _pos)
    {
        //Queue����Ȃ�null
        if (hexQueue.Count <= 0) return null;

        //Queue����e������o��
        Area tmpArea = hexQueue.Dequeue();
        //�e��\������
        tmpArea.gameObject.SetActive(true);
        tmpArea.GetComponent<HexSort>().enabled = true;
        //�n���ꂽ���W�ɒe���ړ�����
        tmpArea.ShowInStage(_pos);
        //�Ăяo�����ɓn��
        return tmpArea;
    }

    //�e�̉������
    public void Collect(Area _hex)
    {
        //�e�̃Q�[���I�u�W�F�N�g���\��
        _hex.gameObject.SetActive(false);
        //Queue�Ɋi�[
        hexQueue.Enqueue(_hex);
    }
}
