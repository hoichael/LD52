using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_ik_target_pos : MonoBehaviour
{
    public Transform currentTargetTrans;
    [SerializeField] float moveSpeed;
    private void Update()
    {
        //transform.localPosition = currentTargetTrans.position;
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentTargetTrans.position, moveSpeed * Time.deltaTime);
    }
}
