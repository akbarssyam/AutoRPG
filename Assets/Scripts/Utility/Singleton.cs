using UnityEngine;
using System.Collections;

public class Singleton<Type>:MonoBehaviour where Type:MonoBehaviour 
{
    private static Type _instance;
 
	private static object _lock = new object();
 
	public static Type Instance
	{
		get
		{
			lock(_lock)
			{
				if (_instance == null)
				{
                    _instance = (Type)FindObjectOfType(typeof(Type));
				}
 
				return _instance;
			}
		}
	}
}
