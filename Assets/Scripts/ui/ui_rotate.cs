using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_rotate : MonoBehaviour
{
    [SerializeField] float rotSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * (-rotSpeed * Time.deltaTime));
    }

    private void OnDisable()
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
