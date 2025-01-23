using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Animator anim;
    private GameManager GM;
    private HealthBar hb;

    [SerializeField]
    public float maxHealth;

    private GameObject player;

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    private float currentHealth;
    public bool isDead;


    private void Start()
    {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        hb = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        hb.SetMaxHealth(maxHealth);

    }
    private void Update()
    {
        anim.SetBool("isDead", isDead);
       
    }

    public void DecreaseHealth(float amount)
    {
        
        currentHealth -= amount;
        anim.SetTrigger("isTakingDMG");
        hb.SetHealth(currentHealth);


        if (currentHealth <= 0.0f)
        {
            Die(); 
            player.gameObject.layer = 13;

        }
        else
        {
            player.gameObject.layer = 11;
        }

    }
    public void IncreaseHealth()
    {

        currentHealth = maxHealth;
        hb.SetHealth(currentHealth);

    }

    private void Die()
    {
        
        isDead = true;
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);

        GM.Respawn();
        Destroy(gameObject, 3);

    }
  
}
