using UnityEngine;

public class pl_cam_rot : MonoBehaviour
{
    [SerializeField] float sens;
    [SerializeField] Transform camHolder;
    [SerializeField] Transform orientation;

    float rotX, rotY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        HandleInput();
        ApplyRotation();
    }

    private void HandleInput()
    {
        rotY += Input.GetAxisRaw("Mouse X") * sens;
        rotX -= Input.GetAxisRaw("Mouse Y") * sens;

        rotX = Mathf.Clamp(rotX, -89.5f, 89.5f);       
    }

    private void ApplyRotation()
    {
        camHolder.localRotation = Quaternion.Euler(rotX, rotY, 0);

        // technically doesnt belong here but too convenient not to do it
        orientation.rotation = Quaternion.Euler(0, rotY, 0);
    }
}

