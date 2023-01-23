using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_grav : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float gravForce;

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravForce, ForceMode.Acceleration);
    }
}
