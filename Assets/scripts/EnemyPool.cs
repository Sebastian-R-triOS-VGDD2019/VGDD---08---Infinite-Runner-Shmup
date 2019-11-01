using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject prefabEnemy;
    public List<GameObject> pooledEnemies;
    public int poolSize = 3;
    public bool canGrow = false;

    public float maxSpawnDuration = 4f;
    public float minSpawnDuration = 1f;
    float spawnTimer = 0;


    private void Awake()
    {
        pooledEnemies = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(prefabEnemy);
            enemy.SetActive(false);
            enemy.transform.SetParent(transform);
            pooledEnemies.Add(enemy);
        }
    }

    public GameObject getPooledEnemy()
    {
        foreach (GameObject pe in pooledEnemies)
        {
            if (!pe.activeInHierarchy)
            {
                return pe;
            }
        }
        if (canGrow)
        {
            GameObject enemy = Instantiate(prefabEnemy);
            enemy.SetActive(true);
            enemy.transform.SetParent(transform);
            pooledEnemies.Add(enemy);
            return enemy;
        }
        return null;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player.state == CharacterMovement.eState.NORMAL)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer < 0)
            {
                spawnEnemy();
                spawnTimer = Random.Range(minSpawnDuration, maxSpawnDuration);
            }



            // test
            if (Input.GetKeyDown(KeyCode.V))
            {
                spawnEnemy();
            }

        }
    }

    void spawnEnemy()
    {
        GameObject newEnemy = null;
        newEnemy = getPooledEnemy();
        if (newEnemy == null)
        {
            return;
        }
        newEnemy.GetComponent<EnemyBehavior>().Spawn(GameManager.instance.player.BulletDamage() / 30);

    }
}
