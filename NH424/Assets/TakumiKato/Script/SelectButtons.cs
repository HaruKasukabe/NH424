using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SelectButtons : MonoBehaviour
{
    public static SelectButtons instance = null;

    [SerializeField] List<Button> buttons;
    [SerializeField] List<bool> keys;
    public TextMeshProUGUI title;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int Selected
    {
        get
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i])
                    return i;
            }

            return -1;
        }
    }

    public IEnumerator CorGetInput(Unit unit)
    {
        //keys�����ׂ�false�ɂ���
        for (int i = 0; i < keys.Count; i++)
        {
            keys[i] = false;
        }

        //�Q�[���I�u�W�F�N�g��\��
        gameObject.SetActive(true);

        title.text = "���Ԃɂ��܂����H\n�K�v�H�ށF" + unit.sta.cost + "\n�����H�ށF" + GameManager.instance.food;

        //�����ꂩ�̃{�^�����������܂őҋ@
        yield return new WaitWhile(() => Selected == -1);

        if (Selected == 0)
            unit.BeMyFriend();

        //�Q�[���I�u�W�F�N�g���\��
        gameObject.SetActive(false);
    }

    // �e�{�^���ɏ��������蓖��
    void Start()
    {
        //keys��buttons�Ɠ���������
        keys = Enumerable.Repeat(false, buttons.Count).ToList();


        //�e�{�^���ɏ��������蓖�Ă�
        for (int i = 0; i < buttons.Count; i++)
        {
            //��U�ʂ̕ϐ��Ɋi�[���Ȃ��ƃG���[����
            int a = i;

            //button�������ƊY������key��true��
            buttons[a].onClick.AddListener(() => keys[a] = true);
        }

        //�I�u�W�F�N�g���\���ɂ���
        gameObject.SetActive(false);
    }
}