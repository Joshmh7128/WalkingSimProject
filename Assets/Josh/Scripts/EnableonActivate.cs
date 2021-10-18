using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableonActivate : ActivateableObject
{
    GameObject targetObject;

    public override void ActivateObject()
    {
        gameObject.SetActive(targetObject);
    }
}