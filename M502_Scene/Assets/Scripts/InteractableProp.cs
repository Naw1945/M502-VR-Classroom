using UnityEngine;

public class InteractableProp : MonoBehaviour, IInteractable
{
    [SerializeField] private ObjectDataSO data;

    public string GetInteractPrompt()
    {
        return data != null ? $"[F] Xem thông tin {data.objectName}" : "[F] Tương tác";
    }

    public void Interact()
    {
        Debug.Log($"[InteractableProp] Gọi hàm Interact trên: {gameObject.name}");

        if (InfoUIController.Instance == null)
        {
            Debug.LogError("[LỖI] InfoUIController.Instance bị NULL! Hãy chắc chắn đối tượng InfoPanel trên Canvas đang được TÍCH BẬT (Active) trong Hierarchy.");
            return;
        }

        if (data == null)
        {
            Debug.LogError($"[LỖI] Vật thể {gameObject.name} chưa được gán file dữ liệu vào ô 'Data' trong Inspector!");
            return;
        }

        if (InfoUIController.Instance.IsOpen)
        {
            InfoUIController.Instance.HideInfo();
        }
        else
        {
            InfoUIController.Instance.ShowInfo(data);
        }
    }
}