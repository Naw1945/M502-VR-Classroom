using UnityEngine;

[CreateAssetMenu(fileName = "NewObjectData", menuName = "Classroom/Object Data")]
public class ObjectDataSO : ScriptableObject
{
    public string objectName = "Tên thiết bị";
    public string objectType = "Loại thiết bị";
    [TextArea(3, 5)]
    public string specifications = "Thông số kỹ thuật...";
    public string status = "Hoạt động bình thường";
}