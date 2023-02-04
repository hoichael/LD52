using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_grav : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float gravForce;
    [SerializeField] en_info_base info;
    [SerializeField] float gravGrowSpeed;
    float currentGravForce;

    private void FixedUpdate()
    {
        //print(currentGravForce);
        //if(!info.grounded)
        //{
        //    currentGravForce = Mathf.MoveTowards(currentGravForce, gravForce, gravGrowSpeed);
        //    rb.AddForce(Vector3.down * currentGravForce, ForceMode.Acceleration);
        //}
        //else
        //{
        //    currentGravForce = 0;
        //}
    }
}
