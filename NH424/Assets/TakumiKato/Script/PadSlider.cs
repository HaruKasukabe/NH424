using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PadSlider : MonoBehaviour
{
    int nowOption = -1; // ０：カメラ　１：サウンド
    float add = 0.001f;
    Slider activeSlider;

    [SerializeField] Slider cameraSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    [SerializeField] Button cameraButton;
    [SerializeField] Button soundButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nowOption != -1)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            if (moveX > 0)
                activeSlider.value += add;
            else if (moveX < 0)
                activeSlider.value -= add;

            if (nowOption == 1)
            {
                if (moveY > 0)
                    activeSlider = bgmSlider;
                else if (moveY < 0)
                    activeSlider = seSlider;
            }
        }
    }

    public void SetCameraSlider()
    {
        nowOption = 0;
        activeSlider = cameraSlider;
        cameraButton.Select();
    }
    public void SetSoundSlider()
    {
        nowOption = 1;
        activeSlider = bgmSlider;
        soundButton.Select();
    }
    public void nullSlider()
    {
        nowOption = -1;
        activeSlider = null;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
