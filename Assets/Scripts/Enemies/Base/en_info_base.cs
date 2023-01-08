using UnityEngine;

public class en_info_base : MonoBehaviour
{
    [Header("Local Refs")]

    public Transform trans;
    public Rigidbody rb;
    public Animator anim;
    public Collider col;

    [Header("Settings")]

    public float knockbackMult; // basically "weight" property, but higher value = lower weight

    [Header("State")]

    public bool grounded;
}
