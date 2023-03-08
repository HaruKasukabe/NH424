using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    public static HexManager instance = null;

    List<List<Transform>> areaList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        areaList = new List<List<Transform>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddArea(List<Transform> area)
    {
        areaList.Add(area);
    }
    public void AddVillage(Transform village, int num)
    {
        areaList[num].Add(village);
    }

    public Transform GetNearHex(Vector3 pos)
    {
        Transform result = null;
        var minTargetDistance = float.MaxValue;
        for (int i = 0; i < areaList.Count; i++)
        {
            for (int j = 0; j < areaList[i].Count; j++)
            {
                var targetDistance = Vector3.SqrMagnitude(pos - areaList[i][j].position);
                if (!(targetDistance < minTargetDistance)) continue;
                minTargetDistance = targetDistance;
                result = areaList[i][j];
            }
        }

        return result?.transform;
    }

    public List<Transform> GetDiscoverHex(Vector3 pos)
    {
        List<Transform> result = new List<Transform>();
        for (int i = 0; i < areaList.Count; i++)
        {
            for (int j = 0; j < areaList[i].Count; j++)
            {
                var targetDistance = Vector3.SqrMagnitude(pos - areaList[i][j].position);
                if (targetDistance > 2.0f)
                    continue;
                if (areaList[i][j].GetComponent<Hex>().bDiscover)
                    continue;

                result.Add(areaList[i][j]);
            }
        }

        return result;
    }

    public List<Transform> GetVillageHex(Vector3 pos)
    {
        List<Transform> result = new List<Transform>();
        for (int i = 0; i < areaList.Count; i++)
        {
            for (int j = 0; j < areaList[i].Count; j++)
            {
                var targetDistance = Vector3.SqrMagnitude(pos - areaList[i][j].position);
                if (!((targetDistance < 2.0f) && !(areaList[i][j].gameObject.CompareTag("Village"))))
                    continue;

                result.Add(areaList[i][j]);
                areaList[i].Remove(areaList[i][j]);
            }
        }

        return result;
    }
}
