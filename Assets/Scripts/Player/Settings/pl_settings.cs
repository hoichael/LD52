using UnityEngine;

[CreateAssetMenu(fileName = "pl_settings", menuName = "ScriptableObjects/Player/Settings", order = 0)]
public class pl_settings : ScriptableObject
{
    [field: SerializeField, Header("General")] public LayerMask solidLayerMask { get; private set; }
    [field: SerializeField] public float groundCheckRadius { get; private set; }
    [SerializeField] public int maxHP; // public set because upgrade system
    [field: SerializeField] public float interactRange { get; private set; }

    [field: SerializeField, Header("Camera")] public float mouseSens { get; private set; }

    [SerializeField, Header("Move")] public float moveForceBase; // public set because upgrade system
    [field: SerializeField] public float dragGround { get; private set; }
    [field: SerializeField] public float moveForceAirMult { get; private set; }
    [field: SerializeField] public float dragAir { get; private set; }

    [SerializeField, Header("Jump")] public float jumpForceBase; // public set because upgrade system

    [field: SerializeField, Header("Gravity")] public float gravityForceMax { get; private set; }
    [field: SerializeField] public float gravityForceBase { get; private set; }
    [field: SerializeField] public float gravityGrowSpeed { get; private set; }
}
