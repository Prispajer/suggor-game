using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBar : MonoBehaviour
{
    public GameObject HeartPrefab;

    public void DrawHeart(float heart)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i <heart; i++)
        {
            if(i + 1 <= heart)
            {
                GameObject health = Instantiate(HeartPrefab, transform.position, Quaternion.identity);
                health.transform.SetParent(transform); 

            }
        }
    }
}
