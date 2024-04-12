using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _wormRigidbody;
    [SerializeField] private Transform _startPosition;

    private Quaternion initialRotation;    

    void Start()
    {
        initialRotation = transform.rotation;       
        _wormRigidbody.gravityScale = 0f;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && _wormRigidbody.gravityScale == 0f)
        {
            _wormRigidbody.gravityScale = 1f;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {          
            ReturnToInitialPosition();
        }
    }
    public void ReturnToInitialPosition()
    {
        _wormRigidbody.velocity = Vector2.zero;
        _wormRigidbody.angularVelocity = 0f;
        transform.position = _startPosition.position;
        transform.rotation = initialRotation;
        _wormRigidbody.gravityScale = 0f;
    }
}
