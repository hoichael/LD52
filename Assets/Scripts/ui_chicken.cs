using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_chicken : MonoBehaviour
{
    [SerializeField] float rotSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * (rotSpeed * Time.deltaTime));
    }
}
