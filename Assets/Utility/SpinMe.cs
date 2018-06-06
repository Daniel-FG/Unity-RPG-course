using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour
{

	[SerializeField] float xRotationsPerMinute = 1f;  //x方向旋轉一分鐘一圈
	[SerializeField] float yRotationsPerMinute = 1f;  //y方向旋轉一分鐘一圈
	[SerializeField] float zRotationsPerMinute = 1f;  //z方向旋轉一分鐘一圈

    void Update ()
    {
        //xDegreePerFrame = Time.deltaTime, 60, 360, xRotationPerMinute;
        //degrees Frame^-1 = second Frame^-1 * second Minute^-1 degree Circle^-1 Circle Minute^-1
        //degrees frame^-1 = 360 * Time.deltaTime * xRotationPerMinute / 60;
        //                             = (degree*circle^-1)*(second*frame^-1)*(circle*minute^-1)*(second^-1*minute)

        float xDegreesPerFrame = 360 * Time.deltaTime * xRotationsPerMinute / 60;
        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = 360 * Time.deltaTime * yRotationsPerMinute / 60;
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = 360 * Time.deltaTime * zRotationsPerMinute / 60;
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}
