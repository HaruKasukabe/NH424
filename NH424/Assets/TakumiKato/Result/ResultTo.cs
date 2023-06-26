//=============================================================================
//
//  ���U���g����̑J�ځ@�N���X [ResultTo.cpp]
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Effekseer;

public class ResultTo : MonoBehaviour
{
    public Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        EffekseerSystem.StopAllEffects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �^�C�g����
    public void Title()
    {
        fade.FadeIn(2.0f, () => { SceneManager.LoadScene("StartScene"); });
    }
    // �Q�[����
    public void Game()
    {
        fade.FadeIn(2.0f, () => { SceneManager.LoadScene("NewScene"); });
    }
}
