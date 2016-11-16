using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that will be incrementing our float value and deal with it during the pause state.
/// </summary>
public class ResourceProducer : MonoBehaviour
{
	#region Fields

	public float timeBetweenIncrementing = 2f;

	//our main timer which our class will be incrementing
	float timer = 0f;

	//timer that will be incremeneting when the game is paused.
	float timerDuringPause = 0f;

	//boolean that will hold info whether our game is in pause mode
	bool isPaused = false;

	//float that will hold our current time
	float timeNow;

	//float that will store our last interval
	float lastInterval;
	#endregion

	#region Methods
	void Start () 
	{
		lastInterval = Time.realtimeSinceStartup;
	}
	
	void Update () 
	{ 
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(isPaused == false)
			{
				Time.timeScale = 0;
				isPaused = true;
			}
			else
			{
				Time.timeScale = 1;
				isPaused = false;
				timer += timerDuringPause;
				timerDuringPause = 0f;
			}
		}
		timeNow = Time.realtimeSinceStartup;

		if (timeNow >= timeBetweenIncrementing + lastInterval && isPaused == false) 
		{ 
			timer++;
			Debug.Log(timer.ToString());
			lastInterval = Time.realtimeSinceStartup;
		}

		if(timeNow >= timeBetweenIncrementing + lastInterval && isPaused == true)
		{
			timerDuringPause++;
			lastInterval = Time.realtimeSinceStartup;
		}
	}
	#endregion
}
