using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rotate : MonoBehaviour
{
    float rnd;
    [SerializeField] Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        rnd = Random.Range(0.1f, 0.6f);�@// �� 1�`9�͈̔͂Ń����_���Ȑ����l���Ԃ�
    }

    // Update is called once per frame
    void Update()
    {
       
        // transform���擾
        Transform myTransform = this.transform;

        // ���[���h���W��ŁA���݂̉�]�ʂ։��Z����
        myTransform.Rotate(rnd, rnd, rnd, Space.World);

            if (myTransform.position.y <= -10.0)
            {
                //fade.FadeIn(1f,()=> 
                SceneManager.LoadScene("StartScene");
            }
    }
}
