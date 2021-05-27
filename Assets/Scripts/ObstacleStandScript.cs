using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObstacleStandScript : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 1f, 0));
    }
}
