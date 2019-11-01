using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public GameObject prefabObstacle;
    public List<GameObject> pooledObstacles;
    public int poolSize = 3;
    public bool canGrow = false;

    public float maxSpawnDuration = 6f;
    public float minSpawnDuration = 2f;
    float spawnTimer = 0;

    void Awake()
    {
        pooledObstacles = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obstacle = Instantiate(prefabObstacle);
            obstacle.SetActive(false);
            obstacle.transform.SetParent(transform);
            pooledObstacles.Add(obstacle);
        }
    }

    public GameObject getPooledObstacle()
    {
        foreach (GameObject po in pooledObstacles)
        {
            if (!po.activeInHierarchy)
            {
                return po;
            }
        }
        if (canGrow)
        {
            GameObject obstacle = Instantiate(prefabObstacle);
            obstacle.SetActive(false);
            obstacle.transform.SetParent(transform);
            pooledObstacles.Add(obstacle);
            return obstacle;
        }
        return null;
    }


    void Update()
    {
        if (GameManager.instance.player.state == CharacterMovement.eState.NORMAL)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer < 0)
            {
                spawnObsstacle();
                spawnTimer = Random.Range(minSpawnDuration, maxSpawnDuration);
            }
        }
    }

    void spawnObsstacle()
    {
        GameObject newObstacle = null;
        newObstacle = getPooledObstacle();
        if (newObstacle == null)
        {
            return;
        }
        newObstacle.GetComponent<Obstacle>().Spawn();
    }
}
