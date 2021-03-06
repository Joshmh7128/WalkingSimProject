// by @torahhorse

// Instructions:
// Place on player. OnBelowLevel will get called if the player ever falls below

using UnityEngine;
using System.Collections;

public class CheckIfBelowLevel : MonoBehaviour
{
	public float resetBelowThisY = -100f;
	public bool fadeInOnReset = true;
	
	private CharacterController controller;

    private void Start()
    {
		controller = GetComponent<CharacterController>();
	}

    void Update ()
	{
		if( transform.position.y < resetBelowThisY )
		{
			OnBelowLevel();
		}
	}
	
	private void OnBelowLevel()
	{
		Debug.Log("Player fell below level");

		controller.enabled = false;
		// reset the player
		transform.position = ResetPositionTrigger.currentResetPosition;
		controller.enabled = true;
		
		if( fadeInOnReset )
		{
			// see if we already have a "camera fade on start"
			CameraFadeOnStart fade = GameObject.Find("Main Camera").GetComponent<CameraFadeOnStart>();
			if( fade != null )
			{
				fade.Fade();
			}
			else
			{
				Debug.LogWarning("CheckIfBelowLevel couldn't find a CameraFadeOnStart on the main camera");
			}
		}
		
		// alternatively, you could just reload the current scene using this line:
		//Application.LoadLevel(Application.loadedLevel);
	}
}
