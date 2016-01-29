
using UnityEngine;

public class PortraitSpin : Entity
{
    public int turnSpeed = 3;
	public int spinSpeed = 360;
	public float distance = 10;

    public Transform childTransform;
    private Transform target;

	private bool hasLockedOn = false;

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

		if (Vector3.Distance(target.transform.position, transform.position) < distance)
		{
			hasLockedOn = true;
		}

		if (hasLockedOn == false)
		{
			return;
		}

		childTransform.rotation = Quaternion.Slerp(childTransform.rotation, Quaternion.LookRotation(target.position - childTransform.position), turnSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, (transform.position - target.position).normalized * 4.0f + target.position, Time.deltaTime * 3.0f);
    }

	public override void OnDeath()
	{
		Destroy(gameObject);
	}
}
