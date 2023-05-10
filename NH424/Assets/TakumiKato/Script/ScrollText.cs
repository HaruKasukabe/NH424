using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI musicText1, musicText2;
    private Vector3 musicText1Position, musicText2Position;
    private readonly float textWidth = 150f;
    float move = 0.1f;

    void Start()
    {
        musicText1Position = musicText1.rectTransform.localPosition;
        musicText2Position = musicText2.rectTransform.localPosition;
        musicText1Position.x = 0;
        musicText2Position.x = textWidth;
        musicText2.rectTransform.localPosition = musicText2Position;
        musicText1.rectTransform.localPosition = musicText1Position;
    }

    void Update()
    {
        musicText1Position.x += move;
        musicText2Position.x += move;
        if (musicText1Position.x > textWidth)
        {
            musicText1Position.x = -textWidth;
        }
        if (musicText2Position.x > textWidth)
        {
            musicText2Position.x = -textWidth;
        }
        musicText1.rectTransform.localPosition = musicText1Position;
        musicText2.rectTransform.localPosition = musicText2Position;
    }
}
