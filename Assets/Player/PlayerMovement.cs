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
    private Vector3 clickPoint;  //�ƹ��I����m
    private Vector3 currentDestination;  //���a��ڤW�|���ʨ���Ӧ�m

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

    private Vector3 ShortDestination(Vector3 destination, float shortening)  //�^�ǵu�����ت��a
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;  //��X�P�樫��V�@�˪��V�q�åB�Y�u
        return destination - reductionVector;  //��Y�u���V�q�æ^��
    }

    //private void OnDrawGizmos()  //�bGame Scene����Gizmo���}����C�թI�s
    //{
    //    Gizmos.color = Color.black;  //�]�w�C��
    //    Gizmos.DrawLine(transform.position, clickPoint);  //�b�Ѽƪ��_�I�P���I�e�u
    //    Gizmos.DrawSphere(clickPoint, 0.1f);  //�b���I�e�b�|��0.1f���y
    //}
}

