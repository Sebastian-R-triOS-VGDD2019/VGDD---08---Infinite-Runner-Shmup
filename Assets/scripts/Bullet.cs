using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody body;
    Vector3 spawnPosition;
    public GameObject firedBy;

    public void Spawn(Vector3 p_spawnPosition, GameObject p_firedBy)
    {
        spawnPosition = p_spawnPosition;
        transform.gameObject.SetActive(true);
        firedBy = p_firedBy;
    }

    private void Awake()
    {
        body = transform.GetComponent<Rigidbody>();
    }

    void Start()
    {
        //Debug.Log("pos: " + body.position);
        //Debug.Log("pos: " + GameManager.instance.player.transform.position);
    }


    void FixedUpdate()
    {
        body.position += new Vector3(0f, .5f);

        //Debug.Log("bs: " + (GameManager.instance.player.transform.position + GameManager.instance.player.bulletSpawners[0]));
        if (body.position.x > 11f || body.position.x < -11f || body.position.y > 6f || body.position.y < -6f)
            transform.gameObject.SetActive(false);
            
    }

    private void OnEnable()
    {
        //body.position = (GameManager.instance.player.transform.position + GameManager.instance.player.bulletSpawners[0]);
        body.position = spawnPosition;
    }
}
