using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour

public transfrom objectToFollow;

private Vector3 _offset;

{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = objectToFollow.position + _offset;
    }
}
