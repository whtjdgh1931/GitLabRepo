using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public MoveObject spawnObjectPrefab;

    public float minSpawnTime;
    public float maxSpawnTime;

    public float spawnTime;
    public float currentTime;

    public bool isLeft;

    public void Awake()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

    }

    public void Start()
    {
        
        GetComponent<BoxCollider>().enabled = false;    
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnTime)
        {
            currentTime = 0;
            CreateObject(isLeft);
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }


    }

    public void CreateObject(bool left)
    {
        MoveObject spawnObject = Instantiate(spawnObjectPrefab,transform.position,transform.rotation,transform);
        spawnObject.InitObject(left);
    }
}
