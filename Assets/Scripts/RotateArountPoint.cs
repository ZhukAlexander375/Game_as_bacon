using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArountPoint : MonoBehaviour
{
    [SerializeField] private Transform _rotationPoint;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _returnRotationSpeed;
    [SerializeField] private float _maxRotationAngle;

    private Quaternion initialRotation;
    private Vector3 initialPosition;

    [SerializeField] private bool isRotating = false;
    [SerializeField] private bool isReturning = false;
    

    void Start()
    {
        initialRotation = transform.rotation;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isRotating = true;
            isReturning = false;
        }

        else
        {
            if (isRotating && !isReturning)
            {
                isRotating = false;
                isReturning = true;
            }
        }

        if (isRotating)
        {
            Rotate();
        }

        if (isReturning)
        {
            ReturnToInitialPosition();
        }

    }
    private void Rotate()
    {
        
        transform.RotateAround(_rotationPoint.position, Vector3.forward, _rotationSpeed * Time.deltaTime);

        float currentAngle = Quaternion.Angle(initialRotation, transform.rotation);

        if (currentAngle >= _maxRotationAngle)
        {
            isRotating = false;
            isReturning = true;
        }
    }

    private void ReturnToInitialPosition()
    {
        transform.RotateAround(_rotationPoint.position, Vector3.forward, -(_returnRotationSpeed * Time.deltaTime));

        float angleDifference = Quaternion.Angle(initialRotation, transform.rotation);

        if (angleDifference <= 0.5f)
        {
            transform.rotation = initialRotation;
            transform.position = initialPosition;
            isReturning = false;
        }  
    }  
}

