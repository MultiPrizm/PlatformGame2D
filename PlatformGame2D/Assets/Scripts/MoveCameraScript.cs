using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{

    public Transform target; // Целевой объект, за которым будет следовать камера
    public float smoothSpeed = 0.125f; // Скорость перемещения камеры
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
            // Получаем позицию, к которой должна двигаться камера
            Vector3 desiredPosition = target.position;
            desiredPosition.z = transform.position.z; // Убедимся, что камера не меняет своей глубины

            // Используем метод SmoothDamp для плавного перемещения камеры к целевой позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Обновляем позицию камеры
            transform.position = smoothedPosition;
        }
    }
}
