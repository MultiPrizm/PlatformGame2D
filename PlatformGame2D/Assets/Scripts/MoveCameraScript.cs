using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{

    public Transform target; // ������� ������, �� ������� ����� ��������� ������
    public float smoothSpeed = 0.125f; // �������� ����������� ������
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        LateCamera();
    }


    void LateCamera()
    {
        if (target != null)
        {
            // �������� �������, � ������� ������ ��������� ������
            Vector3 desiredPosition = target.position;
            desiredPosition.z = transform.position.z; // ��������, ��� ������ �� ������ ����� �������

            // ���������� ����� SmoothDamp ��� �������� ����������� ������ � ������� �������
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // ��������� ������� ������
            transform.position = smoothedPosition;
        }
    }
}
