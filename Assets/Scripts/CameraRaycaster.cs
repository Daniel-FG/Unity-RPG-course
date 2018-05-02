﻿using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities =  { Layer.Enemy, Layer.Walkable };

    [SerializeField] private float distanceToBackground = 100f;
    private Camera viewCamera;

    private RaycastHit m_hit;
    public RaycastHit Hit
    {
        get { return m_hit; }
    }

    private Layer m_layerHit;
    public Layer LayerHit
    {
        get { return m_layerHit; }
    }

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                m_layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
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
