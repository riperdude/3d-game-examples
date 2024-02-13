using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratkingmovement : MonoBehaviour
{
    public int score = 0;
    public float turnSpeed = 20;
    public float moveSpeed = 1f;
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public float outOfBounds = -40f;
    public bool IsOnGround = true;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    private Vector3 _startingPostion;
    private Vector3 _checkpointPosition;
    public bool isAtCheckpoint = false;
    public GameObject checkpointAreaObject;
     private GameObject[] _collectibles;
    public GameObject finishAreaObject;
    private Vector3 _movement;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = _defaultGravity;
        Physics.gravity *= GravityModifier;
        _startingPostion = transform.position;
        _collectibles = GameObject.FindGameObjectsWithTag("Collectible-Return");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }

        if(transform.position.y < outOfBounds)
        {
            if(isAtCheckpoint)
            {
                ReturningCollectibles();
                transform.position = _checkpointPosition;
            }
            else
            {
                ReturningCollectibles();
                transform.position = _startingPostion;
            }
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement.Set(horizontal, 0f, vertical);
        _movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation (desiredForward);

        _rigidbody.MovePosition (_rigidbody.position + _movement * moveSpeed * Time.deltaTime);
        _rigidbody.MoveRotation (_rotation);
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }

        if (collision.gameObject.CompareTag("Spinner"))
        {
            if(isAtCheckpoint)
            {
                ReturningCollectibles();
                transform.position = checkpointAreaObject.transform.position;
                
            }
            else
            {
                ReturningCollectibles();
                transform.position = _startingPostion;
            }
        }
        
    }
    void ReturningCollectibles()
    {
        for(int i = 0; i < _collectibles.Length; i++)
        {
            _collectibles[i].SetActive(true);
            _collectibles[i].GetComponent<Collectibles>().ReturnCollectibles();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == checkpointAreaObject)
        {
            isAtCheckpoint = true;
            _checkpointPosition = checkpointAreaObject.transform.position;
        }

        if(other.gameObject == finishAreaObject)
        {
            isAtCheckpoint = false;
            ReturningCollectibles();
            transform.position = _startingPostion;
        }

        if(other.gameObject.CompareTag("Collectible-Destroy"))
        {
            score++;
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Collectible-Return"))
        {
            score++;
            other.gameObject.GetComponent<Collectibles>().HideCollectibles();
        }
    }

    
}