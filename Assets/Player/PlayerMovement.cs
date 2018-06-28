using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (ThirdPersonCharacter))]
[RequireComponent(typeof (AICharacterControl))]
[RequireComponent(typeof (NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter thirdPersonController;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster cameraRaycaster;
    private AICharacterControl aiCharacterControl;
    private GameObject walkTarget;
    private Vector3 clickPoint;  //滑鼠點擊位置
    private Vector3 currentDestination;  //玩家實際上會移動到哪個位置

    private const int walkableLayer = 8;
    private const int enemyLayer = 9;
    private const int unknownLayer = 10;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonController = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("Walk Target");
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
        //currentDestination = transform.position;
    }

    private void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case enemyLayer:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayer:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
            default:
                print("Don't know what to handle player movement.");
                return;
        }
    }

    private Vector3 ShortDestination(Vector3 destination, float shortening)  //回傳短版的目的地
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;  //找出與行走方向一樣的向量並且縮短
        return destination - reductionVector;  //減掉縮短的向量並回傳
    }

    //private void OnDrawGizmos()  //在Game Scene中把Gizmo打開之後每禎呼叫
    //{
    //    Gizmos.color = Color.black;  //設定顏色
    //    Gizmos.DrawLine(transform.position, clickPoint);  //在參數的起點與終點畫線
    //    Gizmos.DrawSphere(clickPoint, 0.1f);  //在該點畫半徑為0.1f的球
    //}
}

