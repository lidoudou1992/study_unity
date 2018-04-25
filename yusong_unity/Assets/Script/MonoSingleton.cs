using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	protected static T s_Me;
	
	//Alternate Awake and Enable methods provided.
	//The normal methods cannot be used, because this superclass is using
	//them to listen to Unity event, and does not want to rely on subclasses calling base.Awake
	virtual protected void _Awake(){}
	virtual protected void _OnEnable(){}
	virtual protected void _OnDestroy(){}
	
	public static T Me
	{
		get
		{
			return s_Me;
		}
	}
	
	private bool SetupInstance()
	{
		if (s_Me == this)
			return true;
		if (s_Me == null)
		{
			s_Me = this as T;
			return true;
		} 
		else 
		{
			//Log.Warning(LogChannel.Framework, gameObject, "DOUBLE SINGLETON. KILLING SECOND INSTANCE ({0})", GetType());
			enabled = false;
			return false;
		}
	}
	
	//Do not hide/override. Use _Awake instead
	protected virtual void Awake()
	{
		if (SetupInstance())
			_Awake();
	}
	
	//Do not hide/override. Use _OnEnable instead
	protected void OnEnable()
	{
		if (SetupInstance())
			_OnEnable();
	}
	
	//Do not hide/override. Use _OnDestroy instead
	protected void OnDestroy()
	{
		if (s_Me == this) {
			_OnDestroy();
			Destroy(s_Me);
		
			s_Me = null;
		}
	}
}