using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_forces : MonoBehaviour
{
    [SerializeField] en_info_base info;
    [SerializeField] float velClampGround, velClampAir;
    [SerializeField] float moveSpeedGround, moveSpeedAir;
    public bool moveForward;

    [SerializeField] float gravForceMax;
    [SerializeField] float gravGrowSpeed;
    float gravForceCurrent;

    private void FixedUpdate()
    {
        if(moveForward)
        {
            MoveForward();
        }

        if(!info.grounded)
        {
            HandleGrav();
        }
        else
        {
            gravForceCurrent = 0;
        }

        ClampVelocity();

        if (gravForceCurrent == 0) return;
        print(gravForceCurrent);
    }

    private void MoveForward()
    {
        info.rb.AddForce(transform.forward * (info.grounded ? moveSpeedGround : moveSpeedAir));
    }

    private void HandleGrav()
    {
        gravForceCurrent = Mathf.MoveTowards(gravForceCurrent, gravForceMax, gravGrowSpeed);
        info.rb.AddForce(Vector3.down * gravForceCurrent, ForceMode.Acceleration);
    }

    private void ClampVelocity()
    {
        info.rb.velocity = Vector3.ClampMagnitude(info.rb.velocity, (info.grounded ? velClampGround : velClampAir));
    }
}
