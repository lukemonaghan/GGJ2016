
using System.Collections;
using UnityEngine;

public class PortraitSpin : Entity
{
	[Header("Movement")]
    public int moveSpeed = 3;
    public int turnSpeed = 3;
	public int spinSpeed = 360;

	[Header("Targeting")]
	public float followDistance = 10;
	public float stopDistance = 3;
	public float shootDistance = 5;

	[Header("Combat")]
	public float shootCooldown = 2;

	[Header("References")]
    public Transform childTransform;
    private Transform target;

	private bool hasLockedOn = false;
	private Coroutine shootRoutine;

	public Collider collider { get { return _collider ?? (_collider = GetComponentInChildren<Collider>()); } }
	private Collider _collider;

	public Rigidbody rigidBody { get { return _rigidBody ?? (_rigidBody = GetComponentInChildren<Rigidbody>()); } }
	private Rigidbody _rigidBody;

	// Use this for initialization
	void Start ()
    {
		target = GameObject.FindWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (target == null)
		{
			return;
		}

		var dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist < followDistance)
		{
			hasLockedOn = true;
		}

		if (hasLockedOn == false)
		{
			return;
		}

		if (dist < shootDistance && shootRoutine == null)
		{
			shootRoutine = StartCoroutine(Shoot());
		}

		childTransform.rotation = Quaternion.Slerp(childTransform.rotation, Quaternion.LookRotation(target.position - childTransform.position), turnSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, (transform.position - target.position).normalized * stopDistance + target.position, Time.deltaTime * moveSpeed);

		if (rigidBody.position.y > 2)
		{
			rigidBody.position = new Vector3(rigidBody.position.x, 2, rigidBody.position.z);
		}

		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
	}

	IEnumerator Shoot()
	{
		yield return new WaitForSeconds(shootCooldown);
		var spellObject = Instantiate(GameParameters.Instance.Projectile);
		spellObject.transform.position = childTransform.position + childTransform.forward*1.5f;
		spellObject.transform.rotation = childTransform.rotation;

		Physics.IgnoreCollision(collider, spellObject.GetComponentInChildren<Collider>());

		var projectile = spellObject.GetComponent<Projectile>();
		projectile.speed *= shootCooldown;

		shootRoutine = null;
	}

	public override void OnDeath()
	{
		Destroy(gameObject);

		for (var i = 0; i < deathBits.Length; i++)
		{
			Instantiate(deathBits[i], transform.position, transform.rotation);
		}
	}
}
