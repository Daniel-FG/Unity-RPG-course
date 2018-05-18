using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities =  { Layer.Enemy, Layer.Walkable };

    [SerializeField] private float distanceToBackground = 100f;
    private Camera viewCamera;

    private RaycastHit hit;
    public RaycastHit Hit
    {
        get { return hit; }
    }

    private Layer layerHit, temp;
    public Layer LayerHit
    {
        get { return layerHit; }
    }

    public delegate void OnLayerChange(Layer newLayer);  //宣告委派函式
    public event OnLayerChange LayerChangeObservers;  //新建一個委派清單  可以省略event關鍵字
    

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var rayHit = RaycastForLayer(layer);
            if (rayHit.HasValue)
            {
                hit = rayHit.Value;
                if (layerHit != layer)
                {
                    layerHit = layer;
                    LayerChangeObservers(layer);  //呼叫委派關鍵字  傳送訊息到委派清單中
                }
                return;
            }
        }

        // Otherwise return background hit
        hit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;
    }

    //在回傳類別後方加上一個?  可以讓函式回傳null
    private RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;  //並不是每一次都可以傳回hit
        }
        return null;  //其他狀況回傳null
    }
}
