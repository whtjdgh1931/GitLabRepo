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

        InitSpawn();
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnTime)
        {
            currentTime = 0;
            CreateObject();
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }


    }

    public void InitSpawn()
    {
        int carCnt = Random.Range(0, 5);

        BaseMap parentMap = GetComponentInParent<BaseMap>();

        for (int i = 0; i < carCnt; i++)
        {

            int RndNum = Random.Range(0, parentMap.roads.Length);
            if (parentMap.treeBlock.Contains(RndNum)) --i;
            else
            {
                MoveObject spawnObject = Instantiate(spawnObjectPrefab, parentMap.roads[RndNum].transform.position, transform.rotation, transform);
                spawnObject.InitObject();
                parentMap.treeBlock.Add(RndNum);
            }


        }
    }


    public void CreateObject()
    {
        MoveObject spawnObject = Instantiate(spawnObjectPrefab, transform.position, transform.rotation, transform);
        spawnObject.InitObject();
    }
}
