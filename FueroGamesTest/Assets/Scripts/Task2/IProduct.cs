using UnityEngine;
using System.Collections;

/// <summary>
/// Every class which wants to use Factory's methods need to implement it
/// </summary>
public interface IProduct 
{
	/// <summary>
	/// Function that will be responsible for connecting with our Factory's createProduct method
	/// </summary>
	void GenerateNewObject();
}
