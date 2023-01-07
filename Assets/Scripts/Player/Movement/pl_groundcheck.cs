using UnityEngine;

public class pl_groundcheck : MonoBehaviour
{
    [SerializeField] pl_state state;
    [SerializeField] GameObject groundCheckTrans;
    [SerializeField] LayerMask solidLayerMask;
    [SerializeField] float checkRadius;

    private void Update()
    {
        CheckForGround();
    }

    private void CheckForGround()
    {
        state.grounded = Physics.CheckSphere(
            groundCheckTrans.transform.position,
            checkRadius,
            solidLayerMask);
    }
}
