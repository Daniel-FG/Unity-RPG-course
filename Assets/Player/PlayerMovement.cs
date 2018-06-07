using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveToWalkableRadius = 0.2f;  //���a����ؼ��I����0.2���شN�|�P�w��F
    private ThirdPersonCharacter thirdPersonController;   // A reference to the ThirdPersonCharacter on the object
    private CameraRaycaster cameraRaycaster;
    private Vector3 clickPoint;  //�ƹ��I����m
    private Vector3 currentDestination;  //���a��ڤW�|���ʨ���Ӧ�m

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonController = GetComponent<ThirdPersonCharacter>();
        //currentDestination = transform.position;
    }

    private Vector3 ShortDestination(Vector3 destination, float shortening)  //�^�ǵu�����ت��a
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;  //��X�P�樫��V�@�˪��V�q�åB�Y�u
        return destination - reductionVector;  //��Y�u���V�q�æ^��
    }

    private void OnDrawGizmos()  //�bGame Scene����Gizmo���}����C�թI�s
    {
        Gizmos.color = Color.black;  //�]�w�C��
        Gizmos.DrawLine(transform.position, clickPoint);  //�b�Ѽƪ��_�I�P���I�e�u
        Gizmos.DrawSphere(clickPoint, 0.1f);  //�b���I�e�b�|��0.1f���y
    }
}

