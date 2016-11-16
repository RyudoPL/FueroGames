using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that will be instantiating our prefab and dealing with it during our pause state
/// </summary>
public class GameObjectGenerator : MonoBehaviour 
{
	#region Fields
	public GameObject prefab;

	public float timeBetweenSpawningCubes = 2f;

	//boolean that will hold info whether our game is in pause mode
	bool isPaused = false;

	//float that will hold our current time
	float timeNow;

	//float that will store our last interval
	float lastInterval;

	//List that will hold all GameObjects instantiated when our game was in pause mode
	List<GameObject> prefabsInstantiatedDuringPause;
	#endregion

	#region Methods
	void Start () 
	{
		lastInterval = Time.realtimeSinceStartup;
		prefabsInstantiatedDuringPause = new List<GameObject>();
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

				if(prefabsInstantiatedDuringPause.Count != 0)
				{
					foreach(GameObject prefab in prefabsInstantiatedDuringPause)
					{
						Spawn(prefab);
					}
					//Clear the list so we don't spawn the same GameObjects when we resume the game next time
					prefabsInstantiatedDuringPause.Clear();
				}
			}
		}
		timeNow = Time.realtimeSinceStartup;

		if (timeNow >= timeBetweenSpawningCubes + lastInterval && isPaused == false) 
		{
			Spawn(prefab);
			lastInterval = Time.realtimeSinceStartup;
		}

		if(timeNow >= timeBetweenSpawningCubes + lastInterval && isPaused == true)
		{
			GameObject temp = PrepareToSpawn();
			//Add GameObject instantiated during our pause to the list
			prefabsInstantiatedDuringPause.Add(temp);
			lastInterval = Time.realtimeSinceStartup;
		}
	}

	/// <summary>
	/// Instantiates our prefab.
	/// </summary>
	/// <param name="prefab">Prefab that needs to be instantiated</param>
	private void Spawn(GameObject prefab)
	{
		// Instantiate the prefab
		prefab = ( GameObject ) Instantiate( prefab, new Vector3( this.transform.position.x, this.transform.position.y, this.transform.position.z ),
			Quaternion.identity);
	}

	/// <summary>
	/// Function called during the pause to get GameObjects which should have been instantiated during that time
	/// </summary>
	/// <returns>Prefab to instantiate after the game is resumed</returns>
	private GameObject PrepareToSpawn()
	{
		return prefab;
	}
	#endregion
}
