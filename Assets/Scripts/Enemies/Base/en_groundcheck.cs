using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_groundcheck : MonoBehaviour
{
    [SerializeField] en_info_base info;
    [SerializeField] LayerMask solidMask;
    private void FixedUpdate()
    {
        info.grounded = Physics.CheckSphere(
            info.groundcheckPos.position,
            info.groundcheckRadius,
            solidMask);
    }
}
