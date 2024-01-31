using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollABall : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput;
        float verticalInput;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        _playerRigidbody.AddForce(movement);
    }
    
}
