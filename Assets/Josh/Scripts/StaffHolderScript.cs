using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffHolderScript : MonoBehaviour
{
    PuzzleManager puzzleManager;

    public bool spotOccupied; // set in inspector, are we occupied right now?

    private void Start()
    {
        if (puzzleManager == null)
        {
            puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        }
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
            }
        }
    }


}