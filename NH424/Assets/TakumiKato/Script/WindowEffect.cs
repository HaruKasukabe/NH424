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
    Vector3 seasonEffectPos = new Vector3(1474.03f, 268, 439);
    float destroySeasonEffectTime = 8.0f;


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
            rawImage.SetActive(true);
            EffekseerSystem.PlayEffect(GameClearEffect, clearEffectPos);
            bClearEffect = true;
        }
    }
    public void PlayOverEffect()
    {
        rawImage.SetActive(true);
        EffekseerSystem.PlayEffect(GameOverEffect[(int)GameManager.instance.season], overEffectPos);
    }
    public void PlaySeasonEffect()
    {
        rawImage.SetActive(true);
        GameObject effect = Instantiate(SeasonEffect[(int)GameManager.instance.season], seasonEffectPos, Quaternion.identity);
        Destroy(effect, destroySeasonEffectTime);
        StartCoroutine(DelayCoroutine(destroySeasonEffectTime, () =>
        {
            rawImage.SetActive(false);
        }));
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
