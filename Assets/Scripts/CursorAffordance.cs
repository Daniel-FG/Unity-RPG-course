using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{
    [SerializeField] private Texture2D walkable = null;
    [SerializeField] private Texture2D enemy = null;
    [SerializeField] private Texture2D nowhere = null;

    private Vector2 cursorHotSpot = new Vector2(0, 0);  //游標的點擊位置  (0, 0)為左上角滑鼠尖端
    private CameraRaycaster cameraRaycaster;
    // Use this for initialization
    void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (cameraRaycaster.LayerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkable, cursorHotSpot, CursorMode.Auto);  //Cursor.SetCursor() 設定自己想要的游標圖
                break;
            case Layer.Enemy:
                Cursor.SetCursor(enemy, cursorHotSpot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(nowhere, cursorHotSpot, CursorMode.Auto);
                return;
        }
    } 
}
