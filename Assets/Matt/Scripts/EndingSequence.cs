using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSequence : MonoBehaviour
{
    public List<EmitterScript> towers;
    public SunPosition sun;
    public AuraLight[] lights;

    public bool started = false;

    bool finished;

    // Update is called once per frame
    void Update()
    {
        if (!started || finished) return;

        if (lights[0].strength >= 300f)
        {
            return;
        }

        foreach (var light in lights)
        {
            light.strength += Mathf.Min(light.strength * 0.0005f, 0.1f);
        }
    }

    public void TryEnd()
    {
        //started = true;
        //sun.RampAxialTilt(90f);
        //CameraFade.StartAlphaFade(Color.white, false, 40f);

        foreach (var tower in towers)
        {
            if (!tower.isOn) return;
        }

        started = true;
        sun.RampAxialTilt(90f);
        CameraFade.StartAlphaFade(Color.white, false, 40f);
    }
}
