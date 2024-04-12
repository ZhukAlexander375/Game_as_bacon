using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArountPoint : MonoBehaviour
{
    [SerializeField] private Transform _rotationPoint;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _returnRotationSpeed = 100f;
    [SerializeField] private float _maxRotationAngle = 30f;

    private Quaternion initialRotation;
    private Vector3 initialPosition;

    [SerializeField] private bool isRotating = false;
    [SerializeField] private bool isReturning = false;

    [SerializeField] private Rigidbody2D _objectToThrow;
    [SerializeField] private float _throwForce = 50f;   

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

        if (angleDifference <= 1f)
        {
            transform.rotation = initialRotation;
            transform.position = initialPosition;
            isReturning = false;
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRotating)
        {
            if (collision.gameObject.CompareTag("Worm"))
            {
                ThrowObject();
            }
        }
    }
    private void ThrowObject()
    {       

        Vector2 throwDirection = (Vector2)(transform.position - _objectToThrow.transform.position);
        _objectToThrow.AddForce(throwDirection.normalized * _throwForce, ForceMode2D.Impulse);
    }
}

