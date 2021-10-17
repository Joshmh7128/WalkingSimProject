using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffScript : MonoBehaviour
{
    // this script goes on all staffs and tells the player what they are holding and if they can hold it
    PlayerScript playerScript;
    public PlayerScript.staffStates staffState; // what is our current staff state of the player's possible staff states?
    public bool isHeld; // are we being held by the player?

    PuzzleManager puzzleManager;

    // Start is called before the first frame update
    void Start()
    {
        if (puzzleManager == null)
        {
            puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        }

        // set our playerscript from the puzzlemanager
        playerScript = puzzleManager.playerScript;

        // we are not being held
        isHeld = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // check to see if the player steps on us
        if (other.CompareTag("Player"))
        {
            // if the player is holding nothing then we can be picked up
            if (playerScript.StaffState == PlayerScript.staffStates.None)
            {
                // assurance incase of human error
                if (staffState == PlayerScript.staffStates.None && isHeld == false)
                {
                    Debug.LogError("Staff was attempted to be picked up without first setting it's color!");
                }

                // if the staff has it's light set and this staff is not being held
                if (staffState != PlayerScript.staffStates.None && isHeld == false)
                {

                    isHeld = true;

                    // if we are not None then set the player's staff state to our staff state
                    playerScript.StaffState = staffState;
                    playerScript.ourStaff = this;

                    // place our staff on our player so that our light tracks it
                    transform.parent = playerScript.gameObject.transform;
                    transform.position = playerScript.gameObject.transform.position;
                }
            }
        }
    }

    // called when we want to be put down
    public void PlaceStaff(Transform targetTransform)
    {
        isHeld = false;
        transform.parent = null;
        playerScript.StaffState = PlayerScript.staffStates.None;
        transform.position = targetTransform.position;
    }
}
