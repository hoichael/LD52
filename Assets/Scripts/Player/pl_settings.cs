using UnityEngine;

[CreateAssetMenu(fileName = "pl_settings", menuName = "ScriptableObjects/Player/Settings", order = 0)]
public class pl_settings : ScriptableObject
{
    [field: SerializeField, Header("General")] public LayerMask solidLayerMask { get; private set; }
    [field: SerializeField] public float groundCheckRadius { get; private set; }

    [field: SerializeField, Header("Camera")] public float mouseSens { get; private set; }

    [field: SerializeField, Header("Move")] public float moveForceGround { get; private set; }
    [field: SerializeField] public float dragGround { get; private set; }
    [field: SerializeField] public float moveForceAir { get; private set; }
    [field: SerializeField] public float dragAir { get; private set; }

    [field: SerializeField, Header("Jump")] public float jumpForceBase { get; private set; }

    [field: SerializeField, Header("Gravity")] public float gravityForceMax { get; private set; }
    [field: SerializeField] public float gravityForceBase { get; private set; }
    [field: SerializeField] public float gravityGrowSpeed { get; private set; }
}
