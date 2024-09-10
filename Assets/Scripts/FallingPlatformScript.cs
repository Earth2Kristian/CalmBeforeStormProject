using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float fallingDelay = 1f;

    void Start()
    {
        rigidBody.GetComponent<Rigidbody>();
        rigidBody.GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        { 
           StartCoroutine (Fall());
        
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Fall());
           
        }
    }


    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallingDelay);
        rigidBody.GetComponent <Rigidbody>().useGravity = true;
    }
}
