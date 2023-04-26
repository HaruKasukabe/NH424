using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonIconUI : MonoBehaviour
{
    Image image;
    public Sprite[] sprite;

    public static bool bChangeSeasonIcon = false;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bChangeSeasonIcon)
        {
            image.sprite = sprite[(int)GameManager.instance.season];
        }
    }

    public static void SetSeasonIcon()
    {
        bChangeSeasonIcon = true;
    }
}
