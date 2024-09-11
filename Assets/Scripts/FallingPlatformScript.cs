using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    private static FallingPlatformScript instance = null;

    public Rigidbody rigidBody;
    public float fallingDelay = 1f;
    public bool platformFall = false;

    void Start()
    {
        rigidBody.GetComponent<Rigidbody>();
        rigidBody.GetComponent<Rigidbody>().useGravity = false;
    }

    void Update()
    {
       if (platformFall == true)
       {
            StartCoroutine(Fall());
       }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        { 
           StartCoroutine (Fall());
            platformFall = true;
        
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

    void Awake()
    {
        instance = this;
    }

    public static FallingPlatformScript Instance
    {
        get
        {
            return instance;
        }
    }
}
