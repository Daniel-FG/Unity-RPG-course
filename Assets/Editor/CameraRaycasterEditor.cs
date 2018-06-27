using UnityEditor;

// TODO consider changing to a property drawer
[CustomEditor(typeof(CameraRaycaster))]
public class CameraRaycasterEditor : Editor
{
    bool isLayerPrioritiesUnfolded = true;  //用來儲存Inspector中的Layer Priority是否摺疊的狀態

    //自訂Inspector顯示的內容  但是原本Script中宇顯示的變數須為SerializeField  
    //沒經過Serialize的變數即使是public也不會主動顯示
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Serialize cameraRaycaster instance

        isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");
        if (isLayerPrioritiesUnfolded)  //如果目前是展開狀況
        {
            EditorGUI.indentLevel++;  //往內縮
            {
                BindArraySize();  //設定欄位
                BindArrayElements();  //設定內容
            }
            EditorGUI.indentLevel--;  //往外擴
            //PrintString();  //每禎更新文字
        }

        serializedObject.ApplyModifiedProperties(); // De-serialize back to cameraRaycaster (and create undo point)
    }

    void PrintString()
    {
        //不確定型別的時候使用var
        var stringToPrint = serializedObject.FindProperty("stringToPrint");  //找到CameraRaycaster.cs中掛上[SerializedField]的變數
        //字串文字區塊 EditorGUILayout.TextField("變數名稱", 格子內的文字)
        stringToPrint.stringValue = EditorGUILayout.TextField("string to print", stringToPrint.stringValue);  //將Inspector的文字變成變數的內容
    }

    //綁定陣列大小
    void BindArraySize()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;  //陣列大小
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);  //打數字的文字區域
        if (requiredArraySize != currentArraySize)  //如果陣列大小與Inspector中的數字不符合
        {
            serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;  //設定陣列大小與Inspector中的數字相同
        }
    }

    //綁定陣列內容
    void BindArrayElements()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;  //取得陣列大小
        for (int i = 0; i < currentArraySize; i++)
        {
            var prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));  //取得當前陣列欄位
            prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);  //設定陣列欄位
        }
    }
}
