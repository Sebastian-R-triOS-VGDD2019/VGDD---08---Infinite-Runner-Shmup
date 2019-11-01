using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float hullMaxStrength = 8f;
    public float hullHitPoints;
    public float baseDamage = 5f;
    public float damageMultiplier = 1f;
    //public float fireRate = 

    GameObject pickups;
    Animator anim;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        pickups = GameManager.instance.pickups;
    }



    void Update()
    {
        if (hullHitPoints <= 0f)
        {
            GameManager.instance.HUD.ScoreIncrement(50);
            transform.gameObject.SetActive(false);

            // HP
            int rng = Random.Range(0, 100);
            GameObject pickup = pickups.GetComponent<PickupPool>().getPooledPickup();
            pickup.GetComponent<Pickup>().Spawn(transform.position, Pickup.eType.HP);
            if (rng > 60)
            {
                pickup = pickups.GetComponent<PickupPool>().getPooledPickup();
                pickup.GetComponent<Pickup>().Spawn(transform.position, Pickup.eType.HP);
            }
            if (rng > 90)
            {
                pickup = pickups.GetComponent<PickupPool>().getPooledPickup();
                pickup.GetComponent<Pickup>().Spawn(transform.position, Pickup.eType.HP);
            }

            // Other stats
            int bitCount = Random.Range(1, 4);
            for (int i = 0; i < bitCount; i++)
            {
                pickup = pickups.GetComponent<PickupPool>().getPooledPickup();
                pickup.GetComponent<Pickup>().Spawn(transform.position, Random.Range(1, 4));
            }
        }
    }

    public void Spawn(float p_hullStrengthen = 0f)
    {
        hullMaxStrength += p_hullStrengthen;
        transform.gameObject.SetActive(true);
        StartCoroutine("triggerRandomFlyBy");
    }

    IEnumerator triggerRandomFlyBy()
    {
        yield return new WaitForSeconds(.05f);

        int rng = Random.Range(0, 3);
        //Debug.Log("rng: " + rng);
        switch (rng)
        {
            case 0:
                anim.SetTrigger("EnemyFlyBy01");
                break;
            case 1:
                anim.SetTrigger("EnemyFlyBy02");
                break;
            case 2:
                anim.SetTrigger("EnemyFlyBy03");
                break;
        }
    }

    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {

        hullHitPoints = hullMaxStrength;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFire")
        {
            //Debug.Log("FiredBy: " + other.GetComponent<Bullet>().firedBy.GetComponent<CharacterMovement>().BulletDamage());
            hullHitPoints -= other.GetComponent<Bullet>().firedBy.GetComponent<CharacterMovement>().BulletDamage();
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerHitbox")
        {
            GameManager.instance.player.ImpactDamage(hullMaxStrength / 30);
            hullHitPoints -= GameManager.instance.player.hullMaxStrength / 200;
        }
    }
}
