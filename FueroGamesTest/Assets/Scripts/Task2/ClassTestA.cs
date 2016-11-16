using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Our test class to see whether our Factory class is working correctly
/// </summary>
public class ClassTestA : MonoBehaviour, IProduct
{
	#region Fields
	public float randomFloat;
	public int randomInt;
	public string randomString;

	//reference to NestedClass class
	public NestedClass nestedClass;
	#endregion

	#region Properties
	//For testing purposes (to be visible in the inspector)
	[SerializeField]
	private string randomStringProperty;

	public string RandomStringProperty
	{
		get{ return randomStringProperty;}
		set{ randomStringProperty = value;}
	}
	#endregion


	#region Methods
	/// <summary>
	/// Method implemented from our interface, which generates class which we have requested.
	/// </summary>
	public void GenerateNewObject()
	{
		ClassTestA obj =  Factory.Instance.registerProduct<ClassTestA>(this,nestedClass);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.G))
		{
			GenerateNewObject();
		}
	}
	#endregion

	#region Nested Classes
	//For testing purposes (to be visible in the inspector)
	[Serializable]
	public class NestedClass
	{
		public float randomNestedFloat;
		public int randomNestedInt;

		[SerializeField]
		private string randomNestedStringProperty;

		public string RandomNestedStringProperty
		{
			get{ return randomNestedStringProperty;}
			set{ randomNestedStringProperty = value;}
		}
	}
	#endregion
}


