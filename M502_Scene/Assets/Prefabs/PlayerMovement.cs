using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Tốc độ di chuyển và độ nhạy chuột")]
    public float moveSpeed = 4.0f;
    public float mouseSensitivity = 0.1f;

    [Header("Tham chiếu")]
    public Transform cameraTransform;

    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. XOAY CAMERA BẰNG NEW INPUT SYSTEM
        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;

            // Xoay nhân vật trái/phải
            transform.Rotate(Vector3.up * mouseDelta.x);

            // Xoay camera ngước/cúi
            verticalRotation -= mouseDelta.y;
            verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
            if (cameraTransform != null)
            {
                cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            }
        }

        // 2. DI CHUYỂN BẰNG PHÍM W, A, S, D
        if (Keyboard.current != null)
        {
            float moveX = 0f;
            float moveZ = 0f;

            if (Keyboard.current.wKey.isPressed) moveZ += 1f;
            if (Keyboard.current.sKey.isPressed) moveZ -= 1f;
            if (Keyboard.current.dKey.isPressed) moveX += 1f;
            if (Keyboard.current.aKey.isPressed) moveX -= 1f;

            Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Bấm phím ESC để nhả chuột
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}