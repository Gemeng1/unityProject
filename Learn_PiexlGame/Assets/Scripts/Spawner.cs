using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    [SerializeField] private float SpawnerTime;
    private float countTime;
    

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if(countTime >= SpawnerTime)
        {
            CreatePlatform();
            countTime = 0;
        }
    }

    private void CreatePlatform()
    {
        int index = Random.Range(0, platforms.Count);
        int spikeNum = 0;
        if(index == 4)
        {
            spikeNum++;
        }

        if(spikeNum >= 1)
        {
            spikeNum = 0;
            countTime = SpawnerTime;
            return;
        }
        
        Vector3 pos = transform.position;
        pos.x = Random.Range(-3.33f, 3.33f);
        GameObject go = Instantiate(platforms[index], pos, Quaternion.identity);
        go.transform.SetParent(transform);
    }
}
