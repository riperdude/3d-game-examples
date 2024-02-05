using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public float turnSpeed = 7f;
    public float movespeed = 8f;
    private Rigidbody m_Rigidbody;
    private Vector3 _Movement;
    private Quaternion m_Rotation = Quaternion.identity;
    private Rigidbody _playerRb;

    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
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

         m_Rigidbody.MovePosition (m_Rigidbody.position + _Movement * movespeed * Time.deltaTime);
         m_Rigidbody.MoveRotation (m_Rotation);

         if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
         _playerRb.AddForce(_Movement * movespeed);

         _playerRb.AddForce(_Movement);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IsOnGround = true;
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }
}
    