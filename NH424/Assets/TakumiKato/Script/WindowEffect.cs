using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class WindowEffect : MonoBehaviour
{
    public static WindowEffect instance = null;

    [SerializeField] EffekseerEffectAsset GameClearEffect;
    Vector3 clearEffectPos = new Vector3(0, 0, 0);
    float destroyClearEffectTime = 3.0f;

    [SerializeField] EffekseerEffectAsset[] GameOverEffect;
    Vector3 overEffectPos = new Vector3(0, 0, 0);
    float destroyOverEffectTime = 3.0f;

    [SerializeField] GameObject[] SeasoneEffect;
    Vector3 seasonEffectPos = new Vector3(0, 0, 0);
    float destroySeasonEffectTime = 3.0f;


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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClearEffect()
    {
        EffekseerSystem.PlayEffect(GameClearEffect, clearEffectPos);
    }
    public void PlayOverEffect()
    {
        EffekseerSystem.PlayEffect(GameOverEffect[(int)GameManager.instance.season], overEffectPos);
    }
    public void PlaySeasonEffect()
    {
        GameObject effect = Instantiate(SeasoneEffect[(int)GameManager.instance.season], seasonEffectPos, Quaternion.identity);
        Destroy(effect, destroySeasonEffectTime);
    }
}
