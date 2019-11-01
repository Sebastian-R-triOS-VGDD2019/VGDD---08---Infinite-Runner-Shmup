using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMagnet : MonoBehaviour
{
    public float magStrengthMultiplier = 0;
    public float magRadiusStart;
    public float magRadiusGrowth = 0;

    float distance;
    float newRadius;

    void Start()
    {
        magRadiusStart = transform.GetComponent<SphereCollider>().radius;
        //Debug.Log("radisu start: " + magRadiusStart);
    }



    void FixedUpdate()
    {
        //PlusMagnetRange(.001f);
        //Debug.Log("radius growth" + magRadiusGrowth);


        newRadius = magRadiusStart + magRadiusGrowth;
        transform.GetComponent<SphereCollider>().radius = newRadius; //new Vector3(newRadius, newRadius, newRadius);
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.instance.player.state == CharacterMovement.eState.NORMAL)
        {
            if (other.tag == "Pickup")
            {
                //Debug.Log("PICKUP!!!");
                Vector3 pullDirection = (transform.position - other.transform.position).normalized;
                distance = Vector3.Distance(transform.position, other.transform.position);

                float magProxStrength = newRadius - distance;
                if (magProxStrength < 0f)
                    magProxStrength = 0f;
                other.transform.position += (pullDirection / 10) * magProxStrength;
                //Debug.LogError("Distance: " + distance);
            }
            else
            {
            }

        }
    }

    public void PlusMagnetRange(float f_range)
    {
        magRadiusGrowth += f_range;
    }
}
