//=============================================================================
//
// �Q�[���J���� �N���X [GameCamera.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Camera cam;     // �J����

    public float sensitiveMoveMouse = 0.8f;     // �}�E�X�ł̃J�����ړ����x
    public float sensitiveZoomMouse = 10.0f;    // �}�E�X�ł̃J�����g�k���x

    public float sensitiveMovePad = 0.3f;       // �p�b�h�ł̃J�����ړ����x
    public float sensitiveZoomPad = 3;          // �p�b�h�ł̃J�����g�k���x

    [SerializeField] GameObject LRUI; // LR�����������ɂł�UI�̃Q�[���I�u�W�F�N�g

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.bMenuDisplay())    // �������j���[��\�����Ă��邩�ǂ���
        {
            // �}�E�X�ŃJ�����ړ�
            if (Input.GetMouseButton(1))    // �E�N���b�N�������Ȃ���
            {
                float moveX = Input.GetAxis("Mouse X") * sensitiveMoveMouse;
                float moveY = Input.GetAxis("Mouse Y") * sensitiveMoveMouse;
                //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                cam.transform.localPosition -= new Vector3(moveX, 0.0f, moveY);
            }

            // �p�b�h(�E�X�e�B�b�N)�ŃJ�����ړ�
            float moveXSti = Input.GetAxis("R_Stick_H") * sensitiveMovePad;
            float moveYSti = Input.GetAxis("R_Stick_V") * sensitiveMovePad;
            //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            cam.transform.localPosition += new Vector3(moveXSti, 0.0f, moveYSti);

            // �}�E�X�z�C�[���ŃJ�����g�k
            float moveZ = Input.GetAxis("Mouse ScrollWheel") * sensitiveZoomMouse;
            cam.transform.position += cam.transform.forward * moveZ;

            // �p�b�h(LT, RT)�ŃJ�����g�k
            float trigger = Input.GetAxis("LRTrigger");
            if (trigger > 0)        // LT�Ŋg��
                cam.transform.position += cam.transform.forward * sensitiveZoomPad / 100;
            else if (trigger < 0)   // RT�Ŋg�k
                cam.transform.position -= cam.transform.forward * sensitiveZoomPad / 100;
        }

        if (Input.GetButtonDown("LR"))  // LR���������Ƃ���UI�\��
            LRUI.SetActive(true);
        if (Input.GetButtonUp("LR"))    // LR��������Ƃ���UI������
            LRUI.SetActive(false);
    }
}