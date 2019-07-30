using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/String")]
public class StringScriptable : ScriptableObject
{

	public string Value {
		get { return _value; }
		set { _value = value;
			Raise();
		}
	}
	[SerializeField] private string _value;
	[SerializeField] private List<StringListener> OnValueChangeListeners;

	public void AddListener(StringListener floatListener)
	{
		if (OnValueChangeListeners == null)
		{
			OnValueChangeListeners = new List<StringListener>();
		}

		if (floatListener == null ||OnValueChangeListeners.Contains(floatListener))
		{
			return;
		}
		OnValueChangeListeners.Add(floatListener);
	}

	public void RemoveListener(StringListener floatListener)
	{
		if (OnValueChangeListeners == null)
		{
			OnValueChangeListeners = new List<StringListener>();
		}

		if (floatListener == null || !OnValueChangeListeners.Contains(floatListener))
		{
			return;
		}
		OnValueChangeListeners.Remove(floatListener);
	}

	private void Raise()
	{
		if (OnValueChangeListeners == null)
			return;

		for (var k = OnValueChangeListeners.Count - 1; k > -1; k--)
		{
			
			OnValueChangeListeners[k].Raise(_value);
			
		}
	}
}
