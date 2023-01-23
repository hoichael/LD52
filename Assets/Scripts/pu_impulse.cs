using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pu_impulse : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private void OnEnable()
    {
        rb.AddForce(Vector3.up * 8f, ForceMode.Impulse);
        rb.AddTorque((Vector3.up + Vector3.right) * 0.4f, ForceMode.Impulse);
    }
}
