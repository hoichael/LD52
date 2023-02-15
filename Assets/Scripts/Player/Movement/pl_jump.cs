using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_jump : MonoBehaviour
{
    [SerializeField] pl_refs refs;

    int jumpCount;

    void Start() {
        jumpCount = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            ApplyJump();
            jumpCount += 1;
        }

        if (refs.state.grounded)
        {
            jumpCount = 0;
        }
    }

    private void ApplyJump()
    {
        refs.playerRB.velocity = new Vector3(refs.playerRB.velocity.x, 0, refs.playerRB.velocity.z);

        refs.playerRB.AddForce(Vector3.up * refs.settings.jumpForceBase, ForceMode.Impulse);
    }
}
