using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFall : MonoBehaviour
{
    [SerializeField]
    float fallSpeed = 1.5f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.MovePosition(transform.position - Vector3.up * Time.deltaTime * fallSpeed);
    }
}
