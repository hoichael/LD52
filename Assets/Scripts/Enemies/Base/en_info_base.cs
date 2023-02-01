using UnityEngine;

public class en_info_base : MonoBehaviour
{
    [Header("Local Refs")]

    public en_brain brain;
    public Transform trans;
    public Rigidbody rb;
    public Animator anim;
    public Collider col;
    public Transform groundcheckPos;

    [Header("Settings")]

    public float knockbackMult; // basically "weight" property, but higher value = lower weight
    public float groundcheckRadius;
    public int score;

    [Header("State")]

    public bool grounded;
}
