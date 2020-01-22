using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParentPosition : MonoBehaviour
{
    public Transform target;

    void LateUpdate(){

        Vector3 newPosition = target.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

    }
}
