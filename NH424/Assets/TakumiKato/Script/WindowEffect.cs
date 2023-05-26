using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class WindowEffect : MonoBehaviour
{
    public static WindowEffect instance = null;

    [SerializeField] GameObject rawImage;

    [SerializeField] EffekseerEffectAsset GameClearEffect;
    Vector3 clearEffectPos = new Vector3(1442.3f, 9, 439);
    bool bClearEffect;

    [SerializeField] EffekseerEffectAsset[] GameOverEffect;
    Vector3 overEffectPos = new Vector3(1442.3f, 9, 93.3f);

    [SerializeField] GameObject[] SeasonEffect;
    GameObject seasonEffect;
    Vector3 seasonEffectPos = new Vector3(1441.18f, 20.04f, 51.8f);
    Vector3 effectRota = new Vector3(88.96485f, 328.8247f, 316.608f);


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        rawImage.SetActive(false);
        bClearEffect = false;
        PlaySeasonEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClearEffect()
    {
        if (!bClearEffect)
        {
            Destroy(seasonEffect);
            rawImage.SetActive(true);
            EffekseerSystem.PlayEffect(GameClearEffect, clearEffectPos);
            bClearEffect = true;
        }
    }
    public void PlayOverEffect()
    {
        Destroy(seasonEffect);
        rawImage.SetActive(true);
        EffekseerSystem.PlayEffect(GameOverEffect[(int)GameManager.instance.season], overEffectPos);
    }
    public void PlaySeasonEffect()
    {
        rawImage.SetActive(true);
        seasonEffect = Instantiate(SeasonEffect[(int)GameManager.instance.season], seasonEffectPos, Quaternion.identity);
        seasonEffect.transform.Rotate(effectRota);
    }
    public void DestroySeasonEffect()
    {
        Destroy(seasonEffect);
    }
    public void ChangeSeasonEffect()
    {
        Destroy(seasonEffect);
        rawImage.SetActive(true);
        seasonEffect = Instantiate(SeasonEffect[(int)GameManager.instance.season], seasonEffectPos, Quaternion.identity);
        seasonEffect.transform.Rotate(effectRota);
    }

    // 一定時間後に処理を呼び出すコルーチン
    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

    public void StopRawImage()
    {
        rawImage.SetActive(false);
    }
}
