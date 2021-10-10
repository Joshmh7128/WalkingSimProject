using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPosition : MonoBehaviour
{
    // Time, no specific unit, we'll control as needed
    public float time;

    // Time for a full movement cycle
    public float cycleLength = 24;

    // Latitude of 0, 0. 0 will result in an equatorial path, 90 and -90 polar
    public float localLatitude = 20;

    // Tilt of the simulated axis of rotation of the planet
    public float axialTilt = 10;

    // Angle clockwise from the positive x-axis from which the sun rises
    public float east = 90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > cycleLength)
        {
            time -= cycleLength;
        }

        // Math from https://www.sciencedirect.com/topics/engineering/solar-declination

        float hourAngle = Mathf.Abs((time / cycleLength) - (cycleLength * 0.5f)) * Mathf.PI * 2;

        float latRad = localLatitude * Mathf.Deg2Rad;
        float tiltRad = axialTilt * Mathf.Deg2Rad;

        float x = Mathf.Asin(Mathf.Cos(latRad) * Mathf.Cos(tiltRad) * Mathf.Cos(hourAngle) + Mathf.Sin(latRad) * Mathf.Sin(tiltRad));

        float y = Mathf.Acos((Mathf.Sin(x) * Mathf.Sin(latRad) - Mathf.Sin(tiltRad)) / (Mathf.Cos(x) * Mathf.Cos(latRad)));

        if (time > (cycleLength * 0.5f))
        {
            y = 2 * Mathf.PI - y;
        }

        transform.rotation = Quaternion.Euler(x * Mathf.Rad2Deg, y * Mathf.Rad2Deg, 0f);
    }
}
