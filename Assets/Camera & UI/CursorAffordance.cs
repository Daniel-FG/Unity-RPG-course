using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField] private Texture2D walkable = null;
    [SerializeField] private Texture2D enemy = null;
    [SerializeField] private Texture2D nowhere = null;

    private const int walkableLayer = 8;
    private const int enemyLayer = 9;
    private const int unknownLayer = 10;

    private Vector2 cursorHotSpot = new Vector2(0, 0);  //游標的點擊位置  (0, 0)為左上角滑鼠尖端
    private CameraRaycaster cameraRaycaster;
    // Use this for initialization
    private void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChange;  //將OnLayerChange()註冊進委派清單中
        //cameraRaycaster.LayerChangeObservers = OnLayerChange;  //此行意思為覆蓋而不是增加  使用event關鍵字後則不允許此寫法
    }

    // Update is called once per frame
    private void OnLayerChange(int newLayer)
    {
        switch (newLayer)  //TODO 考慮改成有變化時在改變就好了
        {
            case walkableLayer:
                Cursor.SetCursor(walkable, cursorHotSpot, CursorMode.Auto);  //Cursor.SetCursor() 設定自己想要的游標圖
                break;
            case enemyLayer:
                Cursor.SetCursor(enemy, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(nowhere, cursorHotSpot, CursorMode.Auto);
                return;
        }
    } 
}
