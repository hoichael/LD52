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

        // both of these dont really belong here. wthv.
        gravManager.enabled = refs.state.grounded ? false : true;
        refs.playerRB.drag = refs.state.grounded ? refs.settings.dragGround : refs.settings.dragAir;
    }

    private void CheckForGround()
    {
        refs.state.grounded = Physics.CheckSphere(
            groundCheckTrans.transform.position,
            checkRadius,
            refs.settings.solidLayerMask);
    }
}
