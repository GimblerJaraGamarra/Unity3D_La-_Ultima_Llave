using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLookAt : MonoBehaviour
{
    public float mouseIntensity = 80;

    public Transform playerBody;

    float xrotation = 0;

    public PlayerInput playerInput;
    public InputAction playerRotationAction;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerRotationAction = playerInput.actions["rotation"];
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseIntensity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseIntensity * Time.deltaTime;

        //var rotate = playerRotationAction.ReadValue<Vector2>();

        //float mouseX = rotate.x * mouseIntensity * Time.deltaTime;

        //float mouseY = rotate.y * mouseIntensity * Time.deltaTime;

        xrotation -= mouseY;

        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xrotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
