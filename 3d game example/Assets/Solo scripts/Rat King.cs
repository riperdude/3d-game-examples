using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatKing : MonoBehaviour
{
    public float RotaionSpeed = 45f;
    public float RotaionSpeed2 = 35f;
    public float RotaionSpeed3 = 25f;
     

    private float _horizontalInput;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(25, 45, 35) * RotaionSpeed * RotaionSpeed2 * RotaionSpeed3 * Time.deltaTime);
    }
}
