using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;

    public float moveSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            inputHorizontal = Input.GetAxis("Mouse X");
            inputVertical = Input.GetAxis("Mouse Y");

            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

            // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

            // �L�����N�^�[�̌�����i�s������
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
    }

    void FixedUpdate()
    {

    }
}