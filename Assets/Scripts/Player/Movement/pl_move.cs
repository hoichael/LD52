using UnityEngine;

public class pl_move : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform orientation;

    private void FixedUpdate()
    {
        ApplyForce(GetMoveDirection());
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 dir =
            orientation.forward * Input.GetAxisRaw("Vertical") +
            orientation.right * Input.GetAxisRaw("Horizontal");

        return dir.normalized;
    }

    private void ApplyForce(Vector3 moveDir)
    {
        refs.playerRB.AddForce(moveDir * refs.settings.moveForceGround, ForceMode.Acceleration);
    }
}
