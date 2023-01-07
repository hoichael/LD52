using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_gravity : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    public float currentGrav; // public for db

    private void OnEnable()
    {
        currentGrav = refs.settings.gravityForceBase;
    }
    private void FixedUpdate()
    {
        GrowGravity();
        ApplyForce();
    }

    private void GrowGravity()
    {
        currentGrav = Mathf.MoveTowards(currentGrav, refs.settings.gravityForceMax, refs.settings.gravityGrowSpeed);
    }

    private void ApplyForce()
    {
        refs.playerRB.AddForce(Vector3.down * currentGrav, ForceMode.Acceleration);
    }
}
