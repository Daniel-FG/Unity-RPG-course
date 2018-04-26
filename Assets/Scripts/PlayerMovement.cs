using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveToWalkableRadius = 0.2f;
    private ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentClickTarget;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            print("Cursor raycast hit" + cameraRaycaster.Hit.collider.gameObject.name.ToString());
            switch (cameraRaycaster.LayerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.Hit.point;
                    break;
                case Layer.Enemy:
                    print("Enemy hit");
                    break;
                default:
                    print("Shouldn't be here");
                    return;
            }
        }
        Vector3 movingVector = currentClickTarget - transform.position;
        if (movingVector.magnitude >= moveToWalkableRadius)
        {
            m_Character.Move(movingVector, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }
}

