using UnityEngine;

public class pl_groundcheck : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] GameObject groundCheckTrans;
    [SerializeField] float checkRadius;
    [SerializeField] pl_gravity gravManager;

    private void Update()
    {
        CheckForGround();
        gravManager.enabled = refs.state.grounded ? false : true;
    }

    private void CheckForGround()
    {
        refs.state.grounded = Physics.CheckSphere(
            groundCheckTrans.transform.position,
            checkRadius,
            refs.settings.solidLayerMask);
    }
}
