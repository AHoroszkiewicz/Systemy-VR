using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform pos1, pos2;
    [SerializeField] private List<TriggerReaction> baseObjects = new List<TriggerReaction>();
    [SerializeField] private List<SpawnableObjectPool> objectPools = new List<SpawnableObjectPool>();
    [SerializeField] private int toSpawnAtStart = 5;
    [SerializeField] private float spawnInterval = 2f;

    private void Start()
    {
        InicializePools();
        int spawnNumber = toSpawnAtStart;
        SpawnObjects(spawnNumber);
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        var spawnTime = new WaitForSeconds(spawnInterval);
        while (true)
        {
            yield return spawnTime;
            SpawnObjects(1);
        }
    }

    public void OnReturnToPoolFromOutside(TriggerReaction reaction)
    {
        foreach (var pool in objectPools)
        {
            if (pool.poolID == reaction.poolID)
            {
                pool.objectPool.Release(reaction);
                break;
            }
        }
    }

    private void SpawnObjects(int spawnNumber)
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            int poolIndex = objectPools.Count > 1 ? Random.Range(0, objectPools.Count) : 0;
            var objectToSpawn = objectPools[poolIndex].objectPool.Get();
            if (objectToSpawn != null)
            {
                Vector3 spawnPos = Vector3.Lerp(pos1.position, pos2.position, Random.Range(0f, 1f));
                objectToSpawn.transform.position = spawnPos;
            }
        }
    }

    private void InicializePools()
    {
        foreach (var baseObj in baseObjects)
        {
            if (baseObj != null)
            {
                SpawnableObjectPool pool = new SpawnableObjectPool(baseObj.name, baseObj, this.gameObject);
                objectPools.Add(pool);
            }
        }
    }
}

[System.Serializable]
public class SpawnableObjectPool
{
    public string poolID;
    public TriggerReaction baseObject;
    public ObjectPool<TriggerReaction> objectPool;
    public GameObject spawner;

    public SpawnableObjectPool(string poolID, TriggerReaction BaseObj, GameObject spawner)
    {
        this.poolID = poolID;
        this.baseObject = BaseObj;
        this.spawner = spawner;
        objectPool = new ObjectPool<TriggerReaction>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroy,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
        );
        this.spawner = spawner;
    }

    private TriggerReaction CreateItem()
    {
        TriggerReaction item = GameObject.Instantiate(baseObject, spawner.transform);
        item.gameObject.SetActive(false);
        item.onEntered.AddListener(() => OnRelease(item));
        item.poolID = poolID;
        return item;
    }
    private void OnGet(TriggerReaction obj)
    {
        obj.gameObject.SetActive(true);
    }
    private void OnRelease(TriggerReaction obj)
    {
        obj.gameObject.SetActive(false);
    }
    private void OnDestroy(TriggerReaction obj)
    {
        obj?.gameObject.SetActive(false);
    }
}
