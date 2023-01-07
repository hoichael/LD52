using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_gravity : MonoBehaviour
{
    [SerializeField] pl_state playerState;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] float gravForce;

    private void FixedUpdate()
    {
        if(!playerState.grounded)
        {
            ApplyForce();
        }
    }

    private void ApplyForce()
    {
        playerRB.AddForce(Vector3.down * gravForce, ForceMode.Acceleration);
    }
}
