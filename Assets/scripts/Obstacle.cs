using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void Update()
    {
        transform.position += new Vector3(0f, -GameManager.instance.player.worldSpeed);
        if (transform.position.y < -16f)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void Spawn()
    {
        transform.gameObject.SetActive(true);
        transform.localScale = new Vector3(Random.Range(.05f, 6f), Random.Range(.05f, 6f));
        transform.position = new Vector3(Random.Range(-8f, 8f), 7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFire")
        {
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerHitbox")
        {
            GameManager.instance.player.ImpactDamage(1f);
        }
    }

}
