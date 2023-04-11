using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class EffekseerSakura : MonoBehaviour
{
    public EffekseerEffectAsset effect;
    EffekseerHandle handle;

    // Start is called before the first frame update
    void Start()
    {
        effect = Resources.Load<EffekseerEffectAsset>("sakura");
        EffekseerSystem.PlayEffect(effect, transform.position);
        Resources.UnloadAsset(effect);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
