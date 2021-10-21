using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnActivate : ActivateableObject
{
    [SerializeField] Vector3 startOffset;

    [SerializeField] bool moveRelative;
    [SerializeField] Vector3 movement;
    [SerializeField] Transform moveTo;

    [SerializeField] float moveOverTime;

    Vector3 originalPosition;
    Vector3 destination;
    float timer;

    private void Start()
    {
        originalPosition = transform.position + startOffset;
        transform.position = originalPosition;
        //ActivateObject();
    }

    public override void ActivateObject()
    {
        if (moveRelative)
        {
            destination = transform.position + movement;
        }
        else
        {
            if (moveTo != null)
            {
                destination = moveTo.position + movement;
            }
            else
            {
                destination = movement;
            }
        }
        timer = 0;

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (timer < moveOverTime)
        {
            transform.position = Vector3.Lerp(originalPosition, destination, timer / moveOverTime);
            yield return new WaitForFixedUpdate();
            timer += Time.fixedDeltaTime;
        }
        transform.position = destination;
    }
}
