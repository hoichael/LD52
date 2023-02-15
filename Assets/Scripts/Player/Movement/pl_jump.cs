using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_jump : MonoBehaviour
{
    [SerializeField] pl_refs refs;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)/* TODO && refs.state.grounded*/)
        {
            ApplyJump();
        }
    }

    private void ApplyJump()
    {
        refs.playerRB.velocity = new Vector3(refs.playerRB.velocity.x, 0, refs.playerRB.velocity.z);

        refs.playerRB.AddForce(Vector3.up * refs.settings.jumpForceBase, ForceMode.Impulse);
    }
}
