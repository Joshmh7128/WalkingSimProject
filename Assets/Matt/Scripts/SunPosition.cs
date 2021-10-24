using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPosition : ActivateableObject
{
    public bool tickTime = false;

    public float startingPhase = 0.75f;

    // Time, no specific unit, we'll control as needed
    // 0 is effectively noon; sunrise/sunset depend on other variables
    // Sunset is around 1/4 cycleLength, sunrise is around 3/4 cycleLength, deviating based on latitude and tilt
    public float time;

    // Time for a full movement cycle
    public float cycleLength = 24;

    // If the sum of latitude and tilt is > 90 or < -90 it will always be day or night respectively (with variable amounts of light)

    // Latitude of 0, 0. 0 will result in an equatorial path, 90 and -90 polar
    public float localLatitude = 20;

    // Tilt of the simulated axis of rotation of the planet
    public float axialTilt = 10;

    // Angle clockwise from the positive x-axis from which the sun rises
    // Actual sunrise position will deviate with axialTilt (eg at 10 degrees axial tilt, the sun will rise 10 degrees clockwise from east)
    public float east = 90;

    private float scale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        time = startingPhase * cycleLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (tickTime)
        {
            time += Time.deltaTime * scale;
        }
            
        if (time >= cycleLength)
        {
            time -= cycleLength;
        }
        else if (time < 0)
        {
            time += cycleLength;
        }

        // Math from https://www.sciencedirect.com/topics/engineering/solar-declination

        float hourAngle = Mathf.Abs((time / cycleLength) - (cycleLength * 0.5f)) * Mathf.PI * 2;

        float latRad = localLatitude * Mathf.Deg2Rad;
        float tiltRad = axialTilt * Mathf.Deg2Rad;

        float altitude = Mathf.Asin(Mathf.Cos(latRad) * Mathf.Cos(tiltRad) * Mathf.Cos(hourAngle) + Mathf.Sin(latRad) * Mathf.Sin(tiltRad));

        //Debug.Log(altitude);

        float azimuth = Mathf.Acos((Mathf.Sin(altitude) * Mathf.Sin(latRad) - Mathf.Sin(tiltRad)) / (Mathf.Cos(altitude) * Mathf.Cos(latRad)));

        //Debug.Log(azimuth);

        // Above equation doesn't behave at min and max latitude but easy model those cases
        if (localLatitude == 90)
        {
            azimuth = -hourAngle;
        }
        else if (localLatitude == -90)
        {
            azimuth = hourAngle;
        }
        // Should only happen when the sun is directly above or below, safe to set to 0
        else if (float.IsInfinity(azimuth) || float.IsNaN(azimuth))
        {
            azimuth = 0;
        }
        else if (time > (cycleLength * 0.5f))
        {
            azimuth = 2 * Mathf.PI - azimuth;
        }

        transform.rotation = Quaternion.Euler(altitude * Mathf.Rad2Deg, azimuth * Mathf.Rad2Deg + east, 0f);
    }

    public override void ActivateObject()
    {
        tickTime = true;
    }

    public void SetTimeScale(float s)
    {
        scale = s;
    }

    public void RampAxialTilt(float add)
    {
        StartCoroutine(AxialTileRamp(axialTilt + add));
    }

    IEnumerator AxialTileRamp(float dest)
    {
        while (axialTilt < dest)
        {
            axialTilt += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        axialTilt = dest;
    }
}
