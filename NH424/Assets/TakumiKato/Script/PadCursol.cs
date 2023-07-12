//=============================================================================
//
// �p�b�h�J�[�\�� �N���X [PadCursol.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PadCursol : MonoBehaviour
{
    public float padCursolSpeed = 0.08f;         // ���������x
    Unit padSelectUnit = null;                  // ���͂�ł��郆�j�b�g
    RaycastHit hitDown;                         // �J�[�\���̉����擾
    Hex Hex;                                    // �����ɂ���}�X
    [SerializeField] StandaloneInputModule eventSystem;   // �C�x���g�V�X�e��

    // Start is called before the first frame update
    void Start()
    {
        // �ڑ�����Ă���R���g���[���̖��O�𒲂ׂ�
        var controllerNames = Input.GetJoystickNames();

        // �����R���g���[�����ڑ�����Ă��Ȃ��ꍇ
        if (controllerNames.Length == 0)
        {
            eventSystem.submitButton = "Fire2";
            gameObject.SetActive(false);
            GameManager.instance.SetUICursolFlg();
        }
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
                    {
                        padSelectUnit = Hex.Unit;
                        ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.GameStart);
                    }
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

            if (Input.GetButtonDown("Fire2"))
                ManagementAudio.instance.PublicPlaySE(ManagementAudio.GAMESE.Back);
        }
    }
}
