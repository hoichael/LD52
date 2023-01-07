using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_jump : MonoBehaviour
{
    [SerializeField] pl_state playerState;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] float jumpForce;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerState.grounded)
        {
            ApplyJump();
        }    
    }

    private void ApplyJump()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);

        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
