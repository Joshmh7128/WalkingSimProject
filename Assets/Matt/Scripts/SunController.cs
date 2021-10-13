using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public SunPosition sunPosition;
    public Transform axis;
    public Transform latitude;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        axis.localEulerAngles = new Vector3(axis.localEulerAngles.x, axis.localEulerAngles.y, sunPosition.axialTilt);

        float timeDegrees = ((sunPosition.time - sunPosition.cycleLength) / sunPosition.cycleLength) * 360f;

        latitude.localEulerAngles = new Vector3(timeDegrees, 180, sunPosition.localLatitude);
    }
}
