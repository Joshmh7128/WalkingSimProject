using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    // this class handles our light emitter puzzle elements
    public PlayerScript.staffStates emitterState; // what color are we?
    [SerializeField] PuzzleManager puzzleManager; 
    [SerializeField] StaffScript staffScript; 
    [SerializeField] LineRenderer lineRenderer; // our line renderer
    [SerializeField] Light spotLight; // our line renderer
    public Transform lightOrigin; // the target of our light
    public Transform lightTarget; // the target of our light
    public bool isOn; // is this light on?

    // runs once on start of game
    private void Start()
    {
        if (puzzleManager == null)
        {
            puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        }

        // target's coloring
        lightTarget.gameObject.GetComponent<Renderer>().material = puzzleManager.emissionMaterials[(int)emitterState];
    }

    private void Update()
    {
        // update our target
        lineRenderer.SetPosition(0, lightOrigin.position);

        // are we on or off?
        if (isOn)
        {
            // update our renderers to show this
            lightOrigin.gameObject.GetComponent<Renderer>().material = puzzleManager.emissionMaterials[(int)emitterState];
            lineRenderer.material = puzzleManager.emissionMaterials[(int)emitterState];
            lineRenderer.enabled = true;
        }

        if (!isOn)
        {
            // update our renderers to show this
            lightOrigin.gameObject.GetComponent<Renderer>().material = puzzleManager.emissionMaterials[0];
            lineRenderer.enabled = false;
        }

        if (emitterState != PlayerScript.staffStates.None)
        {
            // set our second position
            lineRenderer.SetPosition(1, lightTarget.position);
        }
    }

    // runs once per physics update
    private void FixedUpdate()
    {
        if (Physics.Linecast(lightOrigin.position, lightTarget.position, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        { isOn = false; }

        if (!Physics.Linecast(lightOrigin.position, lightTarget.position, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        { isOn = true; }
    }
}
