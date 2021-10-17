using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // custom player script

    public StaffScript ourStaff;

    public enum staffStates
    {
        // put all staff colors here
        None, // if it is none, we can pick up a new color
        White, Blue, Red, Yellow, Green
    }

    // setup our list of on-screen staff objects
    [SerializeField] List<GameObject> cosmeticStaffObjects; // correspond directly with states. 0 = none, 1 = white, etc

    private staffStates staffState;

    // what staff state are we in?
    public staffStates StaffState
    {
        get { return staffState; }

        set
        {
            staffState = value;
            // disable all our staffObjects
            foreach (GameObject staffObject in cosmeticStaffObjects)
            { staffObject.SetActive(false); }
            // enable the one we have now
            cosmeticStaffObjects[(int)value].SetActive(true);
        }
    }

    // wrapper for staff placement
    public void PlaceStaff(Transform targetTransform)
    {
        ourStaff.PlaceStaff(targetTransform);
    }

}
