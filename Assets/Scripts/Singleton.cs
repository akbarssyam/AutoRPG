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
			if (applicationIsQuitting) {
                Debug.LogWarning("[Singleton] Instance '" + typeof(Type) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
				return null;
			}
 
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
 
	private static bool applicationIsQuitting = false;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	public void OnDestroy () {
		applicationIsQuitting = true;
	}
}
