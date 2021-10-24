using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionTrigger : MonoBehaviour
{
    public static Vector3 currentResetPosition;

    public Transform resetTransform;

    private void OnTriggerEnter(Collider other)
    {
        currentResetPosition = resetTransform.position;
    }
}
