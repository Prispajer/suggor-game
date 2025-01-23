using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float currentHealth;
    float currentStunResistance;
    public GameObject hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 500;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;
        Instantiate(hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }
}
