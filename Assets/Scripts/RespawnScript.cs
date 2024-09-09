using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    // Player
    public Transform player;
    public Rigidbody rb_Player;
    
    // RespawnPoint
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
            //outSound.Play();

            rb_Player.velocity = Vector3.zero;
        }
    }
}
