using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Transform respawnPoint;
    [SerializeField]
    private GameObject player;

    private RespawnBar rb;


    [SerializeField]
    public float respawnTime;
    private float respawnLeft;
    [SerializeField]
    public float respawnMax;

    private float respawnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;

    private void Start()
    {
        CVC = GameObject.Find("Player camera").GetComponent<CinemachineVirtualCamera>();
        rb = GameObject.Find("RespawnBar").GetComponent<RespawnBar>();
        respawnLeft = respawnMax;
        rb.DrawHeart(respawnLeft);
    }

    private void Update()
    {
        CheckRespawn();
        CheckGameOver();
    }
    public void Respawn()
    {
             
        respawnTimeStart = Time.time;
        respawnLeft--;
        rb.DrawHeart(respawnLeft);
        respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn && respawnLeft >=0)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
            
        }
        
    }
    private void CheckGameOver()
    {
        if (respawnLeft == -1 && Time.time >= respawnTimeStart + respawnTime)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SetRespawn(Transform checkpoint)
    {
        respawnPoint = checkpoint; 
    }
}
