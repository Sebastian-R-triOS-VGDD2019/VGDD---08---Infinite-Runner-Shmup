using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject prefabBullet;
    public List<GameObject> pooledBullets;
    public int poolSize = 20;
    public bool canGrow = false;

    float fireCooldown = 0f;

    private void Awake()
    {
        pooledBullets = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(prefabBullet);
            bullet.SetActive(false);
            bullet.transform.SetParent(transform);
            pooledBullets.Add(bullet);
        }
    }

    public GameObject getPooledBullet()
    {
        //for (int i = 0; i < poolSize; i++)
        //{
        //    if (!pooledBullets[i].activeInHierarchy)
        //    {
        //        return pooledBullets[i];
        //    }
        //}
        foreach (GameObject pb in pooledBullets)
        {
            if (!pb.activeInHierarchy)
            {
                return pb;
            }
        }
        if (canGrow)
        {
            GameObject bullet = Instantiate(prefabBullet);
            bullet.SetActive(true);
            bullet.transform.SetParent(transform);
            bullet.transform.position = GameManager.instance.player.transform.position + GameManager.instance.player.bulletSpawners[0];
            pooledBullets.Add(bullet);
            return bullet;
        }
        return null;
    }

    private void Update()
    {
        if (GameManager.instance.player.state == CharacterMovement.eState.NORMAL)
        {

            fireCooldown -= Time.deltaTime;

            if (Input.GetAxis("Fire1") > 0.1f)
            {
                if (fireCooldown < 0)
                {
                    GameObject newBullet = getPooledBullet();
                    if (newBullet == null)
                    {
                        return;
                    }
                    newBullet.GetComponent<Bullet>().Spawn(GameManager.instance.player.transform.position + GameManager.instance.player.bulletSpawners[0], GameManager.instance.player.gameObject);

                    fireCooldown = GameManager.instance.player.fireRate;
                }


            }

        }
    }
}
