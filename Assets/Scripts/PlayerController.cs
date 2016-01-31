using UnityEngine;

public class PlayerController : Entity
{
	public CharacterController charController { get {return _charController ?? (_charController = GetComponentInChildren<CharacterController>()); } }
	private CharacterController _charController;
	public Animator animator { get { return _animator ?? (_animator = GetComponentInChildren<Animator>()); } }
	private Animator _animator;
	public SpellController spellController { get { return _spellController ?? (_spellController = GetComponentInChildren<SpellController>()); } }
	private SpellController _spellController;

	public float speed = 10;
	public float pushPower = 2.0f;

	private Transform cameraForward;

	void Start()
	{
		cameraForward = GameObject.FindGameObjectWithTag("CameraForward").transform;
		OnHealthChanged = f => UIManager.Instance.inGameMenu.SetHealth(Mathf.CeilToInt(f));
		var controller = Camera.main.GetComponent<CameraController>();
		OnHealthChanged += f => controller.Shake(Vector2.one * 2, 0.1f);
	}


	void LateUpdate()
	{

		var A_1 = Input.GetAxisRaw("A_1");
		var sprint = 1.0f;
		if (Mathf.Abs(A_1) > 0.01f)
		{
			sprint *= 1.5f;
		}

		// Looking
		var RYAxis_1 = Input.GetAxis("R_YAxis_1");
		var RXAxis_1 = Input.GetAxis("R_XAxis_1");

		// we use the deadzone of 0.01f to stop it snapping when no axis is in. Keeps old axis.
		if (new Vector2(RXAxis_1,RYAxis_1).magnitude > 0.01f)
		{
			var angle = Mathf.Atan2(RXAxis_1, RYAxis_1) * Mathf.Rad2Deg + cameraForward.parent.eulerAngles.y;
			transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
			spellController.lineObject.SetActive(true);
        }

		// Movement
		var LYAxis_1 = Input.GetAxis("L_YAxis_1");
		var LXAxis_1 = Input.GetAxis("L_XAxis_1");
		
		Vector3 direction = Vector3.zero;
		direction += cameraForward.forward * sprint * speed * LYAxis_1;
		direction += cameraForward.right * sprint * speed * LXAxis_1;

		animator.SetFloat("Velocity",direction.normalized.magnitude);
		// Use simplemove on the charController
		charController.SimpleMove(direction);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		var body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;

		if (hit.moveDirection.y < -0.3F)
			return;

		var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower;
	}

	public override void OnDeath()
	{
		Destroy(gameObject);
		UIManager.Instance.SetActiveAll(false);
		UIManager.Instance.endState.gameObject.SetActive(true);
		UIManager.Instance.endState.DisplayInfo("Aww noo!\nYou failed to escape the witches dungeon.");
		
	}
}
