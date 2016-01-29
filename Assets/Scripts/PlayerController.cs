using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public CharacterController charController { get {return _charController ?? (_charController = GetComponent<CharacterController>()); } }
	private CharacterController _charController;

	public float speed = 10;
	public float pushPower = 2.0f;

	public float health = 100;
	
	// Update is called once per frame
	void Update ()
	{
		// Looking
		var RYAxis_1 = Input.GetAxis("R_YAxis_1");
		var RXAxis_1 = Input.GetAxis("R_XAxis_1");

		// we use the deadzone of 0.01f to stop it snapping when no axis is in. Keeps old axis.
		if (Math.Abs(RYAxis_1) > 0.01f && Math.Abs(RXAxis_1) > 0.01f)
		{
			var angle = Mathf.Atan2(RXAxis_1, -RYAxis_1)*Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
		}

		// Movement
		var LYAxis_1 = Input.GetAxis("L_YAxis_1");
		var LXAxis_1 = Input.GetAxis("L_XAxis_1");

		var direction = Vector3.zero;
		direction += Vector3.forward * speed * -LYAxis_1;
		direction += Vector3.right * speed * LXAxis_1;

		// Use simplemove on the charController
		charController.SimpleMove(direction);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;

		if (hit.moveDirection.y < -0.3F)
			return;

		var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower;
	}
}
