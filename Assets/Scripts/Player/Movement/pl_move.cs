using UnityEngine;

public class pl_move : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] Transform orientation;
    //[SerializeField] AudioSource footstepAudio;

    //bool audioPlaying;

    private void FixedUpdate()
    {
        ApplyForce(GetMoveDirection());
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 dir =
            orientation.forward * Input.GetAxisRaw("Vertical") +
            orientation.right * Input.GetAxisRaw("Horizontal");

        //if (refs.state.grounded && dir.normalized != Vector3.zero)
        //{
        //    if (audioPlaying) return dir.normalized;
        //    footstepAudio.Play();
        //    audioPlaying = true;
        //}
        //else
        //{
        //    if (!audioPlaying) return dir.normalized;
        //    footstepAudio.Stop();
        //    audioPlaying = false;
        //}

        return dir.normalized;
    }

    private void ApplyForce(Vector3 moveDir)
    {
        refs.playerRB.AddForce(moveDir * (refs.state.grounded ? refs.settings.moveForceBase : refs.settings.moveForceBase * refs.settings.moveForceAirMult), ForceMode.Acceleration);
    }
}
