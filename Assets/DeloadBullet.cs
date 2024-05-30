using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeloadBullet : MonoBehaviour
{
    public float life = 3; //the number of seconds the bullet has left to live

    private void Awake()
    {
        Destroy(gameObject, life);
    }

     void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
