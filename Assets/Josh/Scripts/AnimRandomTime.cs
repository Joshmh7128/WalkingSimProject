using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRandomTime : MonoBehaviour
{
    [SerializeField] float randomMin;
    [SerializeField] float randomMax;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().speed = Random.Range(randomMin, randomMax);
    }

}
