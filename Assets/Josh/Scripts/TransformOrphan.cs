using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOrphan : MonoBehaviour
{
    [SerializeField] bool zeroOut;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;   
        if (zeroOut)
        {
            transform.position = Vector3.zero;
        }
    }
}
