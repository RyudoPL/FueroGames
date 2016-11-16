using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

/// <summary>
/// Our factory class responsible for creating classes requested by the client.
/// </summary>
public class Factory : MonoBehaviour
{
	#region Fields
	/// <summary>
	/// Public variables which we can use to set min and max values and length for numeric types and strings in generated class
	/// </summary>
	public int minValue;
	public int maxValue;
	public int minLength;
	public int maxLength;
	#endregion

	#region Properties
	private static Factory instance;

	public static Factory Instance
	{
		get{return instance;}
	}
	#endregion 

	#region Methods
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	/// <summary>
	/// Register the product in our factory and calls createProduct method
	/// </summary>
	/// <returns>New object of requested class to our client.</returns>
	/// <param name="productClass">Class which we want to create new instance of.</param>
	/// <param name="nestedClasses">Array of nested objects in our generated class.</param>
	/// <typeparam name="T">Generic parameter which stores the class we have requested new instance of.</typeparam>
	public T registerProduct<T> (T productClass,params object[] nestedClasses) where T: UnityEngine.Component
	{
		T newObject = createProduct<T>(productClass,nestedClasses);
		return newObject;
	}

	/// <summary>
	/// Creates new instance of our requested class.
	/// </summary>
	/// <returns>New instance of requested class.</returns>
	/// <param name="script">Class which we want to create new instance of.</param>
	/// <param name="nestedClasses">Array of nested objects in our generated class.</param>
	/// <typeparam name="T">Generic parameter which stores the class we have requested new instance of.</typeparam>
	public T createProduct<T>(T script,params object[] nestedClasses) where T: UnityEngine.Component
	{
		//Set flags to filter field and properties we will be looking for
		const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;

		//Get all the fields in our requested class
		FieldInfo[] fields = script.GetType().GetFields();

		foreach(FieldInfo field in fields)
		{
			SetField(field,field.Name,script);
		}

		//Get all properties in our requested class
		PropertyInfo[] properties = script.GetType().GetProperties(flags);

		foreach(PropertyInfo property in properties)
		{
			SetProperty(property,property.Name,script);
		}

		//Get all nested types in our requested class
		System.Type[] nestedTypes = script.GetType().GetNestedTypes();

		//Iterate through all nested types and get fields and properties from each of them
		for(int i = 0; i < nestedTypes.Length; i++)
		{
			FieldInfo[] nestedFields = nestedTypes[i].GetFields();

			foreach(FieldInfo nestedField in nestedFields)
			{
				SetField(nestedField,nestedField.Name,nestedClasses[i]);
			}

			PropertyInfo[] nestedProperties = nestedTypes[i].GetProperties(flags);

			foreach(PropertyInfo nestedProperty in nestedProperties)
			{
				SetProperty(nestedProperty,nestedProperty.Name,nestedClasses[i]);
			}
		}

		return script;
	}
		
	/// <summary>
	/// Set values for all fields in our requested class.
	/// </summary>
	/// <param name="field">Field to which we want to set a value.</param>
	/// <param name="fieldName">Field's name.</param>
	/// <param name="script">Class which we want to create new instance of.</param>
	/// <typeparam name="T">Generic parameter which stores the class we have requested new instance of.</typeparam>
	public static void SetField<T>(FieldInfo field, string fieldName, T script)
	{
		if(field.FieldType == typeof(string))
		{
			int charAmount = Random.Range(Factory.Instance.minLength, Factory.Instance.maxLength);
			string obj = string.Empty;
			for(int i = 0; i < charAmount; i++)
			{
				obj += '*';
			}	
			field.SetValue(script,obj);
		}
		else if(field.FieldType == typeof(int) || field.FieldType == typeof(float))
		{
			field.SetValue(script, Random.Range(Factory.instance.minValue,Factory.instance.maxValue));
		}
	}

	/// <summary>
	/// Set values for all properties in our requested class.
	/// </summary>
	/// <param name="field">Property to which we want to set a value.</param>
	/// <param name="fieldName">Property's name.</param>
	/// <param name="script">Class which we want to create new instance of.</param>
	/// <typeparam name="T">Generic parameter which stores the class we have requested new instance of.</typeparam>
	public static void SetProperty<T>(PropertyInfo property, string propertyName, T script)
	{
		if(property.PropertyType == typeof(string))
		{
			int charAmount = Random.Range(Factory.Instance.minLength, Factory.Instance.maxLength);
			string obj = string.Empty;
			for(int i = 0; i < charAmount; i++)
			{
				obj += '*';
			}	
			property.SetValue(script,obj,null);
		}
		else if(property.PropertyType == typeof(int) || property.PropertyType == typeof(float))
		{
			property.SetValue(script, Random.Range(Factory.instance.minValue,Factory.instance.maxValue),null);
		}
	}
	#endregion	
}
