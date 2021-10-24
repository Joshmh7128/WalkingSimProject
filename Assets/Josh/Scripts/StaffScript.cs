using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffScript : MonoBehaviour
{
    // this script goes on all staffs and tells the player what they are holding and if they can hold it
    public PlayerScript playerScript;
    public PlayerScript.staffStates staffState; // what is our current staff state of the player's possible staff states?
    public bool isHeld; // are we being held by the player?
    public bool canGrab; // can we be picked up?
    PuzzleManager puzzleManager;
    public StaffHolderScript currentHolder;
    [SerializeField] EmitterScript ourEmitter;
    // Start is called before the first frame update

    [SerializeField] IconDisplay icon;

    bool locked;

    void Start()
    {
        if (puzzleManager == null)
        {
            puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        }

        // set our playerscript from the puzzlemanager
        playerScript = puzzleManager.playerScript;

        // set our staff state
        if (ourEmitter != null)
        {
            staffState = ourEmitter.emitterState;
        }
        // gameObject.GetComponent<Renderer>().material = puzzleManager.emissionMaterials[(int)staffState];

        // we are not being held and can be picked up
        isHeld = false;
        canGrab = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // check to see if the player steps on us
        if (other.CompareTag("Player"))
        {
            canGrab = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // check to see if the player steps on us
        if (other.CompareTag("Player"))
        {
            canGrab = false;
        }
    }

    private void Update()
    {
        icon.SetVisible(canGrab && !isHeld && !locked);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canGrab && !locked)
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
                        currentHolder.spotOccupied = false;
                        currentHolder = null;
                        isHeld = true;

                        // if we are not None then set the player's staff state to our staff state
                        playerScript.StaffState = staffState;
                        playerScript.ourStaff = this;

                        // place our staff on our player so that our light tracks it
                        transform.parent = playerScript.collectionTransform;
                        //transform.position = playerScript.gameObject.transform.position;
                        transform.position = playerScript.cosmeticStaffObjects[0].transform.position;
                        transform.rotation = playerScript.cosmeticStaffObjects[0].transform.rotation;
                    }
                }
            }
        }
    }

    // called when we want to be put down
    public void PlaceStaff(Transform targetTransform, StaffHolderScript newHolder)
    {
        isHeld = false;
        transform.parent = null;
        playerScript.StaffState = PlayerScript.staffStates.None;
        currentHolder = newHolder;
        newHolder.spotOccupied = true;
        currentHolder.ActivateObjects(staffState);
        if (newHolder.oneTimeUse)
        {
            transform.position = targetTransform.position - Vector3.up * 0.5f;
            locked = true;
        }
        else
        {
            transform.position = targetTransform.position;
        }
        transform.rotation = targetTransform.rotation;
    }
}
