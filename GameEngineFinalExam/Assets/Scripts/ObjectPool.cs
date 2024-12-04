using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private string objectName = "New Object";
    [SerializeField] private int poolSize;
    [SerializeField] private bool dynamicSize;

    private GameObject objectPrefab;
    private Transform poolContainerTransform;

    private readonly Queue<GameObject> pool = new();
    private readonly List<GameObject> activePool = new();

    public void InitializePool(GameObject objectPrefab)
    {
        this.objectPrefab = objectPrefab;
        GameObject poolContainer = new GameObject($"{objectName} Pool Container");
        poolContainerTransform = poolContainer.transform;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = Instantiate(objectPrefab, poolContainerTransform);
            instance.name = $"{objectName} {i}";
            instance.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject activeObject = pool.Dequeue();
            activeObject.SetActive(true);
            activePool.Add(activeObject);
            return activeObject;
        }
        else if (dynamicSize)
        {
            GameObject instance = Instantiate(objectPrefab, poolContainerTransform);
            instance.name = $"Extra {objectName}";
            activePool.Add(instance);
            return instance;
        }

        return null;
    }

    public void ReturnToPool(GameObject gameObject)
    {
        activePool.Remove(gameObject);
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }
}