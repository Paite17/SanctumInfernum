using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    // Sky broadband gave me it
    [SerializeField] UIHealthBar pHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //pHealth.health -= 10;
            other.GetComponent<UIHealthBar>().TakeDamage(10);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstruction"))
        {
            Destroy(gameObject);
        }
    }
}
