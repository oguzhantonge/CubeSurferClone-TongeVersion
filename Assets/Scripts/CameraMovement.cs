using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smoothSpeed;

    void Start()
    {
        target = GameObject.Find("MainCube").GetComponent<Player>().transform;
        ClearCameraPosition();
        EventManager.onClearLevel += ClearCameraPosition;
    }
    
    void ClearCameraPosition()
    {
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
    }

    void Update()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime);
        }
    }

}
