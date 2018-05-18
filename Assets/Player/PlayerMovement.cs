using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveToWalkableRadius = 0.2f;  //玩家走到目標點附近0.2公尺就會判定到達
    private ThirdPersonCharacter thirdPersonController;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster cameraRaycaster;
    private Vector3 clickPoint;  //滑鼠點擊位置
    private Vector3 currentDestination;  //玩家實際上會移動到哪個位置

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonController = GetComponent<ThirdPersonCharacter>();
        //currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //print("Cursor raycast hit " + cameraRaycaster.Hit.collider.gameObject.name.ToString());
            
            switch (cameraRaycaster.LayerHit)
            {
                case Layer.Walkable:
                    print("Walkable hit");
                    clickPoint = cameraRaycaster.Hit.point;
                    break;
                case Layer.Enemy:
                    print("Enemy hit");
                    break;
                default:
                    print("Shouldn't be here");
                    return;
            }
        }

        Vector3 movingVector = clickPoint - transform.position;
        if (movingVector.magnitude >= moveToWalkableRadius)
        {
            thirdPersonController.Move(movingVector, false, false);  //移動
        }
        else
        {
            thirdPersonController.Move(Vector3.zero, false, false);  //不要移動
        }

    }

    private Vector3 ShortDestination(Vector3 destination, float shortening)  //回傳短版的目的地
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;  //找出與行走方向一樣的向量並且縮短
        return destination - reductionVector;  //減掉縮短的向量並回傳
    }

    private void OnDrawGizmos()  //在Game Scene中把Gizmo打開之後每禎呼叫
    {
        Gizmos.color = Color.black;  //設定顏色
        Gizmos.DrawLine(transform.position, clickPoint);  //在參數的起點與終點畫線
        Gizmos.DrawSphere(clickPoint, 0.1f);  //在該點畫半徑為0.1f的球
    }
}

