using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en_st_shooter_jump : en_state_base
{
    [SerializeField] float rotSlerpDamp;
    [SerializeField] float delayBeforeJump;
    [SerializeField] float jumpForceUp, jumpForceForward;
    [SerializeField] Transform groundcheckTrans;
    [SerializeField] float groundcheckRadius;
    [SerializeField] LayerMask groundMask;

    [SerializeField] float gravForce;
    [SerializeField] float airDrag, groundDrag;

    [SerializeField] AudioSource jumpAudioSrc, landAudioSrc;

    Quaternion currentTargetRot;
    bool conductGroundcheck;

    protected override void OnEnable()
    {
        base.OnEnable();

        conductGroundcheck = false;

        info.anim.CrossFade("Jump", 0.3f);

        Vector3 randomPos = transform.position + Random.insideUnitSphere;
        Vector3 targetVec = randomPos - info.trans.position;
        targetVec.y = 0;
        currentTargetRot = Quaternion.LookRotation(targetVec);

        StartCoroutine(HandleDelay());
    }

    private void FixedUpdate()
    {
        if (conductGroundcheck)
        {
            //ApplyGravity();
            HandleGroundcheck();
        }
        else
        {
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        info.trans.localRotation = Quaternion.Slerp(info.trans.localRotation, currentTargetRot, rotSlerpDamp);
    }

    private void ApplyGravity()
    {
        info.rb.AddForce(Vector3.down * gravForce, ForceMode.Acceleration);
    }

    private void HandleGroundcheck()
    {
        if(Physics.CheckSphere(
            groundcheckTrans.position,
            groundcheckRadius,
            groundMask))
        {
            g_refs.Instance.pool.Dispatch(PoolType.vfx_jump_big, groundcheckTrans.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            info.rb.velocity = Vector3.zero;
            info.rb.drag = groundDrag;
            info.rb.interpolation = RigidbodyInterpolation.None;
            landAudioSrc.Play();
            ChangeState("idle");
        }
    }

    private IEnumerator HandleDelay()
    {
        yield return new WaitForSeconds(delayBeforeJump);
        ExecuteJump();

        yield return new WaitForSeconds(0.3f);
        conductGroundcheck = true;
    }

    private void ExecuteJump()
    {
        g_refs.Instance.pool.Dispatch(PoolType.vfx_jump_small, groundcheckTrans.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        info.rb.interpolation = RigidbodyInterpolation.Interpolate; // rb interp for smoother jump. reset to none after landing for performance
        info.rb.drag = airDrag;
        info.rb.AddForce(Vector3.up * jumpForceUp, ForceMode.Impulse);
        info.rb.AddForce(info.trans.forward * jumpForceForward, ForceMode.Impulse);
        jumpAudioSrc.Play();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
    }
}
