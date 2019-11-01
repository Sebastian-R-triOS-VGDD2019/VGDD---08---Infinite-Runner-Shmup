using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public SpriteRenderer playerSprite;

    float BoundaryPadding = 0.5f;

    public enum eState
    {
        STANDINGBY,
        NORMAL,
        DEAD
    }
    public eState state;

    public float hullMaxStrength = 100f;
    public float hullHitPoints;
    public float baseDamage = 10f;
    public float damageMultiplier = 1f;
    public float fireRate = .33f;
    public float speed = 10f;
    public int exp = 0;

    public float worldSpeed = .015f;

    Rigidbody body;
    //SphereCollider pickupMagnet;

    //public List<GameObject> bulletSpawners;
    public List<Vector3> bulletSpawners;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        //pickupMagnet = GetComponent<SphereCollider>();
        hullHitPoints = hullMaxStrength;
        //ChangeState(eState.STANDINGBY);
    }

    public void ChangeState(eState p_state)
    {
        switch (state)
        {
            case eState.STANDINGBY:
                Time.timeScale = 0;
                break;
            case eState.NORMAL:
                Time.timeScale = 1;
                break;
            case eState.DEAD:
                worldSpeed = 0f;
                break;
        }

        state = p_state;
    }

    private void Update()
    {
        if (state == eState.NORMAL)
        {

            if (worldSpeed < 0.05f)
            {
                worldSpeed += .000002f;
            }
            if (hullHitPoints <= 0f)
            {
                playerSprite.enabled = false;
                transform.GetComponent<BoxCollider>().enabled = false;
                
                ChangeState(eState.DEAD);
            }
        }
    }

    void FixedUpdate()
    {
        body.transform.position += new Vector3((Input.GetAxis("Hor") / 100) * speed, (Input.GetAxis("Ver") / 100) * speed);
        BoundaryCheck();
    }

    void BoundaryCheck()
    {
        if (body.transform.position.x < (-10f + BoundaryPadding))
            body.transform.position = new Vector3(-10f + BoundaryPadding, body.transform.position.y);
        if (body.transform.position.x > (10f - BoundaryPadding))
            body.transform.position = new Vector3(10f - BoundaryPadding, body.transform.position.y);
        if (body.transform.position.y < (-5f + BoundaryPadding))
            body.transform.position = new Vector3(body.transform.position.x, -5f + BoundaryPadding);
        if (body.transform.position.y > (5f - BoundaryPadding))
            body.transform.position = new Vector3(body.transform.position.x, 5f - BoundaryPadding);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            collision.collider.GetComponent<Pickup>().PickedUp();
        }
    }

    public void ImpactDamage(float p_value)
    {
        hullHitPoints -= p_value;
    }

    public float BulletDamage()
    {
        return baseDamage * damageMultiplier;
    }

    public void PlayerStatGrowth(float p_hullMax, float p_hitPoints, float p_baseD, float p_dMultiplier, float p_fireRate, float p_speed)
    {
        hullHitPoints += p_hitPoints;
        if (hullHitPoints >= hullMaxStrength)
        {
            hullMaxStrength += p_hullMax;
            hullHitPoints = hullMaxStrength;
        }
        baseDamage += p_baseD;
        damageMultiplier += p_dMultiplier;
        fireRate -= p_fireRate;
        if (fireRate < .05f)
        {
            fireRate = .05f;
        }
        speed += p_speed;
    }

    public void PlayerEXPGrowth(int p_exp)
    {
        exp += p_exp;
    }
}
