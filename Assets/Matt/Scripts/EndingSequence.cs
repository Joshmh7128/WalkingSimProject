using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequence : MonoBehaviour
{
    public List<EmitterScript> towers;

    public AuraLight[] lights;

    public bool started = false;

    // Update is called once per frame
    void Update()
    {
        if (!started) return;

        foreach (var light in lights)
        {
            light.strength += 1 * Time.deltaTime;
        }
    }

    public void TryEnd()
    {
        foreach (var tower in towers)
        {
            if (!tower.isOn) return;
        }

        started = true;
    }
}
