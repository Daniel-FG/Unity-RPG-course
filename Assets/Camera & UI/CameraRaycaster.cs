using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

public class CameraRaycaster : MonoBehaviour
{
    public delegate void OnCursorLayerChange(int newLayer);  //宣告委派類別  用於通知滑鼠所指到的Layer改變
    public event OnCursorLayerChange notifyLayerChangeObservers;

    public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit);  //宣告委派類別  用於通知滑鼠點擊時的最高Layer層級為何
    public event OnClickPriorityLayer notifyMouseClickObservers; // instantiate an observer set
    
    //SerializeField的變數可以利用自訂Editor Script顯示
    [SerializeField] private int[] layerPriorities;
    //[SerializeField] private string stringToPrint = "new string";

    private float maxRaycastDepth = 100f;  //寫死的最大Raycast範圍
	private int topPriorityLayerLastFrame = -1;  //設成-1之後在最初地形上可以得到?問號游標

    void Update()
	{
		
		if (EventSystem.current.IsPointerOverGameObject ())  //如果游標在UI物件上顯示問號
        {
			NotifyObserersIfLayerChanged (5);  //回傳目前是在UI Layer  將會顯示問號
			return;  //不再找尋UI背後的layer
		}

		// Raycast to max depth, every frame as things can move under mouse
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] raycastHits = Physics.RaycastAll (ray, maxRaycastDepth);

        RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);
        if (!priorityHit.HasValue) // if hit no priority object
		{
			NotifyObserersIfLayerChanged (0); // broadcast default layer
			return;
		}

		// Notify delegates of layer change
		var layerHit = priorityHit.Value.collider.gameObject.layer;
		NotifyObserersIfLayerChanged(layerHit);
		
		// Notify delegates of highest priority game object under mouse when clicked
		if (Input.GetMouseButton (0))
		{
			notifyMouseClickObservers (priorityHit.Value, layerHit);
		}

        print(stringToPrint);
	}

	void NotifyObserersIfLayerChanged(int newLayer)
	{
		if (newLayer != topPriorityLayerLastFrame)
		{
			topPriorityLayerLastFrame = newLayer;
			notifyLayerChangeObservers (newLayer);
		}
	}

	RaycastHit? FindTopPriorityHit (RaycastHit[] raycastHits)
	{
		// Form list of layer numbers hit
		List<int> layersOfHitColliders = new List<int> ();
		foreach (RaycastHit hit in raycastHits)
		{
			layersOfHitColliders.Add (hit.collider.gameObject.layer);
		}

		// Step through layers in order of priority looking for a gameobject with that layer
		foreach (int layer in layerPriorities)
		{
			foreach (RaycastHit hit in raycastHits)
			{
				if (hit.collider.gameObject.layer == layer)
				{
					return hit; // stop looking
				}
			}
		}
		return null; // because cannot use GameObject? nullable
	}
}