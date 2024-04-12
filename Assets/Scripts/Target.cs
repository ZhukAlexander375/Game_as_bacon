using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [SerializeField] private Worm _worm;
    [SerializeField] private Rigidbody2D _wormRigidbody;    
    [SerializeField] private float _timer = 0f;
    [SerializeField] private bool _isObjectStopped = false;
    private float _wormVelocity;
    public UnityEvent OnWinning = new();

    private void FixedUpdate()
    {
       _wormVelocity = _wormRigidbody.velocity.magnitude;

        if (_isObjectStopped) 
        {
            _timer += Time.fixedDeltaTime;
            if (_timer >= 1.5f)
            {                
                Win();                
            }             
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Worm") && _wormVelocity < 0.01f)
        {                       
            _isObjectStopped = true;
        }
        else
        {
           _isObjectStopped = false;
            _timer = 0f;
        }
    } 
    private void Win()
    {
        Debug.Log("Win");
        OnWinning.Invoke();
        _isObjectStopped = false;
        _timer = 0f;
        _worm.ReturnToInitialPosition();
    }
}
