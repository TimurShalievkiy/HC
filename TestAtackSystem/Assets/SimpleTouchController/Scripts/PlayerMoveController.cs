using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMoveController : MonoBehaviour {

	// PUBLIC
	public SimpleTouchController leftController;
	public SimpleTouchController rightController;
	public Transform headTrans;
	public float speedMovements = 5f;
	public float speedContinuousLook = 100f;
	public float speedProgressiveLook = 3000f;

	// PRIVATE
	private Rigidbody _rigidbody;
	[SerializeField] bool continuousRightController = true;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		rightController.TouchEvent += RightController_TouchEvent;
	}

	public bool ContinuousRightController
	{
		set{continuousRightController = value;}
	}

	void RightController_TouchEvent (Vector2 value)
	{
		if(!continuousRightController)
		{
			UpdateAim(value);
		}
	}

	void Update()
	{
		// move
		_rigidbody.MovePosition(transform.position + (transform.forward * leftController.GetTouchPosition.y * Time.deltaTime * speedMovements) +
			(transform.right * leftController.GetTouchPosition.x * Time.deltaTime * speedMovements) );

		if(continuousRightController)
		{
			UpdateAim(rightController.GetTouchPosition);
		}
	}

	void UpdateAim(Vector2 value)
	{

        Quaternion target ;
        if (value.y > 0)
        {
            target = Quaternion.Euler(0, value.x * 90, 0);
           
            headTrans.transform.rotation = Quaternion.Slerp(headTrans.transform.rotation, target, /*Time.deltaTime * 2f*/2f);
        }           
        else
        {
            target = Quaternion.Euler(0, (value.x * -90)+180, 0);
            headTrans.transform.rotation = Quaternion.Slerp(headTrans.transform.rotation, target, /*Time.deltaTime * 2f*/2f);
        }
            
    }

	void OnDestroy()
	{
		rightController.TouchEvent -= RightController_TouchEvent;
	}

}
