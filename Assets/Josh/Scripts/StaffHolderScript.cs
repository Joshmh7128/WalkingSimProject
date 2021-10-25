using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaffHolderScript : MonoBehaviour
{
    PuzzleManager puzzleManager;
    [SerializeField] List<ActivateableObject> activateableObjects;

    public bool spotOccupied; // set in inspector, are we occupied right now?

    public GameObject staffToLock;

    public List<UnityEvent> onPlaceActions = new List<UnityEvent>();

    public GameObject holderBase;

    private void Start()
    {
        if (puzzleManager == null)
        {
            puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        }

        holderBase = transform.Find("Base").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // check to see if the player steps on us
        if (other.CompareTag("Player") && spotOccupied == false)
        {
            // if they are and they are not holding nothing then place the staff
            if (puzzleManager.playerScript.StaffState != PlayerScript.staffStates.None)
            {
                puzzleManager.playerScript.PlaceStaff(transform, this);
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Put Down", holderBase);
            }
        }
    }

    public void ActivateObjects(PlayerScript.staffStates staffState)
    {
        // Could add required staff states to staff holder if we only want certain colors to work
        foreach (var obj in activateableObjects)
        {
            obj.ActivateObject();
        }
    }
}
