using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    
    private PlayerStats ps;


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            ps = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
            ps.IncreaseHealth();
            Destroy(gameObject);
        }
    }
}
