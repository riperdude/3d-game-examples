using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JumpingWithJohn : MonoBehaviour
{
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public Transform checkointAreaObject;
    public bool isAtCheckpoint = false;
    public float turnSpeed = 7f;
    public float outOfBounds = -10;
    public float movespeed = 8f;
    private Vector3 _Movement;
    private Quaternion m_Rotation = Quaternion.identity;
    private Rigidbody _playerRb;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    private Vector3 _startingPosition;

    void Start ()
    {
        _playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
    }
    
    void Update ()
    {
         if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
        if(transform.position.y < outOfBounds)
        {
            transform.position = _startingPosition;
        }
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        _Movement.Set(horizontal, 0f, vertical);
        _Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //m_Animator.SetBool ("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, _Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

         _playerRb.MovePosition (_playerRb.position + _Movement * movespeed * Time.deltaTime);
         _playerRb.MoveRotation (m_Rotation);

        
         _playerRb.AddForce(_Movement * movespeed);

         _playerRb.AddForce(_Movement);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == checkointAreaObject)
        {
            isAtCheckpoint = true;
            //Debug.Log(_startingPosition)
            _startingPosition = checkointAreaObject.transform.position;
            //Debug.Log(_startingPosition)
        }
    }
}
    