using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // TODO - score increase
    // TODO - stat increase

    public enum eType
    {
        HP,
        HULLUP,
        DAMAGEUP,
        FIRERATEUP,
        SPEEDUP,
        MAGRADIUSUP,
        EXP,
    }
    public eType pickupType;

    float valueHP, valueHullUp, valueDamageUp, valueFireRateUp, valueSpeedUp, valueMagRadiusUp;

    int valueEXP = 1;
    public Material matHP, matHullUp, matDamageUp, matFireRateUp, matSpeedUp, matMagRadiusUp, matEXP;


    Rigidbody body;
    static float BLOWOUTFORCE = 150f;

    private void Awake()
    {
        body = transform.GetComponent<Rigidbody>();
        valueHP = valueHullUp = valueDamageUp = valueSpeedUp = valueMagRadiusUp = 0.01f;
        valueFireRateUp = 0.001f;
        //body.GetComponent<MeshRenderer>().material = matHP;
    }

    void Start()
    {
        //Spawned();
    }



    void Update()
    {
        body.position += new Vector3(0f, -GameManager.instance.player.worldSpeed);
        if (body.position.y < -16f)
        {
            TooFar();
        }
    }

    public void PickedUp()
    {
        transform.gameObject.SetActive(false);
        GameManager.instance.HUD.ScoreIncrement(5);
        switch(pickupType)
        {
            case eType.HP:
                GameManager.instance.player.PlayerStatGrowth(valueHullUp, valueHP * 16, valueDamageUp, valueDamageUp / 5, valueFireRateUp, valueSpeedUp);
                break;
            case eType.HULLUP:
                GameManager.instance.player.PlayerStatGrowth(valueHullUp * 16, valueHP, valueDamageUp, valueDamageUp / 5, valueFireRateUp, valueSpeedUp);
                break;
            case eType.DAMAGEUP:
                GameManager.instance.player.PlayerStatGrowth(valueHullUp, valueHP, valueDamageUp * 16, valueDamageUp / 5, valueFireRateUp, valueSpeedUp);
                break;
            case eType.FIRERATEUP:
                GameManager.instance.player.PlayerStatGrowth(valueHullUp, valueHP, valueDamageUp, valueDamageUp * 4, valueFireRateUp, valueSpeedUp);
                break;
            case eType.SPEEDUP:
                GameManager.instance.player.PlayerStatGrowth(valueHullUp, valueHP, valueDamageUp, valueDamageUp / 5, valueFireRateUp, valueSpeedUp * 4);
                break;
            case eType.MAGRADIUSUP:
                //GameManager.instance.player.PlayerStatGrowth(valueHullUp, valueHP, valueDamageUp, valueDamageUp / 5, valueFireRateUp, valueSpeedUp);
                GameManager.instance.player.GetComponent<PickupMagnet>().PlusMagnetRange(valueMagRadiusUp * 5);
                break;
            case eType.EXP:
                GameManager.instance.player.PlayerEXPGrowth(valueEXP);
                break;
        }
    }

    public void TooFar()
    {
        transform.gameObject.SetActive(false);
    }


    public void Spawn(Vector3 p_position, int p_eType, float p_scale = .2f)
    {
        Spawn(p_position, (eType)p_eType, p_scale);
    }

    public void Spawn(Vector3 p_position, eType p_eType, float p_scale = .2f)
    {
        transform.position = p_position;
        transform.localScale = new Vector3(p_scale, p_scale, p_scale);

        switch (p_eType)
        {
            case eType.HP:
                body.GetComponent<MeshRenderer>().material = matHP;
                break;
            case eType.HULLUP:
                body.GetComponent<MeshRenderer>().material = matHullUp;
                break;
            case eType.DAMAGEUP:
                body.GetComponent<MeshRenderer>().material = matDamageUp;
                break;
            case eType.FIRERATEUP:
                body.GetComponent<MeshRenderer>().material = matFireRateUp;
                break;
            case eType.SPEEDUP:
                body.GetComponent<MeshRenderer>().material = matSpeedUp;
                break;
            case eType.MAGRADIUSUP:
                body.GetComponent<MeshRenderer>().material = matMagRadiusUp;
                break;
            case eType.EXP:
                body.GetComponent<MeshRenderer>().material = matEXP;
                break;
        }

        transform.gameObject.SetActive(true);
    }

    public void Spawned()
    {
        //transform.gameObject.SetActive(true);
        body.AddForce(new Vector3(Random.Range(-BLOWOUTFORCE, BLOWOUTFORCE), Random.Range(BLOWOUTFORCE, -BLOWOUTFORCE)));
        //Debug.Log("MAT " + body.GetComponent<MeshRenderer>().material.name);
    }

    private void OnEnable()
    {
        Spawned();
    }
}
