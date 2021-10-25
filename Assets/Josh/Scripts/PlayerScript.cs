using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // custom player script

    public StaffScript ourStaff;

    public StaffScript centerStaff;
    public GameObject centerEnclosure;

    public enum staffStates
    {
        // put all staff colors here
        None, // if it is none, we can pick up a new color
        White, Blue, Red, Yellow, Green
    }

    // setup our list of on-screen staff objects
    public List<GameObject> cosmeticStaffObjects; // correspond directly with states. 0 = none, 1 = white, etc

    private staffStates staffState;

    // what staff state are we in?
    public staffStates StaffState
    {
        get { return staffState; }

        set
        {
            staffState = value;
            // disable all our staffObjects
            //foreach (GameObject staffObject in cosmeticStaffObjects)
            //{ staffObject.SetActive(false); }
            //// enable the one we have now
            //cosmeticStaffObjects[(int)value].SetActive(true);
        }
    }

    public Transform collectionTransform;

    private void Update() 
    {
        centerEnclosure.SetActive(ourStaff == centerStaff);
    }

    // wrapper for staff placement
    public void PlaceStaff(Transform targetTransform, StaffHolderScript newHolder)
    {
        ourStaff.PlaceStaff(targetTransform, newHolder);
    }

}
