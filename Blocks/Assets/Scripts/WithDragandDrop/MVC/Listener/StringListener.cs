using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StringListener : MonoBehaviour
{

	public StringEvent OnValueChange;
	public StringScriptable _value;
	public bool SetToThisText = false;

	public void OnEnable()
	{
		if (_value != null)
		{
			_value.AddListener(this);
			Raise(_value.Value);
		}
	}

	private void OnDisable()
	{
		if (_value != null)
		{
			_value.RemoveListener(this);
		}
	}


	public void Raise(string value)
	{
		if(OnValueChange!=null)
			OnValueChange.Invoke(value);

		if (SetToThisText)
		{
			var text = GetComponent<Text>();
			if (text != null)
				text.text = value;
		}
	}
}
[System.Serializable]
public class StringEvent:UnityEvent<string>{}