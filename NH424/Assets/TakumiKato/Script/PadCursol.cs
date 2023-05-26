using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadCursol : MonoBehaviour
{
    public float padCursolSpeed = 2.0f;     // ���������x
    Unit padSelectUnit = null;              // ���͂�ł��郆�j�b�g
    RaycastHit hitDown;                     // �J�[�\���̉����擾
    Hex Hex;                                // �����ɂ���}�X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.bMenuDisplay())    // �������j���[��\�����Ă��邩�ǂ���
        {
            float moveX = Input.GetAxis("Horizontal") * padCursolSpeed;
            float moveY = Input.GetAxis("Vertical") * padCursolSpeed;
            transform.localPosition += new Vector3(moveX, 0.0f, moveY);

            // �J�[�\���̉��ɂ���}�X���擾
            int mask = 1 << 6;  // Ray���}�X�ɂ���������Ȃ��悤�ɐݒ�
            if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 2.0f, mask))
            {
                Hex hitHex = hitDown.transform.GetComponent<Hex>();

                if (Hex != hitHex)
                    Hex = hitHex;

                Hex.SetCursol(true);

                // �͂�ł��郆�j�b�g�𓮂���
                if (padSelectUnit)
                    padSelectUnit.PadDrag(hitDown);
            }

            // ���j�b�g��͂ށA�u��
            if (Input.GetButtonDown("Fire1"))
            {
                if (padSelectUnit == null)�@// ���j�b�g��͂�ł��Ȃ���
                {
                    if (Hex.bUnit)
                        padSelectUnit = Hex.Unit;
                }
                else // ���j�b�g��͂�ł��鎞
                {
                    padSelectUnit.MouseUp();
                    padSelectUnit = null;
                }
            }
            // �u�����܂ܑf�ނ��l��
            if (Input.GetButtonDown("Fire3"))
            {
                if(Hex.bUnit)
                    Hex.Unit.ActMaterial();
            }
        }
    }
}
