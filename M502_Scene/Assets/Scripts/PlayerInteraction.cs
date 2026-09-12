using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // Thư viện New Input System

public class PlayerInteraction : MonoBehaviour
{
    [Header("Cấu hình Raycast")]
    [SerializeField] private float reachDistance = 5.0f;
    [SerializeField] private LayerMask interactLayer;

    [Header("Giao diện HUD")]
    [SerializeField] private TextMeshProUGUI promptText;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;

        if (promptText != null) 
            promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cam == null) return;

        // Bắn tia từ giữa màn hình
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance, interactLayer))
        {
            IInteractable target = hit.collider.GetComponent<IInteractable>();
            if (target != null)
            {
                if (promptText != null)
                {
                    promptText.text = target.GetInteractPrompt();
                    promptText.gameObject.SetActive(true);
                }

                // Bắt phím F hoặc Click chuột trái theo chuẩn New Input System
                bool isFPressed = Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame;
                bool isMouseLeftPressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

                if (isFPressed || isMouseLeftPressed)
                {
                    Debug.Log($"[Interaction] Tương tác thành công với: {hit.collider.gameObject.name}");
                    target.Interact();
                }
                return;
            }
        }

        // Khi quay đầu ra ngoài
        if (promptText != null && promptText.gameObject.activeSelf)
        {
            promptText.gameObject.SetActive(false);
        }

        if (InfoUIController.Instance != null && InfoUIController.Instance.IsOpen)
        {
            InfoUIController.Instance.HideInfo();
        }
    }
}