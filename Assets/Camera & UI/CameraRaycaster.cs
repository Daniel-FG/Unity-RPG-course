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
    
    // INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
    [SerializeField] private int[] layerPriorities;

    private float maxRaycastDepth = 100f; // Hard coded value
	private int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

    void Update()
	{
		// Check if pointer is over an interactable UI element
		if (EventSystem.current.IsPointerOverGameObject ())
		{
			NotifyObserersIfLayerChanged (5);
			return; // Stop looking for other objects
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