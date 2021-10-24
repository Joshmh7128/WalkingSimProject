using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    FMODUnity.StudioEventEmitter music;

    public PlayerScript player;

    public EndingSequence ending;

    float lead1Target;
    float lead2Target;
    float bassTarget;
    float padTarget;

    float lead1;
    float lead2;
    float bass;
    float pad;

    bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped) return;

        lead1Target = (transform.position.y - 50f) / 100f;
        lead1Target = Mathf.Clamp(lead1Target, 0f, 1f);

        bassTarget = (130f - transform.position.y) / 30f;
        bassTarget = Mathf.Clamp(bassTarget, 0f, 1f);

        lead2Target = player.StaffState == PlayerScript.staffStates.None ? 0f : 1f;
        Vector3 fromCenter = new Vector3(0, 105f, 0) - transform.position;
        lead2Target += Mathf.Clamp((60f - fromCenter.magnitude) / 60f, 0f, 1f);

        padTarget = 1f;

        if (ending.started)
        {
            music.Stop();
            stopped = true;
        }
        else
        {
            lead1 += (lead1Target - lead1) * 0.01f;
            lead2 += (lead2Target - lead2) * 0.001f;
            bass += (bassTarget - bass) * 0.01f;
            pad = padTarget;
        }
        

        music.SetParameter("Bass", bass);
        music.SetParameter("Lead1", lead1);
        music.SetParameter("Lead2", lead2);
        music.SetParameter("Pad", pad);
    }
}
