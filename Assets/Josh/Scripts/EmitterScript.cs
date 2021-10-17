using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    // this class handles our light emitter puzzle elements
    [SerializeField] PlayerScript.staffStates emitterState; // what color are we?
    [SerializeField] LineRenderer lineRenderer; // our line renderer
    public Transform lightOrigin; // the target of our light
    public Transform lightTarget; // the target of our light

    // runs once on start of game
    private void Start()
    {
        // update our target
        lineRenderer.SetPosition(0, lightOrigin.position);
    }

    // runs once per physics update
    private void FixedUpdate()
    {
        if (emitterState != PlayerScript.staffStates.None)
        {
            // update the positions of our target
            lineRenderer.SetPosition(1, lightTarget.position);
        }
    }
}
