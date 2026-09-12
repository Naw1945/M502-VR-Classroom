using UnityEngine;
using TMPro;

public class InfoUIController : MonoBehaviour
{
    public static InfoUIController Instance { get; private set; }

    [SerializeField] private GameObject panelRoot;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtType;
    [SerializeField] private TextMeshProUGUI txtSpecs;
    [SerializeField] private TextMeshProUGUI txtStatus;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        HideInfo();
    }

    public void ShowInfo(ObjectDataSO data)
    {
        if (data == null) return;
        txtName.text = data.objectName;
        txtType.text = $"Phân loại: {data.objectType}";
        txtSpecs.text = data.specifications;
        txtStatus.text = $"Trạng thái: {data.status}";
        panelRoot.SetActive(true);
    }

    public void HideInfo()
    {
        if (panelRoot != null) panelRoot.SetActive(false);
    }

    public bool IsOpen => panelRoot != null && panelRoot.activeSelf;
}